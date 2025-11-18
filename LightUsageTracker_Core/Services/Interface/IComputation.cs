using LightUsageTracker_Core.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightUsageTracker_Core.Services.Interface
{
    public interface IComputation
    {
        Task<string> CalculateLightUsage(Dictionary<Guid, decimal> newUsages, decimal kilowattRate);
        Task<List<LightUsage>> GetLightUsages(string batchId);
    }
}
