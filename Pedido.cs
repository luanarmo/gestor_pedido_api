namespace gestorPedidoAPI
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public List<PedidoProducto>? PedidoProductos { get; set; }
    }
}
