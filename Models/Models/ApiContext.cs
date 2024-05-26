using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Progra_II.Models;

public partial class ApiContext : DbContext
{
    public ApiContext()
    {
    }

    public ApiContext(DbContextOptions<ApiContext> options)
     : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Especialidad> Especialidades { get; set; }

    public virtual DbSet<EstadosCita> EstadosCitas { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Sucursal> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Cita");

            entity.ToTable("citas");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Lugar)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Especialidad");

            entity.ToTable("especialidades");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadosCita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EstadoCita");

            entity.ToTable("estadosCitas");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NombreEstado)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Rol");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NombreRol)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Sucursal");

            entity.ToTable("sucursales");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreSucursal)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Usuario");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
