using EfCoreTut.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreTut.Configurations;

public class SubjectEfConfig : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder
            .HasKey(s => s.IdSubject)
            .HasName("PK_Subject");

        builder
            .Property(s => s.Description)
            .IsRequired();

        builder
            .ToTable("Subject");
    }
}