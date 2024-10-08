using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class Carrito
    {
        public required int NroCarrito { get; set; }
        public int Total { get; set; }
        public List<ItemCarrito> Productos { get; set; } = new List<ItemCarrito>();

        public Carrito()
        {
            Productos = new List<ItemCarrito>();
        }
    }
}