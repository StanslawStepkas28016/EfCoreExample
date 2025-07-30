using DatabaseFirstApproach.Context;
using DatabaseFirstApproach.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstApproach;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        // We firstly design the database and then we scaffold it
        var myDbContext = new MyDbContext();

        // Add new Roles
        await myDbContext.Roles.AddAsync(new Role
        {
            IdRole = Guid.NewGuid(),
            Name = "Regular"
        });
        
        await myDbContext.SaveChangesAsync();

        // Read the available roles
        var li = await myDbContext.Roles.ToListAsync();
        Console.WriteLine(li.Count);
    }
}