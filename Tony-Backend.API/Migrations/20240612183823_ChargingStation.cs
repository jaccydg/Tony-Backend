using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tony_Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class ChargingStation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
