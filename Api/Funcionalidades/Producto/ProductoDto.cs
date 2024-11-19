using Microsoft.AspNetCore.SignalR;

namespace Api.Funcionalidaades.Productos;

public class ProductosQueryDto
{
    public int idProducto { get; set; }
    public required string NombreCategoria { get; set; }
    public int Stock { get; set; } 
    public decimal PrecioUnitario { get; set;}

}

public class ProductosCommandDto
{
    public int idProducto {get; set; }
    public required string Nombre {get; set; }
    public  required string Descripcion {get; set;} 
    public decimal PrecioUnitario { get; set; }
    public int Stock{ get; set; }
    public int idCategoria { get; set; }
}
public class ProductoPrecioRangoDto
{
    public decimal PrecioMinimo { get; set; }
    public decimal PrecioMaximo { get; set; }
}