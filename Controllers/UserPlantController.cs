using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using TE_trsprt_remake.Services;

namespace TE_trsprt_remake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPlantController : ControllerBase
    {
        private readonly IUserPlantService _userPlantService;

        public UserPlantController(IUserPlantService userPlantService)
        {
            _userPlantService = userPlantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPlant>>> GetUserPlants()
        {
            return await _userPlantService.GetUserPlants();
        }

        [HttpPost]
        public async Task<ActionResult> AddUserPlant(UserPlantDTO userPlantDTO)
        {
            var result = await _userPlantService.AddUserPlant(userPlantDTO);
            if (result)
            {
                return Ok("UserPlant created successfully.");
            }
            return BadRequest("Error creating UserPlant.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserPlant(int id, UserPlantDTO userPlantDTO)
        {
            var result = await _userPlantService.UpdateUserPlant(userPlantDTO, id);
            if (result)
            {
                return Ok("UserPlant updated successfully.");
            }
            return BadRequest("Error updating UserPlant.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPlant(int id)
        {
            var result = await _userPlantService.DeleteUserPlant(id);
            if (result)
            {
                return Ok("UserPlant deleted successfully.");
            }
            return BadRequest("Error deleting UserPlant.");
        }
    }
}
