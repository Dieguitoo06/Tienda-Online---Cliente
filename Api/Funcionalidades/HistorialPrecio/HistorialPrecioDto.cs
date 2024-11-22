using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Funcionalidades.HistorialPrecio
{
    public class HistorialPrecioDto
    {
        public int idHistorialPrecio { get; set; }
        public required decimal Monto { get; set; }
        public required string Moneda { get; set; } // Ejemplo: "USD", "EUR"
        public required DateTime Fecha { get; set; }
        public required int idProducto { get; set; } // ID del producto relacionado
    }

    public class HistorialPrecioCommandDto
    {
        public required decimal Monto { get; set; }
        public required string Moneda { get; set; }
        public required DateTime Fecha { get; set; }
        public required int idProducto { get; set; }
    }
}