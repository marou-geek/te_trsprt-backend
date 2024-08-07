using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public interface IUserPlantService
    {
        Task<ActionResult<IEnumerable<UserPlant>>> GetUserPlants();
        Task<bool> AddUserPlant(UserPlantDTO userPlant);
        Task<bool> DeleteUserPlant(int id);
        Task<bool> UpdateUserPlant(UserPlantDTO userPlant, int id);
    }
}
