using Biblioteca;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Api.Funcionalidaades.Clientes;

public interface IClienteService
{
    Task<List<UsuariosQueryDto>> GetClientes();
    Task<string> CreateCliente(UsuariosCommandDto clienteDto);
    Task<UsuariosQueryDto?> GetClienteByDni(int dni);
    Task<string> UpdateCliente(int dni, UsuariosCommandDto clienteDto);
}

public class ClienteService : IClienteService
{
    private readonly AplicacionDbContext _context;
    private readonly PasswordHasher<Cliente> _passwordHasher;

    public ClienteService(AplicacionDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<Cliente>();
    }

    public async Task<List<UsuariosQueryDto>> GetClientes()
    {
        return await _context.Clientes
            .Select(c => new UsuariosQueryDto
            {
                Dni = c.Dni,
                Usuario = c.Usuario
            })
            .ToListAsync();
    }

    public async Task<string> CreateCliente(UsuariosCommandDto clienteDto)
    {
        var cliente = new Cliente
        {
            Dni = clienteDto.Dni,
            Nombre = clienteDto.Nombre,
            Apellido = clienteDto.Apellido,
            Email = clienteDto.Email,
            Usuario = clienteDto.Usuario
        };

        cliente.Contraseña = _passwordHasher.HashPassword(cliente, clienteDto.Contraseña);

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return "Cliente creado exitosamente";
    }

    public async Task<UsuariosQueryDto?> GetClienteByDni(int dni)
    {
        var cliente = await _context.Clientes.FindAsync(dni);

        if (cliente == null)
            return null;

        return new UsuariosQueryDto
        {
            Dni = cliente.Dni,
            Usuario = cliente.Usuario
        };
    }

    public async Task<string> UpdateCliente(int dni, UsuariosCommandDto clienteDto)
    {
        var clienteExistente = await _context.Clientes.FindAsync(dni);
        if (clienteExistente == null)
        {
            return "Cliente no encontrado";
        }

        clienteExistente.Nombre = clienteDto.Nombre;
        clienteExistente.Apellido = clienteDto.Apellido;
        clienteExistente.Email = clienteDto.Email;
        clienteExistente.Usuario = clienteDto.Usuario;

        if (!string.IsNullOrEmpty(clienteDto.Contraseña))
        {
            clienteExistente.Contraseña = _passwordHasher.HashPassword(clienteExistente, clienteDto.Contraseña);
        }

        await _context.SaveChangesAsync();
        return "Cliente actualizado exitosamente";
    }
}