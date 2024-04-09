using CouriersManagementDb.Areas.Identity.Data; // Adjust namespace to your DbContext
using CouriersManagementDb.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

public static class DbInitializer
{
    public static void Initialize(CouriersManagementDbContext context, UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
    {
        context.Database.EnsureCreated();

        if (!context.Customers.Any())
        {
            var customers = new Customer[]
            {
                new Customer { Name = "Alice Wonderland", Address = "123 Fantasy Lane", Number = "555-0101" },
                new Customer { Name = "Bob Builder", Address = "456 Construction Ave", Number = "555-0202" },
                new Customer { Name = "Charlie Chocolate", Address = "789 Cocoa Blvd", Number = "555-0303" },
                new Customer { Name = "Dorothy Oz", Address = "101 Emerald City", Number = "555-0404" },
                new Customer { Name = "Eve Paradise", Address = "234 Eden Gardens", Number = "555-0505" },
                new Customer { Name = "Frankenstein Monster", Address = "567 Gothic Manor", Number = "555-0606" },
                new Customer { Name = "Goldilocks Bears", Address = "890 Porridge Pot Lane", Number = "555-0707" },
                new Customer { Name = "Hansel Gretel", Address = "123 Candy House Forest", Number = "555-0808" },
                new Customer { Name = "Icarus Wings", Address = "456 Sun Close", Number = "555-0909" },
                new Customer { Name = "Jack Beanstalk", Address = "789 Giant’s Castle", Number = "555-1010" }
            };

            foreach (var customer in customers)
            {
                context.Customers.Add(customer);
            }
            context.SaveChanges();
        }

        // Seed Couriers
        if (!context.Couriers.Any())
        {
            var couriers = new Courier[]
            {
                new Courier { Name = "John", Contact = "02108744563" },
                new Courier { Name = "Hasashi", Contact = "02108924532" },
                new Courier { Name = "Linda", Contact = "02108564321" },
                new Courier { Name = "Raj", Contact = "02108432156" },
                new Courier { Name = "Emily", Contact = "02108321564" },
                new Courier { Name = "Tony", Contact = "02108215634" },
                new Courier { Name = "Bruce", Contact = "02108156342" },
                new Courier { Name = "Natasha", Contact = "02108063421" },
                new Courier { Name = "Clint", Contact = "02107934215" },
                new Courier { Name = "Steve", Contact = "02107842153" }
            };

            foreach (var courier in couriers)
            {
                context.Couriers.Add(courier);
            }
            context.SaveChanges();
        }

        if (!context.Employees.Any())
        {
            var employees = new Employee[]
            {
                new Employee { Name = "Alice Johnson", Role = "Manager", ContactInfo = "alice.johnson@example.com" },
                new Employee { Name = "Bob Smith", Role = "Dispatcher", ContactInfo = "bob.smith@example.com" },
                new Employee { Name = "Carol Danvers", Role = "Driver", ContactInfo = "carol.danvers@example.com" },
                new Employee { Name = "Dave Banner", Role = "Loader", ContactInfo = "dave.banner@example.com" },
                // Add more predefined employees...
            };

            foreach (var employee in employees)
            {
                context.Employees.Add(employee);
            }
            context.SaveChanges();
        }

        // Seed Payments
        if (!context.Payments.Any())
        {
            var payments = new Payment[]
            {
             new Payment { ShipmentID = 1, CustomerID = 1, Timestamp = DateTime.Now.AddDays(-10), Amount = 100.00m },
                new Payment { ShipmentID = 2, CustomerID = 2, Timestamp = DateTime.Now.AddDays(-9), Amount = 200.00m },
                new Payment { ShipmentID = 3, CustomerID = 3, Timestamp = DateTime.Now.AddDays(-8), Amount = 150.00m },
                new Payment { ShipmentID = 4, CustomerID = 4, Timestamp = DateTime.Now.AddDays(-7), Amount = 250.00m },
                // Add more predefined payments as needed.
            };

            foreach (var payment in payments)
            {
                context.Payments.Add(payment);
            }
            context.SaveChanges();
        }

        // Seed Trackings
        if (!context.Trackings.Any())
        {
            var trackings = new Tracking[]
            {
                new Tracking { PackageID = 1, LocationID = 1, Status = "Dispatched", Timestamp = DateTime.Now.AddDays(-6) },
                new Tracking { PackageID = 2, LocationID = 2, Status = "In Transit", Timestamp = DateTime.Now.AddDays(-5) },
                new Tracking { PackageID = 3, LocationID = 3, Status = "Delivered", Timestamp = DateTime.Now.AddDays(-4) },
                new Tracking { PackageID = 4, LocationID = 4, Status = "Returned", Timestamp = DateTime.Now.AddDays(-3) },
                // Add more predefined trackings as needed.
            };

            foreach (var tracking in trackings)
            {
                context.Trackings.Add(tracking);
            }
            context.SaveChanges();
        }
        // Seed Pallets
        if (!context.Pallets.Any())
        {
            // First, ensure Couriers are seeded as Pallets rely on CourierID
            // ...

            // Seed Pallets with each Pallet linked to a Courier
            for (int i = 1; i <= 10; i++) // Assuming you have at least 10 couriers
            {
                var pallet = new Pallet { CourierID = i };
                context.Pallets.Add(pallet);
            }
            context.SaveChanges();

            // Seed Packages and assign them to Pallets
            foreach (var pallet in context.Pallets)
            {
                for (int j = 1; j <= 15; j++) // Each pallet will have 15 packages
                {
                    var package = new Package
                    {
                        Dimensions = $"{j}x{j}x{j}",
                        Contents = $"Contents {j}",
                        DeliveryStatus = "Ready for dispatch",
                        PalletID = pallet.PalletID
                    };
                    context.Packages.Add(package);
                }
            }
            context.SaveChanges();
        }
        if (!context.Packages.Any())
        {
            var packages = new Package[]
            {
                new Package { ShipmentID = 1, Dimensions = "10x10x10", Contents = "Books", DeliveryStatus = "Dispatched" },
                new Package { ShipmentID = 1, Dimensions = "5x5x5", Contents = "Electronics", DeliveryStatus = "In Transit" },
                new Package { ShipmentID = 1, Dimensions = "15x10x5", Contents = "Clothing", DeliveryStatus = "Delivered" },
                new Package { ShipmentID = 1, Dimensions = "20x20x15", Contents = "Furniture", DeliveryStatus = "Dispatched" },
                // ...more packages...
                new Package { ShipmentID = 1, Dimensions = "8x8x8", Contents = "Toys", DeliveryStatus = "Delivered" },
                // Assuming you have 15 packages in total
            };

            foreach (var package in packages)
            {
                context.Packages.Add(package);
            }
            context.SaveChanges();
        }
    }

    internal static void Initialize(CouriersManagementDbContext context)
    {
        throw new NotImplementedException();
    }
}