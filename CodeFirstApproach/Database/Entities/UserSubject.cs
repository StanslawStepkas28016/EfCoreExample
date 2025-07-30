namespace EfCoreTut.Entities;

public class UserSubject
{
    public Guid IdUser { get; set; }
    public User User { get; set; }

    public Guid IdSubject { get; set; }
    public Subject Subject { get; set; }

    public DateTime Time { get; set; }
}