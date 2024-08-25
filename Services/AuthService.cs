using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private static readonly Random _random = new Random();

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


       


        public async Task<string> LoginAsync(LoginDTO model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.TE_Id == model.TE_Id);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return null;

            return GenerateJwtToken(user);
        }

        public async Task<string> RegisterAsync(RegisterDTO model)
        {
            var userExists = await _context.Users.AnyAsync(u => u.TE_Id == model.TE_Id);
            if (userExists)
                return null;

            var departementExists = await _context.Departements.AnyAsync(d => d.Id == model.DepartementId);
            if (!departementExists)
                return ("DepartementId does not exist.");

            var user = new User
            {
                TE_Id = model.TE_Id,
                FullName = model.FullName,
                Title = model.Title,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Email = model.Email,
                SvEmail = "",
                DepartementId = model.DepartementId,
                AccountStatus = "pending",
                Address = model.Address,
                Role = "user",
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            foreach (var plantId in model.PlantIds)
            {
                var userPlant = new UserPlant
                {
                    UserId = user.Id,
                    PlantId = plantId
                };
                _context.UserPlants.Add(userPlant);
            }

            await _context.SaveChangesAsync();
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var authClaims = new List<Claim>
                            {   
                                new Claim(ClaimTypes.Name, user.FullName),
                                new Claim(ClaimTypes.Role, user.Role),
                                new Claim("Id", user.Id.ToString()),
                                new Claim("Title", user.Title),
                                new Claim("TEId", user.TE_Id),
                                new Claim("SvEmail", user.SvEmail),
                                new Claim("Email", user.Email),
                                new Claim("Status", user.AccountStatus),
                                new Claim("Departement", user.DepartementId.ToString())
                            };

            var i = 0;
            var userPlants = _context.UserPlants.Where(up => up.UserId == user.Id).ToList();
            foreach (var userPlant in userPlants)
            {
                authClaims.Add(new Claim("Plant" + i, userPlant.PlantId.ToString()));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
