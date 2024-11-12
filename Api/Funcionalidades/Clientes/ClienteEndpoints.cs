using Biblioteca;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Api.Funcionalidaades.Clientes;

public static class ClienteEndpoints
{
    public static RouteGroupBuilder MapClienteEndPoints(this RouteGroupBuilder app)
    {
        app.MapGet("/clientes", async (IClienteService clienteService) =>
        {
            var clientes = await clienteService.GetClientes();
            return Results.Ok(clientes);
        });

        app.MapPost("/clientes", async (IClienteService clienteService, UsuariosCommandDto clienteDto) =>
        {
            var resultado = await clienteService.CreateCliente(clienteDto);
            return Results.Ok(resultado);
        });

        app.MapGet("/clientes/{dni}", async (IClienteService clienteService, int dni) =>
        {
            var cliente = await clienteService.GetClienteByDni(dni);
            
            if (cliente == null)
            {
                return Results.NotFound("Cliente no encontrado");
            }

            return Results.Ok(cliente);
        });

        app.MapPut("/clientes/{dni}", async (IClienteService clienteService, int dni, UsuariosCommandDto clienteDto) =>
        {
            var resultado = await clienteService.UpdateCliente(dni, clienteDto);
            
            if (resultado == "Cliente no encontrado")
            {
                return Results.NotFound(resultado);
            }

            return Results.Ok(resultado);
        });

        return app;
    }
}