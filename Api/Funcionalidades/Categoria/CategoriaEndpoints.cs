using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Api.Funcionalidades.Clientes.Categoria;

public static class CategoriaEndpoints
{
    public static RouteGroupBuilder MapCategoriaEndPoints(this RouteGroupBuilder app)
    {
        app.MapGet("/categorias", async (ICategoriaService categoriaService) =>
        {
            var categorias = await categoriaService.GetCategorias();
            return Results.Ok(categorias);
        });

        app.MapGet("/categorias/{id}", async (ICategoriaService categoriaService, int id) =>
        {
            var categoria = await categoriaService.GetCategoria(id);
            
            if (categoria == null)
            {
                return Results.NotFound("Categoría no encontrada");
            }

            return Results.Ok(categoria);
        });

        app.MapPost("/categorias", async (ICategoriaService categoriaService, CategoriaCommandDto categoriaDto) =>
        {
            var resultado = await categoriaService.CreateCategoria(categoriaDto);
            return Results.Ok(resultado);
        });

        app.MapPut("/categorias/{id}", async (ICategoriaService categoriaService, int id, CategoriaCommandDto categoriaDto) =>
        {
            var resultado = await categoriaService.UpdateCategoria(id, categoriaDto);
            
            if (resultado == "Categoría no encontrada")
            {
                return Results.NotFound(resultado);
            }

            return Results.Ok(resultado);
        });

        app.MapDelete("/categorias/{id}", async (ICategoriaService categoriaService, int id) =>
        {
            var resultado = await categoriaService.DeleteCategoria(id);
            
            if (resultado == "Categoría no encontrada")
            {
                return Results.NotFound(resultado);
            }

            return Results.Ok(resultado);
        });

        return app;
    }
}