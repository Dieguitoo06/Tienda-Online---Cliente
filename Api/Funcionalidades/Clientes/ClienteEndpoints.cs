using Biblioteca;

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

        app.MapPost("/Cliente/Registrar", (AplicacionDbContext context, UsuariosCommandDto usuario) =>
        {
            Cliente nuevoCliente = new Cliente() {Usuario = usuario.Usuario, Apellido = usuario.Apellido, Contraseña = usuario.Contraseña, Dni = usuario.Dni, Nombre = usuario.Nombre, Email = usuario.Email};
            context.Clientes.Add(nuevoCliente);
            context.SaveChanges();
            return Results.Ok();
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

        app.MapPut("/cliente/Actualizar", async (AplicacionDbContext context, int dni, Cliente clienteActualizado) =>
        {
            var clienteExistente = await context.Clientes.FindAsync(dni);

            if (clienteExistente == null)
            {
                return Results.NotFound("Cliente no encontrado");
            }

            clienteExistente.Nombre = clienteActualizado.Nombre;
            clienteExistente.Apellido = clienteActualizado.Apellido;
            clienteExistente.Email = clienteActualizado.Email;
            clienteExistente.Usuario = clienteActualizado.Usuario;
            clienteExistente.Contraseña = clienteActualizado.Contraseña;

            context.SaveChanges();

            return Results.Ok("Cliente actualizado exitosamente");
        });

        return app;
    }
}