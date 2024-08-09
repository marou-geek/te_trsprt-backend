using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public class PlantService : IPlantService
    {
        private readonly AppDbContext _context;

        public PlantService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants()
        {
            return await _context.Plants.ToListAsync();
        }

        public async Task<bool> AddPlant(PlantDTO plant)
        {
            var pl = new Plant
            {
               Location = plant.Location,
               SAPId = plant.SAPId,
               BuildingId = plant.BuildingId,
               SiteManagerEmail = plant.SiteManagerEmail,
            };

            _context.Plants.Add(pl);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeletePlant(long id)
        {
            var pl = await _context.Plants.FirstOrDefaultAsync(pl => pl.Id == id);
            if (pl == null)
            {
                return false;
            }
            _context.Plants.Remove(pl);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdatePlant(PlantDTO plant, long id)
        {
            var existingplant = await _context.Plants.FirstOrDefaultAsync(plant => plant.Id == id);
            if (existingplant == null)
            {
                return false;
            }

            existingplant.Location = plant.Location;
            existingplant.SAPId = plant.SAPId;
            existingplant.SiteManagerEmail = plant.SiteManagerEmail;
            existingplant.BuildingId = plant.BuildingId;
            
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
