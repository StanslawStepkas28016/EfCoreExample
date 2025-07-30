namespace EfCoreTut.Entities;

public class Role
{
    public Guid IdRole { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<User> Users;

    public override string ToString()
    {
        return $"{nameof(Users)}: {Users}, {nameof(IdRole)}: {IdRole}, {nameof(Name)}: {Name}";
    }
}