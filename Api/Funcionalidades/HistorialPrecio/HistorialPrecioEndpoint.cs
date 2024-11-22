using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using Api.Funcionalidades.HistorialPrecio;


namespace Api.Funcionalidades.HistorialPrecioEndPoint;

public static class HistorialPrecioEndpoints
{
    public static RouteGroupBuilder MapHistorialPrecioEndPoints(this RouteGroupBuilder app)
    {

        app.MapPost("/historial-precios", async (IHistorialPrecioService historialPrecioService, IValidator<HistorialPrecioCommandDto> validator, HistorialPrecioCommandDto historialPrecioDto) =>
        {
            var validationResult = await validator.ValidateAsync(historialPrecioDto);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var resultado = await historialPrecioService.CreateHistorialPrecio(historialPrecioDto);
            return Results.Ok(resultado);
        });

        app.MapDelete("/historial-precios/{id}", async (IHistorialPrecioService historialPrecioService, int id) =>
        {
            var resultado = await historialPrecioService.DeleteHistorialPrecio(id);
            if (resultado == "Historial de precio no encontrado")
            {
                return Results.NotFound(resultado);
            }

            return Results.Ok(resultado);
        });

        return app;
    }
}
