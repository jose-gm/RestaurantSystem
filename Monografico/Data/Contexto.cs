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
    public class Contexto : IdentityDbContext<Usuario,Rol,int, IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.Property(e => e.Id);

            });

            builder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");
                entity.Property(e => e.Id);

            });
        }

    }
}
