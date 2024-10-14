using System.ComponentModel.DataAnnotations;

namespace gestorPedidoAPI
{
    public class Producto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 1000000, ErrorMessage = "El precio debe estar entre 0.01 y 1000000")]
        public decimal Precio { get; set; }

        public List<PedidoProducto>? PedidoProductos { get; set; }
    }
}
