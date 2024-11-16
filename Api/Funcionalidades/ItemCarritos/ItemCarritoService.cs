using Biblioteca;
using Microsoft.EntityFrameworkCore;
using Api.Funcionalidades.ItemCarritos;

public interface IItemCarritoService
{
    Task<List<ItemCarritoQueryDto>> GetItemCarritos();
    Task<ItemCarritoQueryDto?> GetItemCarrito(int id);
    Task<bool> UpdateItemCarrito(int nroCarrito, ItemCarritoCommandDto itemCarritoDto);
}

public class ItemCarritoService : IItemCarritoService
{
    private readonly AplicacionDbContext _context;

    public ItemCarritoService(AplicacionDbContext context)
    {
        _context = context;
    }

    public async Task<List<ItemCarritoQueryDto>> GetItemCarritos()
    {
        return await _context.ItemCarritos
            .Select(i => new ItemCarritoQueryDto
            {
                idItemCarrito = i.idItemCarrito,
                Cantidad = i.Cantidad,
                idProducto = i.Producto.idProducto,
                Subtotal = i.Subtotal
            })
            .ToListAsync();
    }

    public async Task<ItemCarritoQueryDto?> GetItemCarrito(int id)
    {
        var carrito = await _context.Carritos
            .Select(c => new ItemCarritoQueryDto
            {
                idItemCarrito = c.NroCarrito,
                Cantidad = c.Productos.Sum(i => i.Cantidad),
                Subtotal = c.Productos.Sum(i => i.Subtotal)
            })
            .FirstOrDefaultAsync(c => c.idItemCarrito == id);

        return carrito;
    }

    public async Task<bool> UpdateItemCarrito(int nroCarrito, ItemCarritoCommandDto itemCarritoDto)
    {
        var item = await _context.ItemCarritos
            .Include(i => i.Producto)
            .FirstOrDefaultAsync(i => i.NroCarrito == nroCarrito && 
                                    i.Producto.idProducto == itemCarritoDto.idProducto);

        if (item == null)
            return false;

        item.Cantidad = itemCarritoDto.Cantidad;
        item.Subtotal = itemCarritoDto.Cantidad * item.Producto.PrecioUnitario;

        await _context.SaveChangesAsync();
        return true;
    }
}