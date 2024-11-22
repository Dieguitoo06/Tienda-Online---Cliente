using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca;

[Table("HistorialPrecio")]
public class HistorialPrecio
{   
    [Key]
    public int idHistorialPrecio {get; set;}

    [Required]
    public decimal Monto { get; set; }

    [Required]
    [StringLength(3)]
    public string Moneda { get; set; } // Ejemplo: "USD", "EUR"

    [Required]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; }

    [Required]
    [ForeignKey("Producto")]
    public int idProducto { get; set; } 

    [Required]
    public Producto Producto { get; set; } // Asumiendo que Producto es una clase existente
}