using Bernhoeft.GRT.Teste.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.Teste.Infra.Data.Context
{
    public class AvisoDbContext : DbContext
    {
        public AvisoDbContext(DbContextOptions<AvisoDbContext> options)
            : base(options)
        {
        }

        public DbSet<AvisoEntity> Avisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Mappings.AvisoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}