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
                new Customer { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Address = "123 Elm St", PhoneNumber = "1234567890" },
                new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Address = "456 Oak St", PhoneNumber = "9876543210" },
                new Customer { FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Address = "789 Pine St", PhoneNumber = "5647382910" },
                new Customer { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", Address = "321 Maple St", PhoneNumber = "1024567890" }
        };

        foreach (Customer c in customers)
        {
            context.Customers.Add(c);
        }

        var couriers = new Courier[]
        {
            new Courier { FirstName = "lol", LastName = "Doe", Email = "johndoe@example.com", PhoneNumber = "1234567890", BaseLocation = "Auckland" },
            new Courier { FirstName = "bobb", LastName = "Smith", Email = "janesmith@example.com", PhoneNumber = "0987654321", BaseLocation = "Wellington" },
            new Courier { FirstName = "wonder", LastName = "Johnson", Email = "alicejohnson@example.com", PhoneNumber = "4561237890", BaseLocation = "Christchurch" },
            new Courier { FirstName = "dutch", LastName = "Brown", Email = "bobbrown@example.com", PhoneNumber = "3216549870", BaseLocation = "Hamilton" },
            new Courier { FirstName = "Maria", LastName = "Garcia", Email = "mariagarcia@example.com", PhoneNumber = "6422017890", BaseLocation = "Dunedin" },
            new Courier { FirstName = "James", LastName = "Wilson", Email = "jameswilson@example.com", PhoneNumber = "642302812", BaseLocation = "Napier" },
            new Courier { FirstName = "Emma", LastName = "Lee", Email = "emmalee@example.com", PhoneNumber = "6424031593", BaseLocation = "Tauranga" },
            new Courier { FirstName = "Oliver", LastName = "Taylor", Email = "olivertaylor@example.com", PhoneNumber = "6425047512", BaseLocation = "Palmerston North" },
            new Courier { FirstName = "Sophia", LastName = "Moore", Email = "sophiamoore@example.com", PhoneNumber = "6426053621", BaseLocation = "Nelson" },
            new Courier { FirstName = "Lucas", LastName = "Martin", Email = "lucasmartin@example.com", PhoneNumber = "6427061728", BaseLocation = "Rotorua" }


        };
              

        foreach (Courier c in couriers)
        {
            context.Couriers.Add(c);
        }
        var employees = new Employee[] 
        {
            new Employee { FirstName = "Jane", LastName = "Smith", Email = "janesmith@example.com", PhoneNumber = "0987654321", Role = "Assistant" },
            new Employee { FirstName = "Alice", LastName = "Johnson", Email = "alicejohnson@example.com", PhoneNumber = "1357924680", Role = "HR" },
            new Employee { FirstName = "Bob", LastName = "Brown", Email = "bobbrown@example.com", PhoneNumber = "2468013579", Role = "Sales" },
            new Employee { FirstName = "Charlie", LastName = "Murphy", Email = "charliemurphy@example.com", PhoneNumber = "3549276481", Role = "Developer" },
            new Employee { FirstName = "David", LastName = "Banner", Email = "davidbanner@example.com", PhoneNumber = "4627813549", Role = "Support" },
            new Employee { FirstName = "Eva", LastName = "Green", Email = "evagreen@example.com", PhoneNumber = "5718362940", Role = "Designer" },
            new Employee { FirstName = "Frank", LastName = "Lampard", Email = "franklampard@example.com", PhoneNumber = "6892743152", Role = "Engineer" },
            new Employee { FirstName = "Gina", LastName = "Linetti", Email = "ginalinetti@example.com", PhoneNumber = "7981324657", Role = "Marketing" },
            new Employee { FirstName = "Harry", LastName = "Potter", Email = "harrypotter@example.com", PhoneNumber = "9078563412", Role = "Consultant" }

        };

        foreach(Employee e in employees)
        {
            context.Employees.Add(e);
        }
        context.SaveChanges();
        
    }


    
}
