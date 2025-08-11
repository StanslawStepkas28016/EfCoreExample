using EfCoreTut.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfCoreTut;

public class MyContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<UserSubject> UserSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyContext).Assembly);


    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("myAppSettings.json")
            .Build();

        var section = config.GetSection("ConnectionStrings");

        optionsBuilder.UseSqlServer(section["DevDB"]);
    }
}