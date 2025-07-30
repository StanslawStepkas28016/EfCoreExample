using EfCoreTut;
using EfCoreTut.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

internal sealed class Program
{
    public static async Task Main(string[] args)
    {
        MyContext context = new MyContext();

        // await OneToMany(myContext);
        // await ManyToMany(context);
        await TransactionExample(context);
    }

    private static async Task TransactionExample(MyContext context)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            // Perform operations which cause a problem,
            // e.g. insert a user whose IdUser was already used before
            var user = await context
                .Users
                .Where(u => u.Name == "John")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            await context
                .Users
                .AddAsync(new User
                {
                    IdUser = user!.IdUser, Name = "Alice", IdRole = Guid.Parse("7c35865c-a3ad-47b3-94fb-05f4c1392522")
                });

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();

            Console.WriteLine(
                $"An exception has occurred, the transaction was rolled back! \n Message: {e.InnerException!.Message}");
        }
    }

    private static async Task ManyToMany(MyContext context)
    {
        // Add a subject
        EntityEntry<Subject> entry = await context
            .Subjects
            .AddAsync(new Subject { IdSubject = Guid.NewGuid(), Description = "Test subject" });

        // Fetch a user
        User? user = await context
            .Users
            .FirstOrDefaultAsync(u => u.Name == "John");

        if (user == null)
        {
            throw new ArgumentException("No user found!");
        }

        // Add a new UserSubject
        await context
            .UserSubjects
            .AddAsync(new UserSubject
                { IdSubject = entry.Entity.IdSubject, IdUser = user.IdUser, Time = DateTime.Now });

        await context.SaveChangesAsync();
    }

    private static async Task OneToMany(MyContext context)
    {
        // See if there are any users in the database - no because we did not seed it
        bool anyAsync = await context.Users.AnyAsync();
        Console.WriteLine(anyAsync);

        Console.WriteLine("A - Add records, D - Delete records");
        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

        if (consoleKeyInfo.Key == ConsoleKey.A)
        {
            await AddRoleAndUser(context);
        }

        if (consoleKeyInfo.Key == ConsoleKey.D)
        {
            await DeleteRoleAndUser(context);
        }

        Console.WriteLine();

        // Fetch data
        List<User> userList = await context.Users.ToListAsync();

        if (userList.Count == 0)
        {
            Console.WriteLine("List empty");
        }
        else
        {
            userList.ForEach(Console.WriteLine);
        }
    }

    private static async Task DeleteRoleAndUser(MyContext context)
    {
        // Not saving changes as this saves changes immediately
        await context.Users.ExecuteDeleteAsync();
        await context.Roles.ExecuteDeleteAsync();
    }

    private static async Task AddRoleAndUser(MyContext context)
    {
        // Add a Role
        Role adminRole = new Role { IdRole = Guid.NewGuid(), Name = "Admin" };
        await context.Roles.AddAsync(adminRole);

        // Add a User  
        User user = new User { IdUser = Guid.NewGuid(), Name = "John", IdRole = adminRole.IdRole };
        await context.Users.AddAsync(user);

        await context.SaveChangesAsync();
    }
}