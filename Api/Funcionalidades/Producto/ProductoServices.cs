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
}

public class ProductoService : IProductoService
{
    private readonly AplicacionDbContext _context;

    public ProductoService(AplicacionDbContext context)
    {
        _context = context;
    }

    public List<ProductosQueryDto> GetProductosPorRangoPrecio(decimal precioMinimo, decimal precioMaximo)
    {
        return _context.Productos
            .Where(p => p.PrecioUnitario >= precioMinimo && p.PrecioUnitario <= precioMaximo)
            .Select(p => new ProductosQueryDto
            {
                idProducto = p.idProducto,
                Nombre = p.Nombre,
                PrecioUnitario = p.PrecioUnitario  // Agregamos el precio a la selección
            })
            .ToList();
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

    public List<ProductosQueryDto> GetProductos()
    {
        return _context.Productos
            .Include(p => p.Categoria)
            .Select(p => new ProductosQueryDto
            {
                idProducto = p.idProducto,
                Nombre = p.Nombre,
                PrecioUnitario = p.PrecioUnitario  // Agregamos el precio a la selección
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

        producto.Nombre = productoDto.Nombre;
        producto.Descripcion = productoDto.Descripcion;
        producto.PrecioUnitario = productoDto.PrecioUnitario;
        producto.Stock = productoDto.Stock;
        producto.Categoria = categoria;

        _context.SaveChanges();
    }
    
}