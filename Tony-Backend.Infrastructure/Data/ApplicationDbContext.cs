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


            // SEED DATA

            modelBuilder.Entity<Gateway>().HasData(
                new Gateway
                {
                    Id = 1,
                    Name = "Gateway 1",
                    Latitude = 48.8566,
                    Longitude = 2.3522
                },
                new Gateway
                {
                    Id = 2,
                    Name = "Gateway 2",
                    Latitude = 48.8566,
                    Longitude = 2.3512
                },
                new Gateway
                {
                    Id = 3,
                    Name = "Gateway 3",
                    Latitude = 48.8566,
                    Longitude = 2.3589
                }) ;


            modelBuilder.Entity<ChargingStation>().HasData(
                new ChargingStation
                {
                    Number = 1,
                    GatewayId = 1
                },
                new ChargingStation
                {
                    Number = 2,
                    GatewayId = 1
                },
                new ChargingStation
                {
                    Number = 1,
                    GatewayId = 2
                },
                new ChargingStation
                {
                    Number = 2,
                    GatewayId = 2
                },
                new ChargingStation
                {
                    Number = 3,
                    GatewayId = 2
                },
                new ChargingStation
                {
                    Number = 1,
                    GatewayId = 3
                },
                new ChargingStation
                {
                    Number = 2,
                    GatewayId = 3
                },
                new ChargingStation
                {
                    Number = 3,
                    GatewayId = 3
                },
                new ChargingStation
                {
                    Number = 4,
                    GatewayId = 3
                });
        }

    }

}
