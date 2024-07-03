using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.Entities
{
    // override di IdentityUser
    public class ApplicationUser : IdentityUser
    {

        // navigation property
        [JsonIgnore]
        public ICollection<ChargingSession> ChargingSessions { get; set; }
        [JsonIgnore]
        public Wallet Wallet { get; set; }
        [JsonIgnore]
        public Subscription Subscription { get; set; }
        public Guid SubscriptionId { get; set; }

    }
}
