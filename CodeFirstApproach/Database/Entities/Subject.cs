namespace EfCoreTut.Entities;

public class Subject
{
    public Guid IdSubject { get; set; }
    public string Description { get; set; } = null!;

    // References
    public ICollection<UserSubject> UserSubjects { get; set; }
}