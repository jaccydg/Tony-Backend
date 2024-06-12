using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoCoordinatePortable;


namespace Tony_Backend.Shared.Entities
{

    public class Gateway
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }


        // Navigation properties
        public ICollection<ChargingStation> ChargingStations { get; set; }
    }
}
