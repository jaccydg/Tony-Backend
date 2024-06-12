using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<ChargingStation> ChargingStations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite primary key
            modelBuilder.Entity<ChargingStation>()
                .HasKey(cs => new { cs.Number, cs.GatewayId });

            // Define relation between ChargingStation and Gateway
            modelBuilder.Entity<ChargingStation>()
                .HasOne(c => c.Gateway)
                .WithMany(g => g.ChargingStations)
                .HasForeignKey(c => c.GatewayId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure GeoCoordinate as a value object
            modelBuilder.Entity<Gateway>(entity =>
            {
                entity.OwnsOne(g => g.Location);
            });
        }
    }

}
