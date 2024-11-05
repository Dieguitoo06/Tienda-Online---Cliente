using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Api.Funcionalidades.Clientes.Categoria;

public static class CategoriaEndpoints
{
    public static RouteGroupBuilder MapCategoriaEndPoints(this RouteGroupBuilder app)
    {
        app.MapGet("/categoria/mostrar", async (AplicacionDbContext context) =>
        {
            var categorias = await context.Categorias.ToListAsync();
            return Results.Ok(categorias);
        });

        app.MapGet("/categoria/buscar/{id}", async (AplicacionDbContext context, int id) =>
        {
            var categoria = await context.Categorias.FindAsync(id);
            
            if (categoria == null)
            {
                return Results.NotFound("Categoría no encontrada");
            }

            return Results.Ok(categoria);
        });

        app.MapPost("/categoria/crear", async (AplicacionDbContext context, CategoriaCommandDto categoriaDto) =>
        {
            var categoria = new Biblioteca.Categoria
            {
                Nombre = categoriaDto.Nombre
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            return Results.Ok("Categoría creada exitosamente");
        });

        app.MapPut("/categoria/actualizar/{id}", async (AplicacionDbContext context, int id, CategoriaCommandDto categoriaDto) =>
        {
            var categoriaExistente = await context.Categorias.FindAsync(id);
            if (categoriaExistente == null)
            {
                return Results.NotFound("Categoría no encontrada");
            }

            categoriaExistente.Nombre = categoriaDto.Nombre;
            await context.SaveChangesAsync();

            return Results.Ok("Categoría actualizada exitosamente");
        });

        app.MapDelete("/categoria/eliminar/{id}", async (AplicacionDbContext context, int id) =>
        {
            var categoria = await context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return Results.NotFound("Categoría no encontrada");
            }

            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();

            return Results.Ok("Categoría eliminada exitosamente");
        });

        return app;
    }
}