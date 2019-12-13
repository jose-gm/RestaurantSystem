﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Monografico.Data;

namespace Monografico.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Monografico.Models.ApplicationUserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UsuarioRoles");
                });

            modelBuilder.Entity("Monografico.Models.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<string>("Imagen");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("Monografico.Models.Cuenta", b =>
                {
                    b.Property<int>("IdCuenta")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activa");

                    b.Property<int>("IdMesa");

                    b.HasKey("IdCuenta");

                    b.ToTable("Cuenta");
                });

            modelBuilder.Entity("Monografico.Models.Factura", b =>
                {
                    b.Property<int>("IdFactura")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Descuento");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdCuenta");

                    b.Property<decimal>("Monto");

                    b.HasKey("IdFactura");

                    b.ToTable("Factura");
                });

            modelBuilder.Entity("Monografico.Models.FacturaDetalle", b =>
                {
                    b.Property<int>("IdFacturaDetalle")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad");

                    b.Property<int>("IdFactura");

                    b.Property<int>("IdProducto");

                    b.Property<decimal>("Precio");

                    b.HasKey("IdFacturaDetalle");

                    b.HasIndex("IdFactura");

                    b.ToTable("FacturaDetalle");
                });

            modelBuilder.Entity("Monografico.Models.Ingrediente", b =>
                {
                    b.Property<int>("IdIngrediente")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Costo");

                    b.Property<string>("Descripcion");

                    b.Property<bool>("LlevaInventario");

                    b.HasKey("IdIngrediente");

                    b.ToTable("Ingrediente");
                });

            modelBuilder.Entity("Monografico.Models.Inventario", b =>
                {
                    b.Property<int>("IdInventario")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad");

                    b.Property<DateTime?>("FechaEntrada");

                    b.Property<int?>("IdIngrediente");

                    b.Property<int?>("IdProducto");

                    b.Property<string>("Unidad")
                        .HasMaxLength(10);

                    b.HasKey("IdInventario");

                    b.HasIndex("IdIngrediente")
                        .IsUnique()
                        .HasFilter("[IdIngrediente] IS NOT NULL");

                    b.HasIndex("IdProducto")
                        .IsUnique()
                        .HasFilter("[IdProducto] IS NOT NULL");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("Monografico.Models.Mesa", b =>
                {
                    b.Property<int>("IdMesa")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Asientos");

                    b.Property<string>("Descripcion");

                    b.Property<int>("IdZona");

                    b.HasKey("IdMesa");

                    b.HasIndex("IdZona");

                    b.ToTable("Mesa");
                });

            modelBuilder.Entity("Monografico.Models.Orden", b =>
                {
                    b.Property<int>("IdOrden")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Enviado");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdCuenta");

                    b.HasKey("IdOrden");

                    b.HasIndex("IdCuenta");

                    b.ToTable("Orden");
                });

            modelBuilder.Entity("Monografico.Models.OrdenDetalle", b =>
                {
                    b.Property<int>("IdOrdenDetalle")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad");

                    b.Property<int>("IdOrden");

                    b.Property<int>("IdProducto");

                    b.HasKey("IdOrdenDetalle");

                    b.HasIndex("IdOrden");

                    b.ToTable("OrdenDetalle");
                });

            modelBuilder.Entity("Monografico.Models.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(60);

                    b.Property<int?>("IdCategoria");

                    b.Property<string>("Imagen");

                    b.Property<bool>("LlevaIngredientes");

                    b.Property<bool>("LlevaInventario");

                    b.Property<decimal>("Precio");

                    b.HasKey("IdProducto");

                    b.HasIndex("IdCategoria");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("Monografico.Models.ProductoDetalle", b =>
                {
                    b.Property<int>("IdDetalle")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad");

                    b.Property<int>("IdIngrediente");

                    b.Property<int>("IdProducto");

                    b.HasKey("IdDetalle");

                    b.HasIndex("IdProducto");

                    b.ToTable("ProductoDetalle");
                });

            modelBuilder.Entity("Monografico.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("Monografico.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Apellido");

                    b.Property<string>("Cedula");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Direccion");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nombre");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("Sexo");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Monografico.Models.Zona", b =>
                {
                    b.Property<int>("IdZona")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.HasKey("IdZona");

                    b.ToTable("Zona");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Monografico.Models.Rol")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Monografico.Models.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Monografico.Models.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Monografico.Models.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Monografico.Models.ApplicationUserRole", b =>
                {
                    b.HasOne("Monografico.Models.Rol", "Role")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Monografico.Models.Usuario", "User")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Monografico.Models.FacturaDetalle", b =>
                {
                    b.HasOne("Monografico.Models.Factura")
                        .WithMany("Detalle")
                        .HasForeignKey("IdFactura")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Monografico.Models.Inventario", b =>
                {
                    b.HasOne("Monografico.Models.Ingrediente", "Ingrediente")
                        .WithOne("Inventario")
                        .HasForeignKey("Monografico.Models.Inventario", "IdIngrediente")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Monografico.Models.Producto", "Producto")
                        .WithOne("Inventario")
                        .HasForeignKey("Monografico.Models.Inventario", "IdProducto")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Monografico.Models.Mesa", b =>
                {
                    b.HasOne("Monografico.Models.Zona")
                        .WithMany("Mesas")
                        .HasForeignKey("IdZona")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Monografico.Models.Orden", b =>
                {
                    b.HasOne("Monografico.Models.Cuenta")
                        .WithMany("ordenes")
                        .HasForeignKey("IdCuenta")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Monografico.Models.OrdenDetalle", b =>
                {
                    b.HasOne("Monografico.Models.Orden")
                        .WithMany("Detalle")
                        .HasForeignKey("IdOrden")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Monografico.Models.Producto", b =>
                {
                    b.HasOne("Monografico.Models.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Monografico.Models.ProductoDetalle", b =>
                {
                    b.HasOne("Monografico.Models.Producto")
                        .WithMany("Detalle")
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
