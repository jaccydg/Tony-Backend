using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tony_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gateways",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaneName = table.Column<int>(type: "integer", nullable: false),
                    MonthlyCost = table.Column<decimal>(type: "numeric", nullable: false),
                    MonthlyCredit = table.Column<decimal>(type: "numeric", nullable: false),
                    CostKWh = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopUpWallets",
                columns: table => new
                {
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpWallets", x => new { x.WalletId, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChargingStations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    UserConnectedId = table.Column<string>(type: "text", nullable: true),
                    GatewayId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargingStations_Gateways_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "Gateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChargingSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    FinalCost = table.Column<decimal>(type: "numeric", nullable: true),
                    FinalConsuption = table.Column<double>(type: "double precision", nullable: true),
                    TotalTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    CostKWh = table.Column<decimal>(type: "numeric", nullable: true),
                    ChargingStationNumber = table.Column<int>(type: "integer", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ChargingStationId = table.Column<Guid>(type: "uuid", nullable: false),
                    GatewayId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargingSessions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChargingSessions_ChargingStations_ChargingStationId",
                        column: x => x.ChargingStationId,
                        principalTable: "ChargingStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChargingSessions_Gateways_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "Gateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionCredit = table.Column<decimal>(type: "numeric", nullable: false),
                    PayPerUseCredit = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gateways",
                columns: new[] { "Id", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("6e18b373-5c97-4ff0-ab8c-ceecd58989b3"), 45.953619000000003, 12.687381999999999, "Aldi - Pordenone" },
                    { new Guid("6f672f1f-fb92-4332-af43-a282a85477a9"), 45.9512, 12.675172, "Poste Italiane" },
                    { new Guid("9249be30-9647-40d8-be3f-7f6299b23d79"), 45.953282000000002, 12.672556, "Naonis Gym" },
                    { new Guid("9d1c399a-5ad4-4dcb-a489-979d5626717b"), 45.951543000000001, 12.680626999999999, "Consorzio Universitario" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "CostKWh", "MonthlyCost", "MonthlyCredit", "PlaneName" },
                values: new object[,]
                {
                    { new Guid("ab32905a-3f12-4ab5-bba8-791efcdb81a2"), 0.25, 29.99m, 100m, 1 },
                    { new Guid("f99d99ab-e389-4c48-8f52-ec620557289c"), 0.34999999999999998, 0m, 0m, 0 }
                });

            migrationBuilder.InsertData(
                table: "TopUpWallets",
                columns: new[] { "Date", "WalletId", "Amount" },
                values: new object[,]
                {
                    { new DateTime(2024, 6, 28, 10, 15, 56, 533, DateTimeKind.Utc).AddTicks(4120), new Guid("6888ba4a-1ac7-4a00-8e97-5f2aed1c771c"), 20m },
                    { new DateTime(2024, 6, 23, 10, 15, 56, 533, DateTimeKind.Utc).AddTicks(4125), new Guid("79e28c98-abbd-4747-b77d-5bcde3dc4b24"), 50m }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SubscriptionId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "440e2022-2917-4807-a5f6-58e66a6a4bbd", 0, "b43fad5c-91e6-4185-ac68-a7e1e9cbc70e", "a@a.com", true, false, null, "A@A.COM", "A@A.COM", "AQAAAAIAAYagAAAAEBs6+gVu0+A7hIdC+8g0t0cyWwoctbalflDhKzQMzvfozn6OyTmXKq0/Y1aFUfyYIQ==", null, false, "f0a8e4e8-c4c3-4467-8616-6c324844b7f7", new Guid("f99d99ab-e389-4c48-8f52-ec620557289c"), false, "a@a.com" },
                    { "4664a79a-6d3b-4c39-98b5-b655cb769f82", 0, "07894613-57eb-43a7-86e2-cdafde098e6f", "giulio@giulio.com", true, false, null, "GIULIO@GIULIO.COM", "GIULIO@GIULIO.COM", "AQAAAAIAAYagAAAAEE9SBLCJ9ilCSEh8YPa7FdufEgBvZNjuWDWtFHi/6k/8kyhV9W1uU5AtA+HVQDNaXw==", null, false, "5c5ebc09-5b38-4025-a3a4-109b87e3134d", new Guid("ab32905a-3f12-4ab5-bba8-791efcdb81a2"), false, "giulio@giulio.com" }
                });

            migrationBuilder.InsertData(
                table: "ChargingStations",
                columns: new[] { "Id", "GatewayId", "Number", "Status", "UserConnectedId" },
                values: new object[,]
                {
                    { new Guid("0217fbe0-3fcf-4c04-b069-5244e2812c60"), new Guid("9249be30-9647-40d8-be3f-7f6299b23d79"), 4, "Free", null },
                    { new Guid("33e3f93c-6b6f-4e43-bdde-16adef6f31b7"), new Guid("9d1c399a-5ad4-4dcb-a489-979d5626717b"), 2, "Free", null },
                    { new Guid("42176c8a-aaa0-44e0-bbfa-43bbd180d128"), new Guid("6e18b373-5c97-4ff0-ab8c-ceecd58989b3"), 2, "Free", null },
                    { new Guid("5a509d46-ba42-4f24-a17e-e7111a11d79f"), new Guid("9249be30-9647-40d8-be3f-7f6299b23d79"), 1, "Free", null },
                    { new Guid("7df0da95-1aa9-4623-9889-4113ba0ee49e"), new Guid("9d1c399a-5ad4-4dcb-a489-979d5626717b"), 1, "Free", null },
                    { new Guid("7ee49dba-31be-4024-8c46-d2be56484cc3"), new Guid("9249be30-9647-40d8-be3f-7f6299b23d79"), 2, "Free", null },
                    { new Guid("d98c683e-2be4-476e-af1d-95f71eeac6ca"), new Guid("9249be30-9647-40d8-be3f-7f6299b23d79"), 3, "Free", null },
                    { new Guid("dd41716a-990f-4fca-a789-56299f1200a5"), new Guid("6e18b373-5c97-4ff0-ab8c-ceecd58989b3"), 1, "Free", null },
                    { new Guid("ecdd2e62-1f05-41dc-807a-f22fcda5c679"), new Guid("6e18b373-5c97-4ff0-ab8c-ceecd58989b3"), 3, "Free", null },
                    { new Guid("f14b7511-b89e-45f4-8d58-4c53e8959481"), new Guid("6f672f1f-fb92-4332-af43-a282a85477a9"), 1, "Free", null }
                });

            migrationBuilder.InsertData(
                table: "ChargingSessions",
                columns: new[] { "Id", "ChargingStationId", "ChargingStationNumber", "CostKWh", "EndingDate", "FinalConsuption", "FinalCost", "GatewayId", "StartingDate", "Status", "TotalTime", "UserId" },
                values: new object[,]
                {
                    { new Guid("69eb8e1a-f2c3-4789-871c-f7eb3466a259"), new Guid("33e3f93c-6b6f-4e43-bdde-16adef6f31b7"), 2, 0.42m, new DateTime(2024, 7, 2, 12, 15, 56, 530, DateTimeKind.Utc).AddTicks(2724), 25.5, 10.5m, new Guid("6e18b373-5c97-4ff0-ab8c-ceecd58989b3"), new DateTime(2024, 7, 2, 10, 15, 56, 530, DateTimeKind.Utc).AddTicks(2721), "Completed", new TimeSpan(0, 2, 0, 0, 0), "4664a79a-6d3b-4c39-98b5-b655cb769f82" },
                    { new Guid("b5af9e5c-615a-46a3-a16f-505a997796c1"), new Guid("7df0da95-1aa9-4623-9889-4113ba0ee49e"), 1, null, null, null, null, new Guid("9d1c399a-5ad4-4dcb-a489-979d5626717b"), new DateTime(2024, 7, 3, 8, 15, 56, 530, DateTimeKind.Utc).AddTicks(2699), "Ongoing", null, "4664a79a-6d3b-4c39-98b5-b655cb769f82" },
                    { new Guid("c0e9234b-f4de-4637-98d7-e62aa8158f41"), new Guid("33e3f93c-6b6f-4e43-bdde-16adef6f31b7"), 2, 0.42m, new DateTime(2024, 7, 2, 12, 15, 56, 530, DateTimeKind.Utc).AddTicks(2750), 25.5, 10.5m, new Guid("6e18b373-5c97-4ff0-ab8c-ceecd58989b3"), new DateTime(2024, 7, 2, 10, 15, 56, 530, DateTimeKind.Utc).AddTicks(2750), "Completed", new TimeSpan(0, 2, 0, 0, 0), "440e2022-2917-4807-a5f6-58e66a6a4bbd" },
                    { new Guid("db930ac6-28f5-4704-bbcc-06aad3373a52"), new Guid("7df0da95-1aa9-4623-9889-4113ba0ee49e"), 1, null, null, null, null, new Guid("6e18b373-5c97-4ff0-ab8c-ceecd58989b3"), new DateTime(2024, 7, 3, 8, 15, 56, 530, DateTimeKind.Utc).AddTicks(2747), "Ongoing", null, "440e2022-2917-4807-a5f6-58e66a6a4bbd" }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "PayPerUseCredit", "SubscriptionCredit", "UserId" },
                values: new object[,]
                {
                    { new Guid("c4616a19-1b8a-4098-a0e6-42f165a0223e"), 20m, 50m, "4664a79a-6d3b-4c39-98b5-b655cb769f82" },
                    { new Guid("dd30b1a4-5dbf-4d2e-8959-779444222af4"), 30m, 0m, "440e2022-2917-4807-a5f6-58e66a6a4bbd" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SubscriptionId",
                table: "AspNetUsers",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSessions_ChargingStationId",
                table: "ChargingSessions",
                column: "ChargingStationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSessions_GatewayId",
                table: "ChargingSessions",
                column: "GatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSessions_UserId",
                table: "ChargingSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingStations_GatewayId",
                table: "ChargingStations",
                column: "GatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChargingSessions");

            migrationBuilder.DropTable(
                name: "TopUpWallets");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ChargingStations");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Gateways");

            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
