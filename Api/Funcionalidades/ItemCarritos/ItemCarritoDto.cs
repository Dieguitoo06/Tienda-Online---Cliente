using Biblioteca;

namespace Api.Funcionalidades.ItemCarritos;

// Lectura
public class ItemCarritoQueryDto
{
    public Guid Id { get; set; }
    public int Cantidad { get; set; }
    public required Producto Producto{ get; set; }
}


// Escritura
public class ItemCarritoCommandDto
{
    public decimal Subtotal { get; set; }
    public int Cantidad { get; set; }
    public required Producto Producto { get; set; }
    public int idItemCarrito { get; set; }
}

