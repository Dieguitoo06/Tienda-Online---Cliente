using Biblioteca;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Api.Funcionalidades.ItemCarritos;

public static class ItemCarritoEndpoints
{ 
    public static RouteGroupBuilder MapitemcarritoEndPoints(this RouteGroupBuilder app)
    {
        // GET: Obtener todos los items
        app.MapGet("/itemcarrito", async (IItemCarritoService itemCarritoService) =>
        {
            var items = await itemCarritoService.GetItemCarritos();
            return items.Any() 
                ? Results.Ok(items)
                : Results.NotFound("No hay items en el carrito.");
        });

        // GET: Obtener item por ID
        app.MapGet("/itemcarrito/{id}", async (IItemCarritoService itemCarritoService, int id) =>
        {
            var item = await itemCarritoService.GetItemCarrito(id);
            return item != null 
                ? Results.Ok(item)
                : Results.NotFound("Item no encontrado.");
        });

        // GET: Obtener items por carrito
        app.MapGet("/itemcarrito/carrito/{nroCarrito}", async (IItemCarritoService itemCarritoService, int nroCarrito) =>
        {
            var items = await itemCarritoService.GetItemCarritosByCarrito(nroCarrito);
            return items.Any()
                ? Results.Ok(items)
                : Results.NotFound($"No hay items en el carrito {nroCarrito}.");
        });

        // POST: Crear nuevo item
        app.MapPost("/itemcarrito", async (IItemCarritoService itemCarritoService, ItemCarritoCommandDto dto) =>
        {
            try
            {
                var nuevoItem = await itemCarritoService.CreateItemCarrito(dto);
                return Results.Created($"/itemcarrito/{nuevoItem.idItemCarrito}", nuevoItem);
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Error al crear el item: {ex.Message}");
            }
        });

        // PUT: Actualizar item
        app.MapPut("/itemcarrito/{id}", async (IItemCarritoService itemCarritoService, int id, ItemCarritoCommandDto dto) =>
        {
            try
            {
                var resultado = await itemCarritoService.UpdateItemCarrito(id, dto);
                return resultado 
                    ? Results.Ok("Item actualizado correctamente")
                    : Results.NotFound("Item no encontrado");
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Error al actualizar el item: {ex.Message}");
            }
        });

        // DELETE: Eliminar item
        app.MapDelete("/itemcarrito/{id}", async (IItemCarritoService itemCarritoService, int id) =>
        {
            var resultado = await itemCarritoService.DeleteItemCarrito(id);
            return resultado
                ? Results.Ok("Item eliminado correctamente")
                : Results.NotFound("Item no encontrado");
        });

        return app;
    }
}