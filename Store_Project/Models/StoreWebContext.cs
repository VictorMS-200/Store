using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Store.Models;

namespace Store.Models;

public class StoreWebContext : DbContext
{
    public DbSet<CategoryModel> Categories { get; set; }

    public DbSet<ProductModel> Products { get; set; }

    public DbSet<CustomerModel> Customers { get; set; }

    public DbSet<OrderModel> Order { get; set; }

    public DbSet<ItemOrderModel> ItemOrder { get; set; }

    public StoreWebContext(DbContextOptions<StoreWebContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryModel>().ToTable("Category");
        modelBuilder.Entity<ProductModel>().ToTable("Products");
        modelBuilder.Entity<CustomerModel>()
            .OwnsMany(c => c.Addresses, e =>
            {
                e.WithOwner().HasForeignKey("IdUser");
                e.HasKey("IdAddress", "IdUser");
            });

        modelBuilder.Entity<UserModel>().Property(u => u.RegistrationDate)
            .HasDefaultValueSql("datetime('now', 'localtime', 'start of day')")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        modelBuilder.Entity<ProductModel>().Property(p => p.Stock)
            .HasDefaultValue(0);

        modelBuilder.Entity<OrderModel>()
            .OwnsOne(o => o.DeliveryAddress, a =>
            {
                a.Ignore(p => p.IdAddress);
                a.Ignore(p => p.Selected);
                a.ToTable("Order");
            });

        modelBuilder.Entity<ItemOrderModel>()
            .HasKey(ip => new { ip.IdOrder, ip.IdProduct });
    }
} 