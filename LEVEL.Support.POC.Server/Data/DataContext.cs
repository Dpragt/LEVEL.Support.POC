using LEVEL.Support.POC.Server.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LEVEL.Support.POC.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Melding> Meldingen { get; set; }
        public DbSet<Oplossing> Oplossingen { get; set; }
        public DbSet<GekoppeldeMelding> GekoppeldeMeldingen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Oplossing>()
                .HasOne(o => o.Melding)
                .WithMany(m => m.Oplossingen)
                .HasForeignKey(o => o.MeldingId);

            modelBuilder.Entity<GekoppeldeMelding>()
                .HasOne(g => g.Melding)
                .WithMany(m => m.GekoppeldeMeldingen)
                .HasForeignKey(g => g.MeldingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GekoppeldeMelding>()
                .HasOne(g => g.Gekoppeld)
                .WithMany()
                .HasForeignKey(g => g.GekoppeldeMeldingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
