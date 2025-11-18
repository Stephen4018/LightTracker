using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightUsageTracker_Core.Core.Entities
{
    public class KilowattRate : BaseClass
    {
        public Guid Id { get; set; }
        public decimal RatePerKilowatt { get; set; }
    }

}
