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
    public DbSet<Favorite> Favorites => Set<Favorite>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<PropertyImage> PropertyImages => Set<PropertyImage>();

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

        builder.Entity<PropertyImage>()
            .HasOne(pi => pi.Property)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Favorite>()
            .HasIndex(f => new { f.AppUserId, f.PropertyId })
            .IsUnique();

        builder.Entity<Favorite>()
            .HasOne(f => f.AppUser)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Favorite>()
            .HasOne(f => f.Property)
            .WithMany(p => p.Favorites)
            .HasForeignKey(f => f.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .Property(c => c.Content)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Entity<Comment>()
            .HasOne(c => c.AppUser)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comment>()
            .HasOne(c => c.Property)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
