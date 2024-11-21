using Api.Funcionalidaades.Productos;
using Biblioteca;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public interface IProductoService
{
    void CreateProducto(ProductosCommandDto productoDto);
    void DeleteProducto(int idProducto);  
    List<ProductosQueryDto> GetProductos();
    void UpdateProducto(int idProducto, ProductosCommandDto productoDto);  
    List<ProductosQueryDto> GetProductosPorRangoPrecio(decimal precioMinimo, decimal precioMaximo);
    List<ProductosQueryDto> BuscarProductos(string? nombre, string? nombreCategoria, int? stockMinimo);
}

public class ProductoService : IProductoService
{
    private readonly AplicacionDbContext _context;

    public ProductoService(AplicacionDbContext context)
    {
        _context = context;
    }

    public List<ProductosQueryDto> GetProductos()
    {
        return _context.Productos
            .Include(p => p.Categoria)
            .Select(p => new ProductosQueryDto
            {
                idProducto = p.idProducto,
                NombreCategoria = p.Nombre,
                PrecioUnitario = p.PrecioUnitario,
                Stock = p.Stock 
            })
            .ToList();
    }
    public List<ProductosQueryDto> BuscarProductos(string? nombre, string? NombreCategoria, int? stockMinimo)
    {
        var query = _context.Productos
            .Include(p => p.Categoria)
            .AsQueryable();

        if (!string.IsNullOrEmpty(nombre))
            query = query.Where(p => p.Nombre.Contains(nombre));

        if (!string.IsNullOrEmpty(NombreCategoria))
            query = query.Where(p => p.Categoria.NombreCategoria.Contains(NombreCategoria));

        if (stockMinimo.HasValue)
            query = query.Where(p => p.Stock >= stockMinimo);

        return query.Select(p => new ProductosQueryDto
        {
            idProducto = p.idProducto,
            PrecioUnitario = p.PrecioUnitario,
            Stock = p.Stock,
            NombreCategoria = p.Categoria.NombreCategoria
        }).ToList();
    }

    public void CreateProducto(ProductosCommandDto productoDto)
    {
        var categoria = _context.Categorias.Find(productoDto.idCategoria) 
            ?? throw new KeyNotFoundException("Categoría no encontrada");

        var producto = new Producto
        {
            Nombre = productoDto.Nombre,
            Descripcion = productoDto.Descripcion,
            PrecioUnitario = productoDto.PrecioUnitario,
            Stock = productoDto.Stock,
            Categoria = categoria
        };

        _context.Productos.Add(producto);
        _context.SaveChanges();
    }

    public void DeleteProducto(int idProducto)
    {
        var producto = _context.Productos.Find(idProducto);
        if (producto is null)
        {
            throw new KeyNotFoundException("Producto no encontrado");
        }

        _context.Productos.Remove(producto);
        _context.SaveChanges();
    }

    public List<ProductosQueryDto> GetProductosPorRangoPrecio(decimal precioMinimo, decimal precioMaximo)
    {
        return _context.Productos
            .Where(p => p.PrecioUnitario >= precioMinimo && p.PrecioUnitario <= precioMaximo)
            .Select(p => new ProductosQueryDto
            {
                idProducto = p.idProducto,
                NombreCategoria = p.Nombre,
                PrecioUnitario = p.PrecioUnitario,
                Stock = p.Stock
            })
            .ToList();
    }

    public void UpdateProducto(int idProducto, ProductosCommandDto productoDto)  
    {
        var producto = _context.Productos.Find(idProducto);
        if (producto is null)
        {
            throw new KeyNotFoundException("Producto no encontrado");
        }

        var categoria = _context.Categorias.Find(productoDto.idCategoria)
            ?? throw new KeyNotFoundException("Categoría no encontrada");

        producto.Stock = productoDto.Stock;

        _context.SaveChanges();
    }
    
}