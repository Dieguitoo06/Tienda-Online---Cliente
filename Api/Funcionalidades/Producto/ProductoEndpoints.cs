using Api.Funcionalidaades.Productos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public static class ProductoEndpoints
{
    public static RouteGroupBuilder MapProductoEndPoints(this RouteGroupBuilder app)
    {
        app.MapGet("/productos", async (IProductoService productoService) =>
        {
            var productos = productoService.GetProductos();
            return Results.Ok(productos);
        });


        app.MapPost("/productos", async (ProductosCommandDto producto, IProductoService productoService) =>
        {
            try 
            {
                productoService.CreateProducto(producto);
                return Results.Created($"/productos/{producto.idProducto}", producto);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapPut("/productos/{id}", async (int id, ProductosCommandDto producto, IProductoService productoService) =>
        {
            try 
            {
                productoService.UpdateProducto(id, producto);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapDelete("/productos/{id}", async (int id, IProductoService productoService) =>
        {
            try 
            {
                productoService.DeleteProducto(id);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        
        });
        
        app.MapGet("/productos/precio-rango", async (decimal precioMinimo, decimal precioMaximo, IProductoService productoService) =>
        {
            if (precioMinimo > precioMaximo)
            {
                return Results.BadRequest("El precio mínimo no puede ser mayor al precio máximo");
            }

            var productos = productoService.GetProductosPorRangoPrecio(precioMinimo, precioMaximo);
            return Results.Ok(productos);
        });
        
        return app;
    }
}