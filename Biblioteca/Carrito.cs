
namespace Biblioteca
{
    public class Carrito
    {
        public  int NroCarrito { get; set; }
        public decimal Total { get; set; }
        public List<ItemCarrito> Productos { get; set; } = new List<ItemCarrito>();

        public Carrito()
        {
            Productos = new List<ItemCarrito>();
        }
    }
}