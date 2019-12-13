using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Data
{
    public class Contexto : IdentityDbContext<Usuario,Rol,int,
                            IdentityUserClaim<int>, ApplicationUserRole, 
                            IdentityUserLogin<int>, IdentityRoleClaim<int>, 
                            IdentityUserToken<int>>
    {
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Zona> Zona { get; set; }
        public DbSet<Cuenta> Cuenta { get; set; }
        public DbSet<Orden> Orden { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.HasMany(e => e.UsuarioRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Rol>(entity =>
            {
                entity.ToTable(name: "Rol");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.HasMany(e => e.UsuarioRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

            });

            builder.Entity<ApplicationUserRole>(entity =>
            {
                entity.ToTable("UsuarioRoles");
            });

            builder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.IdCategoria);
                entity.HasMany(x => x.Productos)
                    .WithOne(p => p.Categoria)
                    .HasForeignKey(pp => pp.IdCategoria)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Producto>(entity =>
            {
                entity.HasKey(p => p.IdProducto);
                entity.HasOne(x => x.Inventario)
                .WithOne(c => c.Producto)
                .HasForeignKey<Inventario>(s => s.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);
            });
                       
            builder.Entity<Ingrediente>(entity =>
            {
                entity.HasKey(p => p.IdIngrediente);
                entity.HasOne(x => x.Inventario)
                .WithOne(c => c.Ingrediente)
                .HasForeignKey<Inventario>(s => s.IdIngrediente)
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(c => c.IdCuenta);
                entity.HasMany(x => x.Ordenes)
                    .WithOne(p => p.Cuenta)
                    .HasForeignKey(pp => pp.IdCuenta)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        }

    }
}
