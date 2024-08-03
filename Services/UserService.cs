using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteUser(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Id == id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> SetAccountStatus(string AccountStatus, long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            user.AccountStatus = AccountStatus;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetRole(string Role, long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            user.Role = Role;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUser(UserDTO user, long id)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.TE_Id = user.TE_Id;
            existingUser.FullName = user.FullName ;
            existingUser.Title = user.Title;
            existingUser.Email = user.Email;
            existingUser.SvEmail = user.SvEmail;
            existingUser.PlantId = user.PlantId;
            existingUser.DepartementId = user.DepartementId;
            existingUser.AccountStatus = user.AccountStatus;
            existingUser.Address = user.Address;
            existingUser.Role = user.Role;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddUser(UserDTO userDto)
        {
            var user = new User
            {
                TE_Id = userDto.TE_Id,
                FullName = userDto.FullName,
                Title = userDto.Title,
                Email = userDto.Email,
                SvEmail = userDto.SvEmail,
                PlantId = userDto.PlantId,  
                DepartementId = userDto.DepartementId,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                AccountStatus = userDto.AccountStatus,
                Address = userDto.Address,
                Role = userDto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetPendingUsersAsync()
        {
            return await _context.Users
                .Where(u => u.AccountStatus == "pending")
                .ToListAsync();
        }

    }
}
