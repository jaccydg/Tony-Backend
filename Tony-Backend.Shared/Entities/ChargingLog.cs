using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.Entities
{
    //Charging log
    //date
    //Speed
    //Consumption so far
    //Cost so far
    //Time
    //Userid
    internal class ChargingLog
    {
        public DateTime Date { get; set; }
        public double Speed { get; set; }
        public double Consumption { get; set; }
        public decimal Cost { get; set; }
        public TimeSpan Time { get; set; }

        public Guid ChargingSessionId { get; set; }
        public Guid UserId { get; set; }

        // navigation properties
        public ChargingSession ChargingSession { get; set; }
        public IdentityUser User { get; set; }
    }
}
