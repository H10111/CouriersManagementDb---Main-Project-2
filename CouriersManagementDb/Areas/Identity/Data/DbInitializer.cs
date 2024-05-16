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
            new Customer {CustomerName = "John Doe", CustomerAddress = "123 Elm Street", CustomerNumber = "1234567890" },
            new Customer {CustomerName = "Jane Smith", CustomerAddress = "456 Oak Road", CustomerNumber = "1987654321" },
            new Customer {CustomerName = "Alice Johnson", CustomerAddress = "789 Pine Lane", CustomerNumber = "1346798520" },
            new Customer {CustomerName = "Bob Brown", CustomerAddress = "321 Maple Ave", CustomerNumber = "1648392746" },
            new Customer {CustomerName = "Jamal", CustomerAddress="123 Hippo lake", CustomerNumber="12345677901"}
        };

        foreach (Customer c in customers)
        {
            context.Customers.Add(c);
        }


        context.SaveChanges();
        
    }


    
}
