﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tony_Backend.API.Data;

#nullable disable

namespace Tony_Backend.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240627200250_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "7d6c3b1a-deb8-46be-a524-984c4409d16d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "671f503d-c9a8-4151-a288-80184f21da1a",
                            Email = "giulio@giulio.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "-2120870629",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5b62ecab-e2f8-4d9c-ad0d-38a168ac19c3",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "31413c56-c404-454a-91a4-c60cff775059",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4f7028fc-e994-4118-9ac4-f0dd9cbd2924",
                            Email = "a@a.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "-152682125",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "856abb43-c65c-421d-bbdd-2db5df417f09",
                            TwoFactorEnabled = false
                        });
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.ChargingSession", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChargingStationId")
                        .HasColumnType("uuid");

                    b.Property<int>("ChargingStationNumber")
                        .HasColumnType("integer");

                    b.Property<decimal?>("CostKWh")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("EndingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double?>("FinalConsuption")
                        .HasColumnType("double precision");

                    b.Property<decimal?>("FinalCost")
                        .HasColumnType("numeric");

                    b.Property<Guid>("GatewayId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan?>("TotalTime")
                        .HasColumnType("interval");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChargingStationId");

                    b.HasIndex("GatewayId");

                    b.HasIndex("UserId");

                    b.ToTable("ChargingSessions");
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.ChargingStation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GatewayId")
                        .HasColumnType("uuid");

                    b.Property<string>("LastLog")
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserConnectedId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GatewayId");

                    b.ToTable("ChargingStations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d7e958e1-b221-4558-a2d1-8a14e525589c"),
                            GatewayId = new Guid("cfc6bf21-3540-44d1-bb7b-1287e2840108"),
                            Number = 1,
                            Status = "Free"
                        },
                        new
                        {
                            Id = new Guid("672ed01b-3dbf-412c-88da-c756c0bc4794"),
                            GatewayId = new Guid("cfc6bf21-3540-44d1-bb7b-1287e2840108"),
                            Number = 2,
                            Status = "Free"
                        },
                        new
                        {
                            Id = new Guid("56d421ec-c195-4d37-b262-bfb8fb82a461"),
                            GatewayId = new Guid("d49761ba-fcf5-419e-ae26-8e5019bf0105"),
                            Number = 1,
                            Status = "Free"
                        },
                        new
                        {
                            Id = new Guid("6a6ea009-93a6-4007-8b55-50ea9ffa7279"),
                            GatewayId = new Guid("d49761ba-fcf5-419e-ae26-8e5019bf0105"),
                            Number = 2,
                            Status = "Free"
                        },
                        new
                        {
                            Id = new Guid("8e708c53-b835-4f81-8594-b1b06f2b7a7e"),
                            GatewayId = new Guid("d49761ba-fcf5-419e-ae26-8e5019bf0105"),
                            Number = 3,
                            Status = "Free"
                        },
                        new
                        {
                            Id = new Guid("ee86d492-c89f-4b31-a77e-6906984fa891"),
                            GatewayId = new Guid("9753a3fe-47be-4cf5-a91b-3e57c0212f15"),
                            Number = 1,
                            Status = "Free"
                        },
                        new
                        {
                            Id = new Guid("68a87a8c-81ac-46fa-a1c6-88b5868631ed"),
                            GatewayId = new Guid("9753a3fe-47be-4cf5-a91b-3e57c0212f15"),
                            Number = 2,
                            Status = "Free"
                        },
                        new
                        {
                            Id = new Guid("1d4e1dc2-a3b8-435e-9ed3-dc2e4875d29b"),
                            GatewayId = new Guid("9753a3fe-47be-4cf5-a91b-3e57c0212f15"),
                            Number = 3,
                            Status = "Free"
                        },
                        new
                        {
                            Id = new Guid("f93682ee-227f-4588-9654-b1994ac18a15"),
                            GatewayId = new Guid("9753a3fe-47be-4cf5-a91b-3e57c0212f15"),
                            Number = 4,
                            Status = "Free"
                        },
                        new
                        {
                            Id = new Guid("2af4da64-b8ee-4d34-8e28-fdfb5b79e7a9"),
                            GatewayId = new Guid("56be472b-8edf-4f41-acd3-3a25c0c0a46f"),
                            Number = 1,
                            Status = "Free"
                        });
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.Gateway", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Gateways");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cfc6bf21-3540-44d1-bb7b-1287e2840108"),
                            Latitude = 45.951543000000001,
                            Longitude = 12.680626999999999,
                            Name = "Consorzio Universitario"
                        },
                        new
                        {
                            Id = new Guid("d49761ba-fcf5-419e-ae26-8e5019bf0105"),
                            Latitude = 45.953619000000003,
                            Longitude = 12.687381999999999,
                            Name = "Aldi - Pordenone"
                        },
                        new
                        {
                            Id = new Guid("9753a3fe-47be-4cf5-a91b-3e57c0212f15"),
                            Latitude = 45.953282000000002,
                            Longitude = 12.672556,
                            Name = "Naonis Gym"
                        },
                        new
                        {
                            Id = new Guid("56be472b-8edf-4f41-acd3-3a25c0c0a46f"),
                            Latitude = 45.9512,
                            Longitude = 12.675172,
                            Name = "Poste Italiane"
                        });
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<double>("CostKWh")
                        .HasColumnType("double precision");

                    b.Property<decimal>("MonthlyCost")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MonthlyCredit")
                        .HasColumnType("numeric");

                    b.Property<int>("PlaneName")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.TopUpWallet", b =>
                {
                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uuid");

                    b.ToTable("TopUpWallets");
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("PayPerUseCredit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("SubscriptionCredit")
                        .HasColumnType("numeric");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Tony_Backend.Shared.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Tony_Backend.Shared.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tony_Backend.Shared.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Tony_Backend.Shared.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.ChargingSession", b =>
                {
                    b.HasOne("Tony_Backend.Shared.Entities.ChargingStation", "ChargingStation")
                        .WithMany("ChargingSessions")
                        .HasForeignKey("ChargingStationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tony_Backend.Shared.Entities.Gateway", "Gateway")
                        .WithMany()
                        .HasForeignKey("GatewayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tony_Backend.Shared.Entities.ApplicationUser", "User")
                        .WithMany("ChargingSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargingStation");

                    b.Navigation("Gateway");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.ChargingStation", b =>
                {
                    b.HasOne("Tony_Backend.Shared.Entities.Gateway", "Gateway")
                        .WithMany("ChargingStations")
                        .HasForeignKey("GatewayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gateway");
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.Subscription", b =>
                {
                    b.HasOne("Tony_Backend.Shared.Entities.ApplicationUser", "User")
                        .WithOne("Subscription")
                        .HasForeignKey("Tony_Backend.Shared.Entities.Subscription", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.Wallet", b =>
                {
                    b.HasOne("Tony_Backend.Shared.Entities.ApplicationUser", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("Tony_Backend.Shared.Entities.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.ApplicationUser", b =>
                {
                    b.Navigation("ChargingSessions");

                    b.Navigation("Subscription")
                        .IsRequired();

                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.ChargingStation", b =>
                {
                    b.Navigation("ChargingSessions");
                });

            modelBuilder.Entity("Tony_Backend.Shared.Entities.Gateway", b =>
                {
                    b.Navigation("ChargingStations");
                });
#pragma warning restore 612, 618
        }
    }
}
