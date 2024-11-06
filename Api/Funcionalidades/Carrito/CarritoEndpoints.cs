using Api.Funcionalidaades.Carritos;
using Biblioteca;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Carritos;

public static class CarritoEndpoints
{
    public static RouteGroupBuilder MapCarritoEndpoints(this RouteGroupBuilder app)
    {
        // 1. Crear un nuevo carrito
        app.MapPost("/Carrito/Crear", async (AplicacionDbContext context, CarritoQueryDto request) =>
        {
            try
            {
                var nuevoCarrito = new Carrito
                {
                    NroCarrito = request.NroCarrito,
                };

                context.Carritos.Add(nuevoCarrito);
                await context.SaveChangesAsync();

                return Results.Ok(new CarritoQueryDto
                {
                    NroCarrito = nuevoCarrito.NroCarrito,
                    Total = nuevoCarrito.Total,
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Error al crear el carrito: {ex.Message}");
            }
        });

        // 2. Obtener todos los carritos
        app.MapGet("/Carrito/Mostrar", async (AplicacionDbContext context) =>
        {
            var carritos = await context.Carritos
                .Select(c => new CarritoQueryDto
                {
                    NroCarrito = c.NroCarrito,
                    Total = c.Total,
                })
                .ToListAsync();

            return carritos.Any() 
                ? Results.Ok(carritos) 
                : Results.NotFound("No hay carritos registrados.");
        });

        // 3. Buscar carrito por NroCarrito
        app.MapGet("/Carrito/Buscar", async (AplicacionDbContext context, int nroCarrito) =>
        {
            var carrito = await context.Carritos
                .Where(c => c.NroCarrito == nroCarrito)
                .Select(c => new CarritoQueryDto
                {
                    NroCarrito = c.NroCarrito,
                    Total = c.Total,
                })
                .FirstOrDefaultAsync();

            return carrito != null 
                ? Results.Ok(carrito) 
                : Results.NotFound($"No se encontró el carrito con número {nroCarrito}");
        });

        // 4. Eliminar un carrito
        app.MapDelete("/Carrito/Eliminar", async (AplicacionDbContext context, int nroCarrito) =>
        {
            var carrito = await context.Carritos
                .FirstOrDefaultAsync(c => c.NroCarrito == nroCarrito);

            if (carrito == null)
                return Results.NotFound($"No se encontró el carrito con número {nroCarrito}");

            try
            {
                context.Carritos.Remove(carrito);
                await context.SaveChangesAsync();
                return Results.Ok($"Carrito {nroCarrito} eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Error al eliminar el carrito: {ex.Message}");
            }
        });

        return app;
    }
}