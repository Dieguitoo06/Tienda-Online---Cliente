using Biblioteca;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.ItemCarritos;

public static class ItemCarritoEndpoints
{
    public static RouteGroupBuilder MapItemcarritoEndPoints(this RouteGroupBuilder app) //Endpoint para mostrar la cantidad de Itemcarrito del carrito especifico
    {
        app.MapGet("/cantidad/Mostrar", (AplicacionDbContext context) =>
        {
            var Cantidad = context.ItemCarritos
            .Select( a => new
            {
                a.Producto
            })
            .ToList();
            return Results.Ok(Cantidad);
        });


    app.MapGet("/items/Mostrar", async (AplicacionDbContext context) => // Endpoint muestra todos los productos y la cantidad del carrito.
        {
            var items = await context.ItemCarritos
                .Include(i => i.Producto) 
                .Select(i => new
                {
                    i.Producto,
                    i.Cantidad,
                    ProductoNombre = i.Producto.Nombre,  
                    Subtotal = i.Cantidad * i.Producto.PrecioUnitario 
                })
                .ToListAsync();

            return Results.Ok(items);
        });

        app.MapPost("/items/Registrar", async (AplicacionDbContext context, Producto nuevoProducto) =>
        {
            context.Productos.Add(nuevoProducto);
            await context.SaveChangesAsync(); // Guardar cambios en la base de datos

            return Results.Ok("Producto creado exitosamente");
        });

        return app;
    }
}


// producto, cantidad, subtotal