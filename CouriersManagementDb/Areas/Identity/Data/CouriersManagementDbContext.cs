using CouriersManagementDb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CouriersManagementDb.Areas.Identity.Data;

public class CouriersManagementDbContext : IdentityDbContext<ApplicationUser>
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
                .HasOne(p => p.Shipments)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.ShipmentID)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Customers)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Package>()
        .HasOne(p => p.Shipments)
        .WithMany(s => s.Packages)
        .HasForeignKey(p => p.ShipmentID)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>().Ignore(p => p.DeliveryStatus);
        modelBuilder.Entity<Payment>().Ignore(p => p.Contents);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
