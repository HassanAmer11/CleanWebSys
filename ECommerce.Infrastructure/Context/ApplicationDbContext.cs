using ECommerce.Core.Const;
using ECommerce.Core.Entities;
using ECommerce.Core.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<ProductLocation>()
    //        .HasKey(sl => new { sl.ProductId, sl.GovernorateId });

    //    modelBuilder.Entity<ProductLocation>()
    //        .HasOne(sl => sl.Product)
    //        .WithMany(s => s.ProductLocations)
    //        .HasForeignKey(sl => sl.ProductId);

    //    modelBuilder.Entity<ProductLocation>()
    //        .HasOne(sl => sl.Governorate)
    //        .WithMany(l => l.ProductLocations)
    //        .HasForeignKey(sl => sl.GovernorateId);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite key for ProductLocation
        modelBuilder.Entity<ProductLocation>()
            .HasKey(pl => new { pl.ProductId, pl.GovernorateId });

        modelBuilder.Entity<ProductLocation>()
            .HasOne(pl => pl.Product)
            .WithMany(p => p.ProductLocations)
            .HasForeignKey(pl => pl.ProductId);

        modelBuilder.Entity<ProductLocation>()
            .HasOne(pl => pl.Governorate)
            .WithMany(g => g.ProductLocations)
            .HasForeignKey(pl => pl.GovernorateId);
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Governorate> Governorates { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<ProductLocation> ProductLocations { get; set; }

}
