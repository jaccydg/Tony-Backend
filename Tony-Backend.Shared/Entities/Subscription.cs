using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Plane PlaneName { get; set; } = Plane.Free;
        public decimal MonthlyCost { get; set; } = 0;
        public decimal MonthlyCredit { get; set; } = 0;
        public double CostKWh { get; set; } = 0;

        // navigation properties
        [JsonIgnore]
        public List<ApplicationUser> User { get; set; }
    }

    public enum Plane
    {
        Free,
        Premium
    }
}




//Plan name
//Monthly cost
//Monthly credit
//Cost kWh