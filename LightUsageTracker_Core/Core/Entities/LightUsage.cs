using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightUsageTracker_Core.Core.Entities
{
    public class LightUsage : BaseClass
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }
        public decimal KilowattPerHour { get; set; }
        public decimal Amount { get; set; }
        public string BatchId { get; set; }
        public Users User { get; set; }

    }
}
