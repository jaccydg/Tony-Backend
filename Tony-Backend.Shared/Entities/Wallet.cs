using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tony_Backend.Shared.Entities;

namespace Tony_Backend.Shared.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public decimal SubscriptionCredit { get; set; } = 0;
        public decimal PayPerUseCredit { get; set; } = 0;

        public Guid UserId { get; set; }

        // navigation properties
        [JsonIgnore]
        public IdentityUser User { get; set; }
    }
}
