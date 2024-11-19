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
    public required string NombreCategoria { get; set; }

}
