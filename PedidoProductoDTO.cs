using System.ComponentModel.DataAnnotations;

namespace gestorPedidoAPI
{
    public class PedidoProductoDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "El ID del producto debe ser un número positivo.")]
        public int Producto { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo.")]
        public int Cantidad { get; set; }

        public PedidoProductoDTO() { }
        public PedidoProductoDTO(PedidoProducto pedidoProducto) => (Producto, Cantidad) = (pedidoProducto.ProductoId, pedidoProducto.Cantidad);
    }
}
