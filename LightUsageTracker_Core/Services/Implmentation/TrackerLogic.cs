using LightUsageTracker_Core.Core.Entities;
using LightUsageTracker_Core.Infrastructure;
using LightUsageTracker_Core.Services.DTOs;
using LightUsageTracker_Core.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightUsageTracker_Core.Services.Implmentation
{

    public class TrackerLogic : ITrackerLogic
    {
        private readonly AppDbContext _Db;

        public TrackerLogic(AppDbContext Db)
        {
            _Db = Db;
        }
        public async Task<bool> CreateUser(CreateUserDto userDto)
        {
            var User = new Users
            {
                UserId = Guid.NewGuid(),
                UserName = userDto.UserName,
                PresentLightUsage = userDto.PresentLightUsage,
                PastLightUsage = userDto.PresentLightUsage,
                Room_No = userDto.Room_No,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            try
            {
                 _Db.Users.Add(User);
                _Db.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public async Task<Users> EditUser(Users userdto)
        {
            userdto.UpdatedAt = DateTime.UtcNow;
            var user = new Users();
            try
            {
                user = _Db.Users.Update(userdto).Entity;
                _Db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return user!;
        }

        public async Task<Users> GetUserById(Guid userId)
        {
            var user = new Users();

            try
            {
                user = _Db.Users.FirstOrDefault(c => c.UserId == userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return user!;
        }

        public async Task<bool> CheckIfUserIsComputed(Guid userId)
        {
            bool hasUsage = false;

            try
            {
                hasUsage = await _Db.LightUsages
                                  .AnyAsync(lu => lu.UserId == userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hasUsage!;
        }



        public async Task<List<Users>> GetUsers()
        {
            var users = new List<Users>();
            try
            {
                users = [.. _Db.Users.OrderBy(c => c.CreatedAt)];

            }
            catch (Exception ex)
            {
                throw;
            }
            return users;
        }

        public async Task<List<LightUsage>> GetLightUsagesForSingleUser(Guid id)
        {
            var usages = new List<LightUsage>();
            var user = new Users();

            try
            {
                usages =  await _Db.LightUsages.Where(c => c.UserId == id).OrderBy(c => c.CreatedAt).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return usages;
        }
    }
}
