using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDTO model);
        Task<string> RegisterAsync(RegisterDTO model);
        Task<bool> GenerateAndSendResetPasswordRequestAsync(string email);
        Task<bool> ResetPasswordBySupervisorAsync(string resetToken);


    }
}
