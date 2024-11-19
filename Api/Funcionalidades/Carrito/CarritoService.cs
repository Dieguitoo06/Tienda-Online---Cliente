using Api.Funcionalidaades.Carritos;
using Biblioteca;
using Microsoft.EntityFrameworkCore;

public interface ICarritoService
{
    Task<CarritoQueryDto> CreateCarrito(CarritoQueryDto carritoDto);
    Task<List<CarritoQueryDto>> GetCarritos();
    Task<CarritoQueryDto?> GetCarritoByNro(int nroCarrito);
    Task<string> DeleteCarrito(int nroCarrito);
    Task<string> CerrarCarrito(int nroCarrito);
    Task<string> CancelarCarrito(int nroCarrito);
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
   public async Task<string> CerrarCarrito(int nroCarrito)
{
    var carrito = await _context.Carritos
        .Include(c => c.Productos)
            .ThenInclude(p => p.Producto)
        .FirstOrDefaultAsync(c => c.NroCarrito == nroCarrito);

    if (carrito == null)
        return "Carrito no encontrado";

    if (!carrito.Productos.Any())
        return "El carrito está vacío";

    if (carrito.Estado != "Abierto")
        return "El carrito ya está cerrado o cancelado";

    // Verificar stock disponible antes de cerrar
    foreach (var item in carrito.Productos)
    {
        if (item.Producto.Stock < item.Cantidad)
            return $"No hay suficiente stock para el producto {item.Producto.Nombre}";
        
        // Actualizar el stock solo al cerrar el carrito
        item.Producto.Stock -= item.Cantidad;
    }

    carrito.Estado = "Cerrado";
    await _context.SaveChangesAsync();

    return "Carrito cerrado exitosamente";
}

public async Task<string> CancelarCarrito(int nroCarrito)
{
    var carrito = await _context.Carritos
        .Include(c => c.Productos)
        .FirstOrDefaultAsync(c => c.NroCarrito == nroCarrito);

    if (carrito == null)
        return "Carrito no encontrado";

    if (carrito.Estado != "Abierto")
        return "El carrito ya está cerrado o cancelado";

    // Eliminar los items del carrito sin modificar el stock
    _context.ItemCarritos.RemoveRange(carrito.Productos);
    carrito.Estado = "Cancelado";
    await _context.SaveChangesAsync();

    return "Carrito cancelado exitosamente";
}
}