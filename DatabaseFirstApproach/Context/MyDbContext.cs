using System;
using System.Collections.Generic;
using DatabaseFirstApproach.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstApproach.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSubject> UserSubjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(
            "Server=localhost,1434;Database=vention-ef-tut;User Id=sa;Password=bazaTestowa1234;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).ValueGeneratedNever();
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.IdSubject);

            entity.ToTable("Subject");

            entity.Property(e => e.IdSubject).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.HasIndex(e => e.IdRole, "IX_User_IdRole");

            entity.Property(e => e.IdUser).ValueGeneratedNever();

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UserSubject>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdSubject });

            entity.ToTable("UserSubject");

            entity.HasIndex(e => e.IdSubject, "IX_UserSubject_IdSubject");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.UserSubjects)
                .HasForeignKey(d => d.IdSubject)
                .HasConstraintName("UserSubject_Subject_FK");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserSubjects)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("UserSubject_User_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}