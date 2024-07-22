using Microsoft.AspNetCore.Mvc;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public interface IPlantService
    {
        Task<ActionResult<IEnumerable<Plant>>> GetPlants();
        Task<bool> UpdatePlant(PlantDTO plant, long id);
        Task<bool> DeletePlant(long id);
        Task<bool> AddPlant(PlantDTO plant);
    }
}
