using ApiParaPWA.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiParaPWA.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(product => product.Price).HasPrecision(10, 2);
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Cuaderno", Description = "Cuaderno de 100 hojas", Price = 45.00m, Stock = 20 },
            new Product { Id = 2, Name = "Lápiz", Description = "Lápiz de grafito", Price = 8.50m, Stock = 100 },
            new Product { Id = 3, Name = "Mochila", Description = "Mochila escolar", Price = 399.00m, Stock = 12 });
    }
}
