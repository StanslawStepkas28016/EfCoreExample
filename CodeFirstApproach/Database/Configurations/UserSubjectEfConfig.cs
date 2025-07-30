using EfCoreTut.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreTut.Configurations;

public class UserSubjectEfConfig : IEntityTypeConfiguration<UserSubject>
{
    public void Configure(EntityTypeBuilder<UserSubject> builder)
    {
        builder
            .HasKey(us => new { us.IdUser, us.IdSubject })
            .HasName("PK_UserSubject");

        builder
            .Property(us => us.Time)
            .IsRequired();

        builder
            .HasOne(us => us.Subject)
            .WithMany(s => s.UserSubjects)
            .HasForeignKey(us => us.IdSubject)
            .HasConstraintName("UserSubject_Subject_FK");

        builder
            .HasOne(us => us.User)
            .WithMany(u => u.UserSubjects)
            .HasForeignKey(us => us.IdUser)
            .HasConstraintName("UserSubject_User_FK");
        
        builder
            .ToTable("UserSubject");
    }
}