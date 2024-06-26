﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tony_Backend.Shared.Entities
{
    public class ChargingSession
    {
        public Guid Id { get; set; } 
      
        public ChargingSessionStatus Status { get; set; }
        public decimal? FinalCost { get; set; }
        public double? FinalConsuption { get; set; }
        public TimeSpan? TotalTime { get; set; }

        public decimal? CostKWh { get; set; }
        public Guid UserId { get; set; }
        public int ChargindStationNumber { get; set; }
        public int GatewayId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
    }

    public enum ChargingSessionStatus
    {
        Ongoing,
        Completed
    }
}

