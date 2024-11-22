using Microsoft.EntityFrameworkCore;
using Biblioteca;
using Api.Funcionalidades.HistorialPrecio;

public interface IHistorialPrecioService
{
    Task<string> CreateHistorialPrecio(HistorialPrecioCommandDto historialPrecioDto);
    Task<string> DeleteHistorialPrecio(int id);
}

public class HistorialPrecioService : IHistorialPrecioService
{
    private readonly AplicacionDbContext _context;

    public HistorialPrecioService(AplicacionDbContext context)
    {
        _context = context;
    }

    public async Task<string> CreateHistorialPrecio(HistorialPrecioCommandDto historialPrecioDto)
    {
        var historialPrecio = new HistorialPrecio
        {
            Monto = historialPrecioDto.Monto,
            Moneda = historialPrecioDto.Moneda,
            Fecha = historialPrecioDto.Fecha,
            idProducto = historialPrecioDto.idProducto
        };

        _context.HistorialPrecios.Add(historialPrecio);
        await _context.SaveChangesAsync();

        return "Historial de precio creado exitosamente";
    }

    public async Task<string> DeleteHistorialPrecio(int id)
    {
        var historialPrecio = await _context.HistorialPrecios.FindAsync(id);
        if (historialPrecio == null)
        {
            return "Historial de precio no encontrado";
        }

        _context.HistorialPrecios.Remove(historialPrecio);
        await _context.SaveChangesAsync();

        return "Historial de precio eliminado exitosamente";
    }
}
