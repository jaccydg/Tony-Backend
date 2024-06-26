using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tony_Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class CharginSessionEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargingSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FinalCost = table.Column<decimal>(type: "numeric", nullable: true),
                    FinalConsuption = table.Column<double>(type: "double precision", nullable: true),
                    TotalTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    CostKWh = table.Column<decimal>(type: "numeric", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChargindStationNumber = table.Column<int>(type: "integer", nullable: false),
                    GatewayId = table.Column<int>(type: "integer", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingSessions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargingSessions");
        }
    }
}
