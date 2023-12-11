using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project_for_DB;

public partial class DoorContext : DbContext
{
    public DoorContext()
    {
    }

    public DoorContext(DbContextOptions<DoorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Audit> Audits { get; set; }

    public virtual DbSet<Departament> Departaments { get; set; }

    public virtual DbSet<Lock> Locks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=door;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("adress_pkey");

            entity.ToTable("adress");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Building).HasColumnName("building");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Audit>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("audit_pkey");

            entity.ToTable("audit");

            entity.Property(e => e.Number)
                .ValueGeneratedNever()
                .HasColumnName("number");
            entity.Property(e => e.Closed).HasColumnName("closed");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.IdDoor).HasColumnName("id_door");
            entity.Property(e => e.IdStreet).HasColumnName("id_street");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .HasColumnName("login");

            entity.HasOne(d => d.LoginNavigation).WithMany(p => p.Audits)
                .HasForeignKey(d => d.Login)
                .HasConstraintName("audit_login_fkey");

            entity.HasOne(d => d.Id).WithMany(p => p.Audits)
                .HasForeignKey(d => new { d.IdDoor, d.IdStreet })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("audit_id_door_id_street_fkey");
        });

        modelBuilder.Entity<Departament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("departament_pkey");

            entity.ToTable("departament");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
        });

        modelBuilder.Entity<Lock>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IdStreet }).HasName("lock_pkey");

            entity.ToTable("lock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdStreet).HasColumnName("id_street");
            entity.Property(e => e.Closed).HasColumnName("closed");
            entity.Property(e => e.Level).HasColumnName("level");

            entity.HasOne(d => d.IdStreetNavigation).WithMany(p => p.Locks)
                .HasForeignKey(d => d.IdStreet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lock_id_street_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .HasColumnName("login");
            entity.Property(e => e.DepartamentId).HasColumnName("departament_id");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Sname)
                .HasMaxLength(100)
                .HasColumnName("sname");

            entity.HasOne(d => d.Departament).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartamentId)
                .HasConstraintName("users_departament_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
