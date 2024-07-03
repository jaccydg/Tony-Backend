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

                entity.Ignore(cs => cs.LastLog);
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
                    .HasMany(w => w.User)
                    .WithOne(u => u.Subscription)
                    .HasForeignKey(u => u.SubscriptionId);
            });

            modelBuilder.Entity<TopUpWallet>(entity =>
            {
                entity.HasKey(t => new { t.WalletId, t.Date });
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

            var charginStationList = new List<ChargingStation>
            {
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
            };

            modelBuilder.Entity<ChargingStation>().HasData(charginStationList);

            var subscription1 = new Subscription
            {
                Id = Guid.NewGuid(),
                PlaneName = Plane.Premium,
                MonthlyCost = 29.99m,
                MonthlyCredit = 100,
                CostKWh = 0.25
            };

            var subscription2 = new Subscription
            {
                Id = Guid.NewGuid(),
                PlaneName = Plane.Free,
                MonthlyCost = 0,
                MonthlyCredit = 0,
                CostKWh = 0.35
            };


            modelBuilder.Entity<Subscription>().HasData(subscription1, subscription2);

            var hasher = new PasswordHasher<ApplicationUser>();

            var user1 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "giulio@giulio.com",
                NormalizedUserName = "GIULIO@GIULIO.COM",
                Email = "giulio@giulio.com",
                NormalizedEmail = "GIULIO@GIULIO.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                SubscriptionId = subscription1.Id
            };
            user1.PasswordHash = hasher.HashPassword(user1, "Ciaociao35@");

            var user2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "a@a.com",
                NormalizedUserName = "A@A.COM",
                Email = "a@a.com",
                NormalizedEmail = "A@A.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                SubscriptionId = subscription2.Id
            };
            user2.PasswordHash = hasher.HashPassword(user2, "Password1!");

            modelBuilder.Entity<ApplicationUser>().HasData(user1, user2);

            var chargingSessions = new List<ChargingSession>
            {
                new ChargingSession
                {
                    Id = Guid.NewGuid(),
                    Status = ChargingSessionStatus.Ongoing,
                    StartingDate = DateTime.UtcNow.AddHours(-2),
                    UserId = user1.Id,
                    ChargingStationId = charginStationList[0].Id,
                    GatewayId = gatewayList[0].Id,
                    ChargingStationNumber = 1
                },
                new ChargingSession
                {
                    Id = Guid.NewGuid(),
                    Status = ChargingSessionStatus.Completed,
                    StartingDate = DateTime.UtcNow.AddDays(-1),
                    EndingDate = DateTime.UtcNow.AddDays(-1).AddHours(2),
                    FinalCost = 10.5m,
                    FinalConsuption = 25.5,
                    TotalTime = TimeSpan.FromHours(2),
                    CostKWh = 0.42m,
                    UserId = user1.Id,
                    ChargingStationId = charginStationList[1].Id,
                    GatewayId = gatewayList[1].Id,
                    ChargingStationNumber = 2
                },
                new ChargingSession
                {
                    Id = Guid.NewGuid(),
                    Status = ChargingSessionStatus.Ongoing,
                    StartingDate = DateTime.UtcNow.AddHours(-2),
                    UserId = user2.Id,
                    ChargingStationId = charginStationList[0].Id,
                    GatewayId = gatewayList[1].Id,
                    ChargingStationNumber = 1,
                },
                new ChargingSession
                {
                    Id = Guid.NewGuid(),
                    Status = ChargingSessionStatus.Completed,
                    StartingDate = DateTime.UtcNow.AddDays(-1),
                    EndingDate = DateTime.UtcNow.AddDays(-1).AddHours(2),
                    FinalCost = 10.5m,
                    FinalConsuption = 25.5,
                    TotalTime = TimeSpan.FromHours(2),
                    CostKWh = 0.42m,
                    UserId = user2.Id,
                    ChargingStationId = charginStationList[1].Id,
                    GatewayId = gatewayList[1].Id,
                    ChargingStationNumber = 2
                }
            };

            var chargingLogs = new List<ChargingLog>
            {
                new ChargingLog
                {
                    Date = DateTime.UtcNow.AddDays(-1).AddHours(-1),
                    Speed = 50,
                    Consumption = 12.5,
                    Cost = 5.25m,
                    Time = TimeSpan.FromMinutes(30),
                    ChargingSessionId = chargingSessions[0].Id.ToString(),
                    UserId = user1.Id
                },
                new ChargingLog
                {
                    Date = DateTime.UtcNow.AddDays(-2).AddHours(-1),
                    Speed = 50,
                    Consumption = 12.5,
                    Cost = 5.25m,
                    Time = TimeSpan.FromMinutes(30),
                    ChargingSessionId = chargingSessions[1].Id.ToString(),
                    UserId = user1.Id
                },
                new ChargingLog
                {
                    Date = DateTime.UtcNow.AddDays(-1).AddHours(-1),
                    Speed = 50,
                    Consumption = 12.5,
                    Cost = 5.25m,
                    Time = TimeSpan.FromMinutes(30),
                    ChargingSessionId = chargingSessions[2].Id.ToString(),
                    UserId = user2.Id
                },
                new ChargingLog
                {
                    Date = DateTime.UtcNow.AddDays(-2).AddHours(-1),
                    Speed = 50,
                    Consumption = 12.5,
                    Cost = 5.25m,
                    Time = TimeSpan.FromMinutes(30),
                    ChargingSessionId = chargingSessions[3].Id.ToString(),
                    UserId = user2.Id
                }
            };

            chargingSessions[0].LastLog = chargingLogs[0];
            chargingSessions[1].LastLog = chargingLogs[1];
            chargingSessions[2].LastLog = chargingLogs[2];
            chargingSessions[3].LastLog = chargingLogs[3];

            modelBuilder.Entity<ChargingSession>().HasData(chargingSessions);

            modelBuilder.Entity<Wallet>().HasData(
                new Wallet
                {
                    Id = Guid.NewGuid(),
                    SubscriptionCredit = 50,
                    PayPerUseCredit = 20,
                    UserId = user1.Id
                },
                new Wallet
                {
                    Id = Guid.NewGuid(),
                    SubscriptionCredit = 0,
                    PayPerUseCredit = 30,
                    UserId = user2.Id
                }
            );

            modelBuilder.Entity<TopUpWallet>().HasData(
                new TopUpWallet
                {
                    WalletId = Guid.NewGuid(),
                    Date = DateTime.UtcNow.AddDays(-5),
                    Amount = 20
                },
                new TopUpWallet
                {
                    WalletId = Guid.NewGuid(),
                    Date = DateTime.UtcNow.AddDays(-10),
                    Amount = 50
                }
            );

        }

    }

}
