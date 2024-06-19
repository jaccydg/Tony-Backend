using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.Entities
{
    public class ChargingStation
    {
        public int Number { get; set; }
        public int GatewayId { get; set; }
        public ChargingStationStatus Status { get; set; }
        public int? UserConnectedId { get; set; }
        public int? LastLogId { get; set; }

        // Navigation properties
        public Gateway Gateway { get; set; }
    }

    public enum ChargingStationStatus
    {
        Vacant,
        Charging,
        Completed,
        Idle
    }
}
