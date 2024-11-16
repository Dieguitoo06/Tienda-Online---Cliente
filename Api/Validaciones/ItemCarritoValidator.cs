using FluentValidation;
using Api.Funcionalidades.ItemCarritos;
using Microsoft.EntityFrameworkCore;
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
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0")
            .MustAsync(async (model, cantidad, cancellation) => {
                var producto = await _context.Productos.FindAsync(model.idProducto);
                return producto != null && producto.Stock >= cantidad;
            }).WithMessage("No hay suficiente stock disponible");

        RuleFor(x => x.idProducto)
            .NotEmpty().WithMessage("El ID del producto es requerido")
            .MustAsync(async (idProducto, cancellation) => {
                return await _context.Productos.AnyAsync(p => p.idProducto == idProducto);
            }).WithMessage("El producto especificado no existe");
    }
} 