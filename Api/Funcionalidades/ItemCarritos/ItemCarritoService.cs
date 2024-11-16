using Biblioteca;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.ItemCarritos;

public interface IItemCarritoService
{
    Task<List<ItemCarritoQueryDto>> GetItemCarritos();
    Task<ItemCarritoQueryDto?> GetItemCarrito(int id);
    Task<List<ItemCarritoQueryDto>> GetItemCarritosByCarrito(int nroCarrito);
    Task<ItemCarritoQueryDto> CreateItemCarrito(ItemCarritoCommandDto dto);
    Task<bool> UpdateItemCarrito(int id, ItemCarritoCommandDto dto);
    Task<bool> DeleteItemCarrito(int id);
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
            .Include(i => i.Producto)
            .Include(i => i.Carrito)
            .Select(i => new ItemCarritoQueryDto
            {
                idItemCarrito = i.idItemCarrito,
                Cantidad = i.Cantidad,
                idProducto = i.Producto.idProducto,
                NroCarrito = i.Carrito.NroCarrito,
                Subtotal = i.Subtotal
            })
            .ToListAsync();
    }

    public async Task<ItemCarritoQueryDto?> GetItemCarrito(int id)
    {
        return await _context.ItemCarritos
            .Include(i => i.Producto)
            .Include(i => i.Carrito)
            .Where(i => i.idItemCarrito == id)
            .Select(i => new ItemCarritoQueryDto
            {
                idItemCarrito = i.idItemCarrito,
                Cantidad = i.Cantidad,
                idProducto = i.Producto.idProducto,
                NroCarrito = i.Carrito.NroCarrito,
                Subtotal = i.Subtotal
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<ItemCarritoQueryDto>> GetItemCarritosByCarrito(int nroCarrito)
    {
        return await _context.ItemCarritos
            .Include(i => i.Producto)
            .Include(i => i.Carrito)
            .Where(i => i.Carrito.NroCarrito == nroCarrito)
            .Select(i => new ItemCarritoQueryDto
            {
                idItemCarrito = i.idItemCarrito,
                Cantidad = i.Cantidad,
                idProducto = i.Producto.idProducto,
                NroCarrito = i.Carrito.NroCarrito,
                Subtotal = i.Subtotal
            })
            .ToListAsync();
    }

    public async Task<ItemCarritoQueryDto> CreateItemCarrito(ItemCarritoCommandDto dto)
    {
        var producto = await _context.Productos.FindAsync(dto.idProducto)
            ?? throw new Exception("El producto no existe");

        var carrito = await _context.Carritos.FindAsync(dto.NroCarrito)
            ?? throw new Exception("El carrito no existe");

        if (producto.Stock < dto.Cantidad)
            throw new Exception("No hay suficiente stock disponible");

        var itemCarrito = new ItemCarrito
        {
            Cantidad = dto.Cantidad,
            Producto = producto,
            Carrito = carrito,
            Subtotal = dto.Cantidad * producto.PrecioUnitario
        };

        _context.ItemCarritos.Add(itemCarrito);
        await _context.SaveChangesAsync();

        return new ItemCarritoQueryDto
        {
            idItemCarrito = itemCarrito.idItemCarrito,
            Cantidad = itemCarrito.Cantidad,
            idProducto = producto.idProducto,
            NroCarrito = carrito.NroCarrito,
            Subtotal = itemCarrito.Subtotal
        };
    }

    public async Task<bool> UpdateItemCarrito(int id, ItemCarritoCommandDto dto)
    {
        var item = await _context.ItemCarritos
            .Include(i => i.Producto)
            .FirstOrDefaultAsync(i => i.idItemCarrito == id);

        if (item == null)
            return false;

        var producto = await _context.Productos.FindAsync(dto.idProducto)
            ?? throw new Exception("El producto no existe");

        if (producto.Stock < dto.Cantidad)
            throw new Exception("No hay suficiente stock disponible");

        item.Cantidad = dto.Cantidad;
        item.Producto = producto;
        item.Subtotal = dto.Cantidad * producto.PrecioUnitario;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteItemCarrito(int id)
    {
        var item = await _context.ItemCarritos.FindAsync(id);
        if (item == null)
            return false;

        _context.ItemCarritos.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}