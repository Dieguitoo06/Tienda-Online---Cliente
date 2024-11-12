using FluentValidation;
using Api.Funcionalidaades.Carritos;
using Biblioteca;

namespace Api.Validaciones;

public class CarritoCommandDtoValidator : AbstractValidator<CarritoCommandoDto>
{
    public CarritoCommandDtoValidator()
    {
        RuleFor(x => x.Total)
            .GreaterThanOrEqualTo(0).WithMessage("El total no puede ser negativo");
    }
} 