using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca;

[Table("ItemCarrito")]    
public class ItemCarrito
{
    [Required]
    public decimal Subtotal { get; set; }

    [Required]
    public int Cantidad { get; set; }

    [Required]
    [ForeignKey("Carrito")]
    public  required Producto Producto { get; set; }

    [Key]
    [Required]
    public int idItemCarrito { get; set; }

}
