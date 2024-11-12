using FluentValidation;
using Api.Funcionalidaades.Productos;
using Microsoft.EntityFrameworkCore;
using Biblioteca;

namespace Api.Validaciones;

public class ProductoCommandDtoValidator : AbstractValidator<ProductosCommandDto>
{
    private readonly AplicacionDbContext _context;

    public ProductoCommandDtoValidator(AplicacionDbContext context)
    {
        _context = context;

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido")
            .Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 caracteres");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es requerida")
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres");

        RuleFor(x => x.PrecioUnitario)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");

        RuleFor(x => x.idCategoria)
            .MustAsync(async (id, cancellation) => {
                return await _context.Categorias.FindAsync(id) != null;
            }).WithMessage("La categoría especificada no existe");
    }
} 