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
    public string Nombre { get; set;}

    [Required]
    public string Apellido { get; set; }

    [StringLength(50)]
    public string Email { get; set; }    

    [Required]
    [StringLength(50)]
    public string Usuario { get; set; } 

    [Required]
    [StringLength(50)]
    public string Contraseña { get; set; }

}
