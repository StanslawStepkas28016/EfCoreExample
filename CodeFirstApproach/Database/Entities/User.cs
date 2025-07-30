namespace EfCoreTut.Entities;

public class User
{
    public Guid IdUser { get; set; }
    public string Name { get; set; } = null!;

    // References
    public Guid IdRole { get; set; }
    public Role Role { get; set; }

    public ICollection<UserSubject> UserSubjects { get; set; }

    public override string ToString()
    {
        return $"{nameof(IdUser)}: {IdUser}, {nameof(Name)}: {Name}, {nameof(IdRole)}: {IdRole}";
    }
}