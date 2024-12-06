using FeriasTJBase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FeriasTJBase.Infra.Configurations
{
    public class PgDbContext : DbContext
    {
        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options) { }

        public DbSet<Ferias> Ferias { get; set; }
        public DbSet<Usufruto> Usufruto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ferias>()
                .HasKey(f => f.IdFerias);

            modelBuilder.Entity<Usufruto>()
                .HasKey(f => f.IdUsufruto);

            modelBuilder.Entity<Ferias>()
                .HasMany(b => b.Usufrutos)
                .WithOne(u => u.Ferias)
                .HasForeignKey(u => u.IdFerias);
        }
    }
}
