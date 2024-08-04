using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using TE_trsprt_remake.Services;

namespace TE_trsprt_remake.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userservice;

        public UserController(IUserService userservice)
        {
            _userservice = userservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userservice.GetUsers();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
      
            bool isDeleted = await _userservice.DeleteUser(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost("{accountstatus}/{id}")]
        public async Task<IActionResult> SetAccountStatus(string accountstatus, long id)
        {
            bool isAccountStatusChanged = await _userservice.SetAccountStatus(accountstatus, id);

            if (isAccountStatusChanged)
            {
                return NoContent(); 
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpPut("{Role}/{id}")]
        public async Task<IActionResult> setRole(string Role, long id)
        {
            bool isRoleChanged = await _userservice.SetRole(Role, id);

            if (isRoleChanged)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateUser(long id, [FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            bool isUpdated = await _userservice.UpdateUser(user, id);
            if (isUpdated)
            {
                return NoContent(); 
            }
            else
            {
                return NotFound(); 
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(UserDTO userDto)
        {
            var result = await _userservice.AddUser(userDto);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingUsers()
        {
            var pendingUsers = await _userservice.GetPendingUsersAsync();
            return Ok(pendingUsers);
        }

    }
}
