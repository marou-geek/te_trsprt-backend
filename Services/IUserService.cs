using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public interface IUserService
    {
        Task<bool> SetAccountStatus(String AccountStatus , long id);
        Task<bool> SetRole(String Role, long id);
        Task<ActionResult<IEnumerable<User>>> GetUsers();
        Task<bool> UpdateUser(UserDTO user, long id);
        Task<bool> DeleteUser (long id);
        Task<IEnumerable<User>> GetPendingUsersAsync();
        Task<bool> AddUser(UserDTO user);

    }
}
