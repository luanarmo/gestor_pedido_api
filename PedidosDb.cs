
namespace gestorPedidoAPI
{
    using Microsoft.EntityFrameworkCore;
    public class PedidosDb(DbContextOptions<PedidosDb> options) : DbContext(options)
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<PedidoProducto> PedidoProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoProducto>()
                .HasKey(pp => new { pp.PedidoId, pp.ProductoId });
        }
    }
}
