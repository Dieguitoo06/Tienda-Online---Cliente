using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class ItemCarrito
    {
        public int Subtotal { get; set; }
        public int Cantidad { get; set; }
        public Producto Producto { get; set; }

    public ItemCarrito (int subtotal, int cantidad, Producto producto )
    {
        Subtotal = subtotal;
        Cantidad = cantidad;
        Producto = producto;
    }
    }
}