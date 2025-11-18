using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightUsageTracker_Core.Services.DTOs
{
    public class CreateUserDto
    {
        public string? UserName { get; set; }
        public string? Room_No { get; set; }
        public decimal? PresentLightUsage { get; set; }
    }
}
