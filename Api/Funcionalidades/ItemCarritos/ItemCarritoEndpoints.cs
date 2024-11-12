using Biblioteca;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Api.Funcionalidades.ItemCarritos;

public static class ItemCarritoEndpoints
{ 
    public static RouteGroupBuilder MapitemcarritoEndPoints(this RouteGroupBuilder app)
    {
        app.MapGet("/itemcarrito", async (IItemCarritoService itemCarritoService) =>
        {
            var items = await itemCarritoService.GetItemCarritos();
            return Results.Ok(items);
        });

        app.MapGet("/itemcarrito/{id}", async (IItemCarritoService itemCarritoService, int id) =>
        {
            var carrito = await itemCarritoService.GetItemCarrito(id);
            
            if (carrito == null)
            {
                return Results.NotFound("El carrito está vacío o no existe.");
            }

            return Results.Ok(carrito);
        });

        app.MapPut("/itemcarrito/{id}", async (IItemCarritoService itemCarritoService, int id, ItemCarritoCommandDto dto) =>
        {
            var resultado = await itemCarritoService.UpdateItemCarrito(id, dto);
            
            if (!resultado)
            {
                return Results.NotFound("Item no encontrado");
            }

            return Results.Ok("Cantidad actualizada.");
        });

        return app;
    }
}