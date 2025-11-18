using LightUsageTracker_Core.Core.Entities;
using LightUsageTracker_Core.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightUsageTracker_Core.Services.Interface
{
    public interface ITrackerLogic
    {
       Task<List<Users>> GetUsers();
       Task<bool> CreateUser(CreateUserDto userDto);
       Task<Users> GetUserById(Guid userId);
       Task<Users> EditUser(Users userDto);
        Task<bool> CheckIfUserIsComputed(Guid userId);
        Task<List<LightUsage>> GetLightUsagesForSingleUser(Guid id);
    }
}
