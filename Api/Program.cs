using Api.Funcionalidaades.Clientes;
using Biblioteca;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("aplicacion_db");


builder.Services.AddDbContext<AplicacionDbContext>(opciones =>
    opciones.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 30))));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

   
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}

app.UseHttpsRedirection();


app.MapGet("/", () => Results.Redirect("/swagger"));

using (var scope = app.Services.CreateScope())
{
    var contexto = scope.ServiceProvider.GetRequiredService<AplicacionDbContext>();
    contexto.Database.EnsureCreated();
}

app.MapGet("/Cliente", (AplicacionDbContext context) =>
{
    var Cliente = context.Clientes.ToList();
    return Results.Ok(Cliente);
});

app.MapPost("/Cliente", (AplicacionDbContext context, UsuariosCommandDto usuario) =>
{
    Cliente nuevoCliente = new Cliente() {Usuario = usuario.Usuario, Apellido = usuario.Apellido, Contraseña = usuario.Contraseña, Dni = usuario.Dni, Nombre = usuario.Nombre, Email = usuario.Email};
    context.Clientes.Add(nuevoCliente);
    context.SaveChanges();
    return Results.Ok();
});

app.Run();