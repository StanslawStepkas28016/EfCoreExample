using EfCoreTut.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreTut.Configurations;

public class RoleEfConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasKey(r => r.IdRole)
            .HasName("PK_Role");

        builder
            .Property(r => r.Name)
            .IsRequired();

        builder
            .Property(r => r.Description)
            .IsRequired(false);
        
        builder
            .ToTable("Role");
    }
}