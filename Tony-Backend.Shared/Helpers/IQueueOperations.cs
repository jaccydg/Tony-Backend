using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony_Backend.Shared.Helpers
{
    public interface IQueueOperations
    {
        public Task SendMessage(string message);
    }
}
