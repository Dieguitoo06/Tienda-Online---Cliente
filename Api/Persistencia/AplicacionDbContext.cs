using Biblioteca;
using Microsoft.EntityFrameworkCore;

public class AplicacionDbContext : DbContext
{
    public AplicacionDbContext(DbContextOptions<AplicacionDbContext> opciones)
        : base(opciones)
    {
    }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Producto> Productos { get ; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<ItemCarrito> ItemCarritos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}