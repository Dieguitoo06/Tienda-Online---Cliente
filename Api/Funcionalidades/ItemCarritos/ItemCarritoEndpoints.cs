using Biblioteca;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.ItemCarritos;

public static class ItemCarritoEndpoints
{ 
    public static RouteGroupBuilder MapitemcarritoEndPoints(this RouteGroupBuilder app)
    {
         app.MapGet("/categoria/mostrar", async (AplicacionDbContext context) =>
        {
            var item = await context.ItemCarritos.ToListAsync();
            return Results.Ok(item);
        });

        app.MapGet("/categoria/buscar/{id}", async (AplicacionDbContext context, int id) =>
        {
            var carrito = await context.Carritos.FindAsync(id);
            
            if (carrito == null)
            {
                return Results.NotFound("El carrito está vacío o no existe.");
            }

            return Results.Ok(carrito);
        });

        app.MapPost("/ItemCarrito/mostrar", async (AplicacionDbContext context) =>
        {
            var producto = await context.ItemCarritos.ToListAsync();
            return Results.Ok(producto);
        });

        app.MapPut("/itemcarrito/actualizar", async (AplicacionDbContext context,int id, ItemCarritoCommandDto dto) =>
        {
           var item = await context.ItemCarritos
            .FirstOrDefaultAsync(i => i.NroCarrito == id && i.Producto.idProducto == dto.idProducto);

            item.Cantidad = dto.Cantidad;
            item.Subtotal = dto.Cantidad * item.Producto.PrecioUnitario;

            await context.SaveChangesAsync();

            return Results.Ok("Cantidad actualizada.");
        });
        return app;
    }
}
    
