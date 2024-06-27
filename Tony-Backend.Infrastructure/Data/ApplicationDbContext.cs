using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tony_Backend.Shared.Entities;
using System.Text.Json.Serialization;

namespace Tony_Backend.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<ChargingSession> ChargingSessions { get; set; } 
        public DbSet<ChargingStation> ChargingStations { get; set; }
        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<TopUpWallet> TopUpWallets { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChargingSession>(entity =>
            {
                entity
                    .HasKey(cs =>  cs.Id);
                entity
                    .Property(cs => cs.Id).ValueGeneratedNever();

                entity 
                    .HasOne(c => c.User)
                    .WithMany(u => u.ChargingSessions)
                    .HasForeignKey(c => c.UserId);

                entity
                    .Property(cs => cs.Status)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => (ChargingSessionStatus)Enum.Parse(typeof(ChargingSessionStatus), v));

                entity
                    .Property(cs => cs.UserId)
                    .IsRequired();

                entity
                    .Property(cs => cs.ChargingStationNumber)
                    .IsRequired();

                entity
                    .Property(cs => cs.GatewayId)
                    .IsRequired();
            });

            modelBuilder.Entity<ChargingStation>(entity =>
            {
                entity
                    .HasKey(cs => cs.Id);
                entity
                    .Property(cs => cs.Id).ValueGeneratedNever();

                // Define relation between ChargingStation and Gateway
                entity
                    .HasOne(c => c.Gateway)
                    .WithMany(g => g.ChargingStations)
                    .HasForeignKey(c => c.GatewayId);
                // .OnDelete(DeleteBehavior.Cascade); // set by default

                entity
                    .HasMany(c => c.ChargingSessions)
                    .WithOne(cs => cs.ChargingStation)
                    .HasForeignKey(cs => cs.ChargingStationId);

                // Define Status required
                entity.Property(b => b.Status)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => (ChargingStationStatus)Enum.Parse(typeof(ChargingStationStatus), v));
            });

            modelBuilder.Entity<Gateway>(entity =>
            {
                entity
                    .HasKey(g => g.Id);
                entity
                    .Property(cs => cs.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity
                    .HasKey(s => s.Id );
                entity
                    .Property(cs => cs.Id).ValueGeneratedNever();
                entity
                    .HasOne(w => w.User)
                    .WithOne(u => u.Subscription)
                    .HasForeignKey<Subscription>(w => w.UserId);
            });

            modelBuilder.Entity<TopUpWallet>(entity =>
            {
                entity.HasNoKey();

                entity.Property(t => t.WalletId).IsRequired();
                entity.Property(t => t.Amount).IsRequired();
                entity.Property(t => t.Date).IsRequired();
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity
                    .HasKey(w => w.Id );
                entity
                    .Property(cs => cs.Id).ValueGeneratedNever();
                entity
                    .HasOne(w => w.User)
                    .WithOne(u => u.Wallet)
                    .HasForeignKey<Wallet>(w => w.UserId);
            });


            // SEED DATA
            var gatewayList = new List<Gateway>
            {
                new Gateway { Id = Guid.NewGuid(), Name = "Consorzio Universitario", Latitude = 45.951543, Longitude = 12.680627 },
                new Gateway { Id = Guid.NewGuid(), Name = "Aldi - Pordenone", Latitude = 45.953619, Longitude = 12.687382 },
                new Gateway { Id = Guid.NewGuid(), Name = "Naonis Gym", Latitude = 45.953282, Longitude = 12.672556 },
                new Gateway { Id = Guid.NewGuid(), Name = "Poste Italiane", Latitude = 45.951200, Longitude = 12.675172 }
            };

            modelBuilder.Entity<Gateway>()
                .HasData(gatewayList);

            modelBuilder.Entity<ChargingStation>().HasData(
                new ChargingStation { Id = Guid.NewGuid(), Number = 1, GatewayId = gatewayList[0].Id, Status = ChargingStationStatus.Free },
                new ChargingStation { Id = Guid.NewGuid(), Number = 2, GatewayId = gatewayList[0].Id, Status = ChargingStationStatus.Free },
                new ChargingStation { Id = Guid.NewGuid(), Number = 1, GatewayId = gatewayList[1].Id, Status = ChargingStationStatus.Free },
                new ChargingStation { Id = Guid.NewGuid(), Number = 2, GatewayId = gatewayList[1].Id, Status = ChargingStationStatus.Free },
                new ChargingStation { Id = Guid.NewGuid(), Number = 3, GatewayId = gatewayList[1].Id, Status = ChargingStationStatus.Free },
                new ChargingStation { Id = Guid.NewGuid(), Number = 1, GatewayId = gatewayList[2].Id, Status = ChargingStationStatus.Free },
                new ChargingStation { Id = Guid.NewGuid(), Number = 2, GatewayId = gatewayList[2].Id, Status = ChargingStationStatus.Free },
                new ChargingStation { Id = Guid.NewGuid(), Number = 3, GatewayId = gatewayList[2].Id, Status = ChargingStationStatus.Free },
                new ChargingStation { Id = Guid.NewGuid(), Number = 4, GatewayId = gatewayList[2].Id, Status = ChargingStationStatus.Free },
                new ChargingStation { Id = Guid.NewGuid(), Number = 1, GatewayId = gatewayList[3].Id, Status = ChargingStationStatus.Free }
            );

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Email = "giulio@giulio.com", PasswordHash = "Ciaociao35@".GetHashCode().ToString() },
                new ApplicationUser { Email = "a@a.com", PasswordHash = "Password1!".GetHashCode().ToString() }
            );

        }

    }

}
