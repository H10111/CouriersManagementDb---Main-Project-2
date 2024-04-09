using CouriersManagementDb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CouriersManagementDb.Areas.Identity.Data;

public class CouriersManagementDbContext : IdentityDbContext<IdentityUser>
{
    public CouriersManagementDbContext(DbContextOptions<CouriersManagementDbContext> options)
        : base(options)
    {
    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Tracking> Trackings { get; set; }
    public DbSet<Pallet> Pallets { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.Amount).HasPrecision(18, 2); // For example, this supports values up to 999,999,999,999,999.99


        });
        modelBuilder.Entity<Payment>()
                .HasOne(p => p.Shipment)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.ShipmentID)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
