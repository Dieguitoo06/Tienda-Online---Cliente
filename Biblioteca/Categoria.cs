using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca;

[Table("Categoria")]
public class Categoria
{
    [Key]
    [Required]
    public int idCategoria { get; set; }

    [Required]
    public required string Nombre { get; set; }

    public List<Producto> Productos { get; set; } = new List<Producto>();
}
