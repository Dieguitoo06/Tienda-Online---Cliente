using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca;

[Table("Carrito")]
public class Carrito
{   
    [Key]
    [Required]
    public int NroCarrito { get; set; }

    [Required]
    public decimal Total { get; set; } = 0;

    public List<ItemCarrito> Productos { get; set; } = new List<ItemCarrito>();

    public Carrito()
    {
        Productos = new List<ItemCarrito>();
    }
}
