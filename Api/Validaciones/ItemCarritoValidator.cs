using FluentValidation;
using Api.Funcionalidades.ItemCarritos;
using Biblioteca;

namespace Api.Validaciones;

public class ItemCarritoCommandDtoValidator : AbstractValidator<ItemCarritoCommandDto>
{
    private readonly AplicacionDbContext _context;

    public ItemCarritoCommandDtoValidator(AplicacionDbContext context)
    {
        _context = context;

        RuleFor(x => x.Cantidad)
            .NotEmpty().WithMessage("La cantidad es requerida")
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");

        RuleFor(x => x.idProducto)
            .NotEmpty().WithMessage("El ID del producto es requerido")
            .MustAsync(async (idProducto, cancellation) => {
                return await _context.Productos.FindAsync(idProducto) != null;
            }).WithMessage("El producto especificado no existe");

        RuleFor(x => x.Subtotal)
            .NotEmpty().WithMessage("El subtotal es requerido")
            .GreaterThan(0).WithMessage("El subtotal debe ser mayor a 0");
    }
} 