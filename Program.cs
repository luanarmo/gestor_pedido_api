using gestorPedidoAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PedidosDb>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var productosItems = app.MapGroup("/productos");
var pedidosItems = app.MapGroup("/pedidos");

// Productos API
productosItems.MapGet("/", async (PedidosDb db) => await db.Productos.ToListAsync());

productosItems.MapGet("/{id}", async (PedidosDb db, int id) =>
{
    return await db.Productos.FindAsync(id) is Producto producto ? Results.Ok(producto) : Results.NotFound();
});

productosItems.MapPost("/", async (PedidosDb db, Producto producto) =>
{
    db.Productos.Add(producto);
    await db.SaveChangesAsync();
    return Results.Created($"/productos/{producto.Id}", producto);
});


productosItems.MapPut("/{id}", async (PedidosDb db, int id, Producto producto) =>
{
    var productoActual = await db.Productos.FindAsync(id);

    if (productoActual is null) return Results.NotFound();

    productoActual.Nombre = producto.Nombre;
    productoActual.Precio = producto.Precio;

    await db.SaveChangesAsync();
    return Results.NoContent();

});

productosItems.MapDelete("/{id}", async (PedidosDb db, int id) =>
{
    var producto = await db.Productos.FindAsync(id);
    if (producto == null)
    {
        return Results.NotFound();
    }

    db.Productos.Remove(producto);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Pedidos API
pedidosItems.MapGet("/", async (PedidosDb db) => await db.Pedidos.ToListAsync());

pedidosItems.MapGet("/{id}", async (PedidosDb db, int id) =>
{
    return await db.Pedidos.FindAsync(id) is Pedido pedido ? Results.Ok(pedido) : Results.NotFound();
});

pedidosItems.MapPost("/", async (PedidosDb db, Pedido pedido) =>
{
    db.Pedidos.Add(pedido);
    await db.SaveChangesAsync();
    return Results.Created($"/pedidos/{pedido.Id}", pedido);
});

pedidosItems.MapPut("/{id}", async (PedidosDb db, int id, Pedido pedido) =>
{
    var pedidoActual = await db.Pedidos.FindAsync(id);

    if (pedidoActual is null) return Results.NotFound();

    pedidoActual.Fecha = pedido.Fecha;
    pedidoActual.ClienteId = pedido.ClienteId;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

pedidosItems.MapDelete("/{id}", async (PedidosDb db, int id) =>
{
    var pedido = await db.Pedidos.FindAsync(id);
    if (pedido == null)
    {
        return Results.NotFound();
    }

    db.Pedidos.Remove(pedido);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

pedidosItems.MapPost("/{id}/productos", async (PedidosDb db, int id, PedidoProductoDTO pedidoProductoDTO) =>
{
    var pedido = await db.Pedidos
    .Include(p => p.PedidoProductos)
    .FirstOrDefaultAsync(p => p.Id == id);

    if (pedido == null)
    {
        return Results.NotFound("El pedido no existe");
    }

    var producto = await db.Productos.FindAsync(pedidoProductoDTO.Producto);
    if (producto == null)
    {
        return Results.NotFound("El producto no existe");
    }

    var pedidoProductoExistente = pedido.PedidoProductos?
        .FirstOrDefault(pp => pp.ProductoId == pedidoProductoDTO.Producto);

    if (pedidoProductoExistente != null)
    {
        // Si el producto ya existe en el pedido, actualiza la cantidad
        pedidoProductoExistente.Cantidad += pedidoProductoDTO.Cantidad;
    }
    else
    {
        // Si el producto no existe en el pedido, crea una nueva relación
        var pedidoProductoNuevo = new PedidoProducto
        {
            PedidoId = id,
            ProductoId = pedidoProductoDTO.Producto,
            Cantidad = pedidoProductoDTO.Cantidad
        };
        pedido.PedidoProductos ??= new List<PedidoProducto>();
        pedido.PedidoProductos.Add(pedidoProductoNuevo);
    }

    await db.SaveChangesAsync();

    return Results.Created($"/pedidos/{id}/productos/{pedidoProductoDTO.Producto}", pedidoProductoDTO);
});

pedidosItems.MapGet("/{id}/productos", async (PedidosDb db, int id) =>
{
    var pedido = await db.Pedidos.Include(p => p.PedidoProductos).FirstOrDefaultAsync(p => p.Id == id);
    if (pedido == null)
    {
        return Results.NotFound();
    }

    var pedidoProductos = pedido.PedidoProductos?.Select(pp => new PedidoProductoDTO(pp)).ToList();
    return Results.Ok(pedidoProductos);
});

pedidosItems.MapPut("/{id}/productos/{productoId}", async (PedidosDb db, int id, int productoId, PedidoProductoDTO pedidoProductoDTO) =>
{
    var pedido = await db.Pedidos
        .Include(p => p.PedidoProductos)
        .FirstOrDefaultAsync(p => p.Id == id);

    if (pedido == null)
    {
        return Results.NotFound("El pedido no existe");
    }

    var pedidoProducto = pedido.PedidoProductos?
        .FirstOrDefault(pp => pp.ProductoId == productoId);

    if (pedidoProducto == null)
    {
        return Results.NotFound("El producto no está en este pedido");
    }

    pedidoProducto.Cantidad = pedidoProductoDTO.Cantidad;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

pedidosItems.MapDelete("/{id}/productos/{productoId}", async (PedidosDb db, int id, int productoId) =>
{
    var pedido = await db.Pedidos
        .Include(p => p.PedidoProductos)
        .FirstOrDefaultAsync(p => p.Id == id);

    if (pedido == null)
    {
        return Results.NotFound("El pedido no existe");
    }

    var pedidoProducto = pedido.PedidoProductos?
        .FirstOrDefault(pp => pp.ProductoId == productoId);

    if (pedidoProducto == null)
    {
        return Results.NotFound("El producto no está en este pedido");
    }

    pedido.PedidoProductos!.Remove(pedidoProducto);
    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.Run();