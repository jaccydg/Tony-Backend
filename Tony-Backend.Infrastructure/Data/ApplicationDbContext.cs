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
                new Gateway { Id = 1, Name = "Consorzio Universitario", Latitude = 45.951543, Longitude = 12.680627 },
                new Gateway { Id = 2, Name = "Aldi - Pordenone", Latitude = 45.953619, Longitude = 12.687382 },
                new Gateway { Id = 3, Name = "Naonis Gym", Latitude = 45.953282, Longitude = 12.672556 },
                new Gateway { Id = 4, Name = "Poste Italiane", Latitude = 45.951200, Longitude = 12.675172 });

            modelBuilder.Entity<ChargingStation>().HasData(
                new ChargingStation { Number = 1, GatewayId = 1 },
                new ChargingStation { Number = 2, GatewayId = 1 },
                new ChargingStation { Number = 1, GatewayId = 2 },
                new ChargingStation { Number = 2, GatewayId = 2 },
                new ChargingStation { Number = 3, GatewayId = 2 },
                new ChargingStation { Number = 1, GatewayId = 3 },
                new ChargingStation { Number = 2, GatewayId = 3 },
                new ChargingStation { Number = 3, GatewayId = 3 },
                new ChargingStation { Number = 4, GatewayId = 3 });
        }

    }

}
