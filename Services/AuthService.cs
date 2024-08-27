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
        private readonly IEmailService _emailservice;
        private static readonly Random _random = new Random();

        public AuthService(AppDbContext context, IConfiguration configuration,IEmailService emailservice)
        {
            _context = context;
            _configuration = configuration;
            _emailservice = emailservice;
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

        public async Task<bool> GenerateAndSendResetPasswordRequestAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            var supervisor = await _context.Users.SingleOrDefaultAsync(u => u.Role == "SuperAdmin");
            if (supervisor == null)
                return false;

            var resetToken = Guid.NewGuid().ToString();
            var resetUrl = $"https://localhost:4200/api/auth/resetpassword?token={resetToken}";

            user.PasswordResetToken = resetToken;
            await _context.SaveChangesAsync();

            var body = $@"
                    <p>Dear <strong>{supervisor.FullName}</strong>,</p>
    
                    <p>You are receiving this email because the user <strong>{user.FullName.ToUpper()}</strong> ({user.Email}) has requested a password reset. As their supervisor, you can assist them by resetting their password.</p>

                    <p>Please click the link below to generate a new password :</p>

                    <p><a href='{resetUrl}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; border-radius: 5px;'>Reset Password</a></p>
    
                    <p>Thank you,</p>
                    <p><strong>TE Connectivity Team</strong></p>
                    <p><em>This is an automated message. Please do not reply to this email.</em></p>";
            ;
            await _emailservice.SendEmailAsync(supervisor.Email, "Password Reset Request", body);

            return true;
        }

        public async Task<bool> ResetPasswordBySupervisorAsync(string resetToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.PasswordResetToken == resetToken);
            if (user == null)
                return false;

            var newPassword = GenerateRandomPassword();
            user.Password = HashPassword(newPassword);
            user.PasswordResetToken = null;

            await _context.SaveChangesAsync();

            var body = $@"
                            <p>Dear {user.FullName.ToUpper()},</p>
                            <p>Your password has been successfully reset by the Admin. Please find your new login credentials below:</p>
                            <p><strong>New Password:</strong> <h1>{newPassword}</h1></p>
                            <p>For your security, please log in and change this password as soon as possible.</p>
                            <p>If you encounter any issues or did not request this change, please contact your supervisor or the IT department immediately.</p>
                            <p>Thank you,</p>
                            <p><strong>TE Connectivity</strong></p>
                            <p><em>This is an automated message. Please do not reply to this email.</em></p> ";
            await _emailservice.SendEmailAsync(user.Email, "Your New Password", body);

            return true;
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string HashPassword(string password)
        {
            string pwd = BCrypt.Net.BCrypt.HashPassword(password);
            return pwd; 
        }

    }
}
