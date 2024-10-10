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
        public double PrecioUnitario { get; set;}
        public string Categoria { get; set;}
        public int Stock { get; set; }

        public Producto (string nombre, string descripcion, int preciounitario, string categoria, int stock)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            PrecioUnitario = preciounitario;
            Categoria = categoria;
            Stock = stock;
        }
    }
}