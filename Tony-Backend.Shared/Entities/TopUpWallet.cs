using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.Entities
{
    public class TopUpWallet
    {
        public Guid WalletId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; } = 0;
    }
}
