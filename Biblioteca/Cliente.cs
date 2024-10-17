using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca;

[Table("Cliente")]
public class Cliente
{   
    [Key]
    [Required]
    public int Dni { get; set; }

    [Required]
    public required string Nombre { get; set;}

    [Required]
    public required string Apellido { get; set; }

    [StringLength(50)]
    public required string Email { get; set; }    

    [Required]
    [StringLength(50)]
    public required string Usuario { get; set; } 

    [Required]
    [StringLength(50)]
    public required string Contraseña { get; set; }

}
