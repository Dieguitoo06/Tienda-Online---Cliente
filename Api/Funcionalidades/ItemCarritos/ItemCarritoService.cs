using Biblioteca;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.ItemCarritos;

public interface IItemCarritoService
{
    Task<List<ItemCarritoQueryDto>> GetItemCarritos();
    Task<ItemCarritoQueryDto?> GetItemCarrito(int id);
    Task<List<ItemCarritoQueryDto>> GetItemCarritosByCarrito(int nroCarrito);
    Task<ItemCarritoQueryDto> AddItemToCarrito(ItemCarritoCommandDto dto);
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

    public async Task<ItemCarritoQueryDto> AddItemToCarrito(ItemCarritoCommandDto dto)
{
    // Verificar si el producto existe
    var producto = await _context.Productos.FindAsync(dto.idProducto)
        ?? throw new Exception("El producto no existe");

    // Verificar si el carrito existe y está abierto
    var carrito = await _context.Carritos
        .Include(c => c.Productos)
        .FirstOrDefaultAsync(c => c.NroCarrito == dto.NroCarrito)
        ?? throw new Exception("El carrito no existe");

    if (carrito.Estado != "Abierto")
        throw new Exception("No se pueden agregar items a un carrito cerrado o cancelado");

    // Verificar stock disponible sin modificarlo aún
    if (producto.Stock < dto.Cantidad)
        throw new Exception("No hay suficiente stock disponible");

    // Verificar si ya existe el item en el carrito
    var itemExistente = await _context.ItemCarritos
        .FirstOrDefaultAsync(i => i.NroCarrito == dto.NroCarrito && i.idProducto == dto.idProducto);

    if (itemExistente != null)
    {
        // Actualizar cantidad y subtotal del item existente
        itemExistente.Cantidad += dto.Cantidad;
        itemExistente.Subtotal = itemExistente.Cantidad * producto.PrecioUnitario;
    }
    else
    {
        // Crear nuevo item
        var nuevoItem = new ItemCarrito
        {
            Cantidad = dto.Cantidad,
            Producto = producto,
            Carrito = carrito,
            Subtotal = dto.Cantidad * producto.PrecioUnitario,
            idProducto = producto.idProducto,
            NroCarrito = carrito.NroCarrito
        };
        
        _context.ItemCarritos.Add(nuevoItem);
        itemExistente = nuevoItem;
    }

    // Actualizar el total del carrito
    await _context.SaveChangesAsync();
    carrito.Total = await _context.ItemCarritos
        .Where(i => i.NroCarrito == carrito.NroCarrito)
        .SumAsync(i => i.Subtotal);

    await _context.SaveChangesAsync();

    return new ItemCarritoQueryDto
    {
        idItemCarrito = itemExistente.idItemCarrito,
        Cantidad = itemExistente.Cantidad,
        idProducto = producto.idProducto,
        NroCarrito = carrito.NroCarrito,
        Subtotal = itemExistente.Subtotal
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