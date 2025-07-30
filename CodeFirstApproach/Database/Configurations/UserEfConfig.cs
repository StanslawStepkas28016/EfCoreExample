using EfCoreTut.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreTut.Configurations;

public class UserEfConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(u => u.IdUser)
            .HasName("PK_User");

        builder
            .Property(u => u.Name)
            .IsRequired();

        builder
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.IdRole)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .ToTable("User");
    }
}