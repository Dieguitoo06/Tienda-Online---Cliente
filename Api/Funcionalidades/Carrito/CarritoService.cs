using Api.Funcionalidaades.Carritos;
using Biblioteca;
using Microsoft.EntityFrameworkCore;

public interface ICarritoService
{
    Task<CarritoQueryDto> CreateCarrito(CarritoQueryDto carritoDto);
    Task<List<CarritoQueryDto>> GetCarritos();
    Task<CarritoQueryDto?> GetCarritoByNro(int nroCarrito);
    Task<string> DeleteCarrito(int nroCarrito);
}

public class CarritoService : ICarritoService
{
    private readonly AplicacionDbContext _context;

    public CarritoService(AplicacionDbContext context)
    {
        _context = context;
    }

    public async Task<CarritoQueryDto> CreateCarrito(CarritoQueryDto carritoDto)
    {
        var nuevoCarrito = new Carrito
        {
            NroCarrito = carritoDto.NroCarrito,
            Total = 0
        };

        _context.Carritos.Add(nuevoCarrito);
        await _context.SaveChangesAsync();

        return new CarritoQueryDto
        {
            NroCarrito = nuevoCarrito.NroCarrito,
            Total = nuevoCarrito.Total,
        };
    }

    public async Task<List<CarritoQueryDto>> GetCarritos()
    {
        return await _context.Carritos
            .Select(c => new CarritoQueryDto
            {
                NroCarrito = c.NroCarrito,
                Total = c.Total,
            })
            .ToListAsync();
    }

    public async Task<CarritoQueryDto?> GetCarritoByNro(int nroCarrito)
    {
        return await _context.Carritos
            .Where(c => c.NroCarrito == nroCarrito)
            .Select(c => new CarritoQueryDto
            {
                NroCarrito = c.NroCarrito,
                Total = c.Total,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<string> DeleteCarrito(int nroCarrito)
    {
        var carrito = await _context.Carritos
            .FirstOrDefaultAsync(c => c.NroCarrito == nroCarrito);

        if (carrito == null)
            return $"No se encontró el carrito con número {nroCarrito}";

        _context.Carritos.Remove(carrito);
        await _context.SaveChangesAsync();
        return $"Carrito {nroCarrito} eliminado exitosamente.";
    }
}