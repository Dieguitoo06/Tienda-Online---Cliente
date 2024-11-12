using FluentValidation;
using Api.Funcionalidades.Clientes.Categoria;
using Biblioteca;

namespace Api.Validaciones;

public class CategoriaCommandDtoValidator : AbstractValidator<CategoriaCommandDto>
{
    public CategoriaCommandDtoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido")
            .Length(2, 50).WithMessage("El nombre debe tener entre 2 y 50 caracteres");
    }
} 