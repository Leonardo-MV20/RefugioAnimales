using System.Data.Entity;
using RefugioAnimal.Domain.Entities;

namespace RefugioAnimal.Infrastructure.Data
{
    public class RefugioContext : DbContext
    {
        public RefugioContext()
            : base("name=RefugioConnection")
        {
        }

        public DbSet<Mascota> Mascotas { get; set; }

        public DbSet<HistorialCuidado> HistorialCuidados { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistorialCuidado>()
                .ToTable("HistorialCuidados");
            modelBuilder.Entity<HistorialCuidado>()
                .HasRequired(h => h.Mascota)
                .WithMany(m => m.HistorialCuidados)
                .HasForeignKey(h => h.MascotaId)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}