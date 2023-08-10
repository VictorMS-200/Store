using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Models;

public class StoreWebContext : DbContext
{
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<ProductModel> Products { get; set; }

    public StoreWebContext(DbContextOptions<StoreWebContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryModel>().ToTable("Category");
        modelBuilder.Entity<ProductModel>().ToTable("Products");
    }
} 