using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.Entities
{
    public class ChargingStation
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
        public ChargingStationStatus? Status { get; set; }
        public int? UserConnectedId { get; set; }
        public string? LastLog { get; set; }

        public Guid GatewayId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Gateway Gateway { get; set; }
        [JsonIgnore]
        public List<ChargingSession> ChargingSessions { get; set; }
    }

    public enum ChargingStationStatus
    {
        Free,
        Charging,
        Completed,
        Idle
    }
}
