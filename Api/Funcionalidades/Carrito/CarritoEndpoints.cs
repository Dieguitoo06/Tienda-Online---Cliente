using Api.Funcionalidaades.Carritos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Api.Funcionalidades.Carritos;

public static class CarritoEndpoints
{
    public static RouteGroupBuilder MapCarritoEndpoints(this RouteGroupBuilder app)
    {
        app.MapPost("/carritos", async (ICarritoService carritoService, CarritoQueryDto request) =>
        {
            try
            {
                var nuevoCarrito = await carritoService.CreateCarrito(request);
                return Results.Ok(nuevoCarrito);
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Error al crear el carrito: {ex.Message}");
            }
        });

        app.MapGet("/carritos", async (ICarritoService carritoService) =>
        {
            var carritos = await carritoService.GetCarritos();
            return carritos.Any() 
                ? Results.Ok(carritos) 
                : Results.NotFound("No hay carritos registrados.");
        });

        app.MapGet("/carritos/{nroCarrito}", async (ICarritoService carritoService, int nroCarrito) =>
        {
            var carrito = await carritoService.GetCarritoByNro(nroCarrito);
            return carrito != null 
                ? Results.Ok(carrito) 
                : Results.NotFound($"No se encontró el carrito con número {nroCarrito}");
        });

        app.MapDelete("/carritos/{nroCarrito}", async (ICarritoService carritoService, int nroCarrito) =>
        {
            try
            {
                var resultado = await carritoService.DeleteCarrito(nroCarrito);
                return resultado.Contains("eliminado")
                    ? Results.Ok(resultado)
                    : Results.NotFound(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Error al eliminar el carrito: {ex.Message}");
            }
        });

        return app;
    }
}