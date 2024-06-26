using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.Entities
{
    public class ChargingSession
    {
        public int ChargindStationNumber { get; set; }
        public ChargingSessionStatus? Status { get; set; }
        public decimal? FinalCost { get; set; }
        public double? FinalConsumation { get; set; }
        public Timer? TotalTime { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public decimal? CostKWh { get; set; }
    }

    public enum ChargingSessionStatus
    {
        Ongoing,
        Completed
    }
}

