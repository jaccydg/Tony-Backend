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
                    Name = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false)
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
                    UserConnectedId = table.Column<int>(type: "integer", nullable: false),
                    LastLogId = table.Column<int>(type: "integer", nullable: false)
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
                    { 1, 48.8566, 2.3521999999999998, "Gateway 1" },
                    { 2, 48.8566, 2.3512, "Gateway 2" },
                    { 3, 48.8566, 2.3589000000000002, "Gateway 3" }
                });

            migrationBuilder.InsertData(
                table: "ChargingStations",
                columns: new[] { "GatewayId", "Number", "LastLogId", "UserConnectedId" },
                values: new object[,]
                {
                    { 1, 1, 0, 0 },
                    { 2, 1, 0, 0 },
                    { 3, 1, 0, 0 },
                    { 1, 2, 0, 0 },
                    { 2, 2, 0, 0 },
                    { 3, 2, 0, 0 },
                    { 2, 3, 0, 0 },
                    { 3, 3, 0, 0 },
                    { 3, 4, 0, 0 }
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
