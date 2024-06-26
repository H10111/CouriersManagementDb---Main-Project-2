using CouriersManagementDb.Areas.Identity.Data; 
using CouriersManagementDb.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

public static class DbInitializer
{
    public static void Initialize(CouriersManagementDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Customers.Any())
        {
            return;
        }

        var customers = new Customer[]
        {
                new Customer { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Address = "123 Elm St", PhoneNumber = "123-456-7890" },
                new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Address = "456 Oak St", PhoneNumber = "987-654-3210" },
                new Customer { FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Address = "789 Pine St", PhoneNumber = "564-738-2910" },
                new Customer { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", Address = "321 Maple St", PhoneNumber = "102-456-7890" }
        };

        foreach (Customer c in customers)
        {
            context.Customers.Add(c);
        }


        context.SaveChanges();
        
    }


    
}
