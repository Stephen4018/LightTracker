using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightUsageTracker_Core.Core.Entities
{
    public class Users : BaseClass
    {
        [Key]
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Room_No { get; set; }
        public decimal? PresentLightUsage { get; set; }
        public decimal? PastLightUsage { get; set; }
        public ICollection<LightUsage>? LightUsages { get; set; }
    }
}
