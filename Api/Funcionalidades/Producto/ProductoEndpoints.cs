using Biblioteca;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidaades.Clientes;

public static class ProductoEndpoints
{
    public static RouteGroupBuilder MapProductoEndPoints(this RouteGroupBuilder app)
    {
        app.MapGet("/Producto/Mostrar", (AplicacionDbContext context) =>
        {
            var Producto = context.Productos.ToList();
            return Results.Ok(Producto);
        });

        app.MapPost("/productos", async (Producto producto, AplicacionDbContext context) =>
        {
            context.Productos.Add(producto);
            await context.SaveChangesAsync();
            return Results.Created($"/productos/{producto.idProducto}", producto);
        });

        app.MapGet("/productos", async (AplicacionDbContext context, string search = null, decimal? minPrice = null, decimal? maxPrice = null) =>
        {
            var query = context.Productos.Include(p => p.Categoria).AsQueryable(); // Asegúrate de incluir la entidad Categoria

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => 
                    p.Nombre.Contains(search) || 
                    p.Descripcion.Contains(search) || 
                    p.Categoria.Nombre.Contains(search)); // Accede al Nombre de la Categoria
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.PrecioUnitario >= minPrice.Value); // Asegúrate de usar PrecioUnitario
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.PrecioUnitario <= maxPrice.Value); // Asegúrate de usar PrecioUnitario
            }

            var productos = await query.ToListAsync();
            return Results.Ok(productos);
        });

        app.MapDelete("/productos/{id}", async (int idProducto, AplicacionDbContext context) =>
        {
            var producto = await context.Productos.FindAsync(idProducto);
            if (producto is null)
            {
                return Results.NotFound();
            }

            context.Productos.Remove(producto);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });

        
        
        return app;
    }

}