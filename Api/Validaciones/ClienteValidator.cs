using FluentValidation;
using Api.Funcionalidaades.Clientes;
using Microsoft.EntityFrameworkCore;
using Biblioteca;

namespace Api.Validaciones;

public class UsuariosCommandDtoValidator : AbstractValidator<UsuariosCommandDto>
{
    private readonly AplicacionDbContext _context;

    public UsuariosCommandDtoValidator(AplicacionDbContext context)
    {
        _context = context;

        RuleFor(x => x.Dni)
            .NotEmpty().WithMessage("El DNI es requerido")
            .GreaterThan(0).WithMessage("El DNI debe ser mayor a 0")
            .MustAsync(async (dni, cancellation) => {
                return !await _context.Clientes.AnyAsync(c => c.Dni == dni);
            }).WithMessage("El DNI ya está registrado");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido")
            .Length(2, 50).WithMessage("El nombre debe tener entre 2 y 50 caracteres");

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El apellido es requerido")
            .Length(2, 50).WithMessage("El apellido debe tener entre 2 y 50 caracteres");

        RuleFor(x => x.Usuario)
            .NotEmpty().WithMessage("El usuario es requerido")
            .Length(4, 20).WithMessage("El usuario debe tener entre 4 y 20 caracteres")
            .MustAsync(async (usuario, cancellation) => {
                return !await _context.Clientes.AnyAsync(c => c.Usuario == usuario);
            }).WithMessage("El nombre de usuario ya está en uso");

        RuleFor(x => x.Contraseña)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres")
            .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una mayúscula")
            .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El email no tiene un formato válido")
            .MustAsync(async (email, cancellation) => {
                return !await _context.Clientes.AnyAsync(c => c.Email == email);
            }).WithMessage("El email ya está registrado");
    }
} 