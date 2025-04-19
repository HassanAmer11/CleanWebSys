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
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Governorate> Governorates { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }


}
