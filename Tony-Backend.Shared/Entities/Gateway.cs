using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.Entities
{

    public class Gateway
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }


        // Navigation properties
        [JsonIgnore]
        public ICollection<ChargingStation> ChargingStations { get; set; }
    }
}
