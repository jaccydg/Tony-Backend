using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tony_Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class SomeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gateways",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargingStations",
                columns: table => new
                {
                    Number = table.Column<int>(type: "integer", nullable: false),
                    GatewayId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    UserConnectedId = table.Column<int>(type: "integer", nullable: true),
                    LastLogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingStations", x => new { x.Number, x.GatewayId });
                    table.ForeignKey(
                        name: "FK_ChargingStations_Gateways_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "Gateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gateways",
                columns: new[] { "Id", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, 45.951543000000001, 12.680626999999999, "Consorzio Universitario" },
                    { 2, 45.953619000000003, 12.687381999999999, "Aldi - Pordenone" },
                    { 3, 45.953282000000002, 12.672556, "Naonis Gym" },
                    { 4, 45.9512, 12.675172, "Poste Italiane" }
                });

            migrationBuilder.InsertData(
                table: "ChargingStations",
                columns: new[] { "GatewayId", "Number", "LastLogId", "Status", "UserConnectedId" },
                values: new object[,]
                {
                    { 1, 1, null, "Vacant", null },
                    { 2, 1, null, "Vacant", null },
                    { 3, 1, null, "Vacant", null },
                    { 1, 2, null, "Vacant", null },
                    { 2, 2, null, "Vacant", null },
                    { 3, 2, null, "Vacant", null },
                    { 2, 3, null, "Vacant", null },
                    { 3, 3, null, "Vacant", null },
                    { 3, 4, null, "Vacant", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargingStations_GatewayId",
                table: "ChargingStations",
                column: "GatewayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargingStations");

            migrationBuilder.DropTable(
                name: "Gateways");
        }
    }
}
