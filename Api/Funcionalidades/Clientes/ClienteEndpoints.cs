using Biblioteca;
using Microsoft.AspNetCore.Identity;

namespace Api.Funcionalidaades.Clientes;

public static class ClienteEndpoints
{
    public static RouteGroupBuilder MapClienteEndPoints(this RouteGroupBuilder app)
    {
        app.MapGet("/Cliente/Mostrar", (AplicacionDbContext context) =>
        {
            var Cliente = context.Clientes.ToList();
            return Results.Ok(Cliente);
        });

        var passwordHasher = new PasswordHasher<Cliente>();

        app.MapPost("/cliente/Registrar", async (AplicacionDbContext context, Cliente nuevoCliente) =>
        {
            nuevoCliente.Contraseña = passwordHasher.HashPassword(nuevoCliente, nuevoCliente.Contraseña);

            context.Clientes.Add(nuevoCliente);
            await context.SaveChangesAsync();

            return Results.Ok("Cliente creado exitosamente");
        });


        app.MapGet("/cliente/Buscar", async (AplicacionDbContext context, int dni) =>
        {
            var cliente = await context.Clientes.FindAsync(dni);

            if (cliente == null)
            {
                return Results.NotFound("Cliente no encontrado");
            }

            return Results.Ok(cliente);
        });

       app.MapPut("/clientes/Actualizar", async (AplicacionDbContext context, int id, Cliente clienteActualizado) =>
        {
            var clienteExistente = await context.Clientes.FindAsync(id);
            if (clienteExistente == null)
            {
                return Results.NotFound("Cliente no encontrado");
            }

            clienteExistente.Nombre = clienteActualizado.Nombre;
            clienteExistente.Apellido = clienteActualizado.Apellido;
            clienteExistente.Email = clienteActualizado.Email;
            clienteExistente.Usuario = clienteActualizado.Usuario;

            if (!string.IsNullOrEmpty(clienteActualizado.Contraseña))
            {
                clienteExistente.Contraseña = passwordHasher.HashPassword(clienteExistente, clienteActualizado.Contraseña);
            }

            await context.SaveChangesAsync();
            return Results.Ok("Cliente actualizado exitosamente");
        });


        return app;
    }
}