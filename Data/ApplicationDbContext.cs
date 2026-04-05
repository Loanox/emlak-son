using emlak_son.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace emlak_son.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Property> Properties => Set<Property>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>()
            .Property(c => c.CategoryName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Entity<Property>()
            .Property(p => p.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Entity<Property>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        builder.Entity<Property>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Properties)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Property>()
            .HasOne(p => p.AppUser)
            .WithMany(u => u.Properties)
            .HasForeignKey(p => p.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
