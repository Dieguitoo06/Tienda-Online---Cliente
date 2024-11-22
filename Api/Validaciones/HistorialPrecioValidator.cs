using FluentValidation;
using Api.Funcionalidades.HistorialPrecio;

namespace Api.Validaciones
{
    public class HistorialPrecioCommandDtoValidator : AbstractValidator<HistorialPrecioCommandDto>
    {
        public HistorialPrecioCommandDtoValidator()
        {
            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a 0");

            RuleFor(x => x.Moneda)
                .NotEmpty().WithMessage("La moneda es requerida")
                .Length(3).WithMessage("La moneda debe tener exactamente 3 caracteres"); // Ejemplo: "USD", "EUR"

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha es requerida");

            RuleFor(x => x.idProducto)
                .GreaterThan(0).WithMessage("El ID del producto es requerido y debe ser mayor a 0");
        }
    }
}
