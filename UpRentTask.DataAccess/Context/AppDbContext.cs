using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UpRentTask.DataAccess.Entities;

namespace UpRentTask.DataAccess.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Version).HasDefaultValue(1);
            entity.Property(e => e.Visible).HasDefaultValue(true);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.RoleCreatedByUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Role_User_Created");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.RoleModifiedByUsers).HasConstraintName("FK_Role_User_Modified");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Version).HasDefaultValue(1);
            entity.Property(e => e.Visible).HasDefaultValue(true);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.InverseCreatedByUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_User_Created");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.InverseModifiedByUser).HasConstraintName("FK_User_User_Modified");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Version).HasDefaultValue(1);
            entity.Property(e => e.Visible).HasDefaultValue(true);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.UserRoleCreatedByUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_User_Created");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.UserRoleModifiedByUsers).HasConstraintName("FK_UserRole_User_Modified");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
