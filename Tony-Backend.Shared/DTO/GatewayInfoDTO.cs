using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.DTO
{
    public class GatewayInfoDTO
    {
        public string? Name { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int FreeChargingStations { get; set; }
        public int TotalChargingStations { get; set; }
    }
}
