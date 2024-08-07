using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public class UserPlantService : IUserPlantService
    {
        private readonly AppDbContext _context;

        public UserPlantService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<UserPlant>>> GetUserPlants()
        {
            return await _context.UserPlants.ToListAsync();
        }

        public async Task<bool> AddUserPlant(UserPlantDTO userPlant)
        {
            var up = new UserPlant
            {
                UserId = userPlant.UserId,
                PlantId = userPlant.PlantId
            };

            _context.UserPlants.Add(up);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserPlant(int id)
        {
            var up = await _context.UserPlants.FirstOrDefaultAsync(up => up.Id == id);
            if (up == null)
            {
                return false;
            }

            _context.UserPlants.Remove(up);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserPlant(UserPlantDTO userPlant, int id)
        {
            var existingUserPlant = await _context.UserPlants.FirstOrDefaultAsync(up => up.Id == id);
            if (existingUserPlant == null)
            {
                return false;
            }

            existingUserPlant.UserId = userPlant.UserId;
            existingUserPlant.PlantId = userPlant.PlantId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
