using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class Producto
    {
        public string Nombre { get; set;}
        public string Descripcion { get; set;}
        public decimal PrecioUnitario { get; set;}
        public int Stock { get; set; }

        public Producto (string nombre, string descripcion, decimal preciounitario,  int stock)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            PrecioUnitario = preciounitario;
            Stock = stock;
        }
    }
}