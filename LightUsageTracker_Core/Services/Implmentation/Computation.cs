using LightUsageTracker_Core.Core.Entities;
using LightUsageTracker_Core.Infrastructure;
using LightUsageTracker_Core.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightUsageTracker_Core.Services.Implmentation
{
    public class Computation : IComputation
    {
        private readonly AppDbContext _Db;

        public Computation(AppDbContext Db)
        {
            _Db = Db;
        }

        public async Task<string> CalculateLightUsage(Dictionary<Guid, decimal> newUsages, decimal kilowattRate)
        {
            if (kilowattRate < 0)
            {
                throw new ValidationException("Kilowatt rate can not be null");
            }
            var user = _Db.Users.ToList();


            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            string guidPart = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpperInvariant();
            
            string batchId = $"{datePart}-{user.Count}-{guidPart}";
            try
            {
                foreach (var entry in newUsages)
                {
                    var userId = entry.Key;
                    var InputedUsage = entry.Value;

                    var Singleuser = user!.FirstOrDefault(u => u.UserId == userId);
                    
                    if (user != null)
                    {
                        // calculate difference
                        var usageDiff = InputedUsage - Singleuser.PresentLightUsage;
                        var amount = usageDiff * kilowattRate;

                        // save computation record
                        var Light = new LightUsage
                        {
                            id = Guid.NewGuid(),
                            UserId = Singleuser.UserId,
                            KilowattPerHour = usageDiff.Value,
                            Amount = amount.Value,
                            CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                            UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                            BatchId = batchId,
                        };

                        _Db.LightUsages.Add(Light);

                        // update user's past/present usage
                        Singleuser.PastLightUsage = Singleuser.PresentLightUsage;
                        Singleuser.PresentLightUsage = InputedUsage;

                        _Db.Users.Update(Singleuser);

                    }
                }

                _Db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return batchId;
        }

        public async Task<List<LightUsage>> GetLightUsages(string batchId)
        {
            var result = new List<LightUsage>();
            try
            {
                result = _Db.LightUsages.Include(c => c.User).Where(c => c.BatchId == batchId).OrderByDescending(C => C.CreatedAt).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
