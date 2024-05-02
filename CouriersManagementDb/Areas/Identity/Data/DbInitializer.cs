using CouriersManagementDb.Areas.Identity.Data; // Adjust namespace to your DbContext
using CouriersManagementDb.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

public static class DbInitializer
{
    public static void Initialize(CouriersManagementDbContext context, UserManager<ApplicationUser> userManager)
    {
        context.Database.EnsureCreated();
    

       
        context.SaveChanges();
        
    }

    internal static void Initialize(CouriersManagementDbContext context)
    {
        throw new NotImplementedException();
    }
}
