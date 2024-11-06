using Api.Funcionalidades.ItemCarritos;

namespace Api.Funcionalidaades.Carritos;
public class CarritoQueryDto
{
    public int NroCarrito { get; set; }
    public decimal Total { get; set; }

    public List<ItemCarritoQueryDto> Productos { get; set; } = new();
}

public class CarritoCommandoDto
{
    public decimal Total { get; set; } = 0;

}