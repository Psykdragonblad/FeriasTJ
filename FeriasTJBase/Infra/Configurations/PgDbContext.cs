using FeriasTJ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FeriasTJBase.Infra.Configurations
{
    public class PgDbContext : DbContext
    {
        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options) { }

        public DbSet<Ferias> Ferias { get; set; }
        public DbSet<Ferias> Usufruto { get; set; }
    }
}
