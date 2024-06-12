using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoCoordinatePortable;


namespace Tony_Backend.Shared.Entities
{
    public class ChargingStation
    {
        public int Number { get; set; }
        public int UserConnectedId { get; set; }
        public int LastLogId { get; set; }
        public int GatewayId { get; set; }

        // Navigation properties
        public Gateway Gateway { get; set; }
    }
}
