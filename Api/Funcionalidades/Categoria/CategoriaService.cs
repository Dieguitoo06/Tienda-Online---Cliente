using Microsoft.EntityFrameworkCore;
using Biblioteca;
using Api.Funcionalidades.Clientes.Categoria;

public interface ICategoriaService
{
    Task<List<CategoriaDto>> GetCategorias();
    Task<CategoriaDto?> GetCategoria(int id);
    Task<string> CreateCategoria(CategoriaCommandDto categoriaDto);
    Task<string> UpdateCategoria(int id, CategoriaCommandDto categoriaDto);
    Task<string> DeleteCategoria(int id);
}

public class CategoriaService : ICategoriaService
{
    private readonly AplicacionDbContext _context;

    public CategoriaService(AplicacionDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoriaDto>> GetCategorias()
    {
        return await _context.Categorias
            .Select(c => new CategoriaDto
            {
                idCategoria = c.idCategoria,
                Nombre = c.Nombre
            })
            .ToListAsync();
    }

    public async Task<CategoriaDto?> GetCategoria(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        
        if (categoria == null)
            return null;

        return new CategoriaDto
        {
            idCategoria = categoria.idCategoria,
            Nombre = categoria.Nombre
        };
    }

    public async Task<string> CreateCategoria(CategoriaCommandDto categoriaDto)
    {
        var categoria = new Categoria
        {
            Nombre = categoriaDto.Nombre
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return "Categoría creada exitosamente";
    }

    public async Task<string> UpdateCategoria(int id, CategoriaCommandDto categoriaDto)
    {
        var categoriaExistente = await _context.Categorias.FindAsync(id);
        if (categoriaExistente == null)
        {
            return "Categoría no encontrada";
        }

        categoriaExistente.Nombre = categoriaDto.Nombre;
        await _context.SaveChangesAsync();

        return "Categoría actualizada exitosamente";
    }

    public async Task<string> DeleteCategoria(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return "Categoría no encontrada";
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return "Categoría eliminada exitosamente";
    }
}