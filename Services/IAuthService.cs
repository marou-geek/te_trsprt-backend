using TE_trsprt_remake.DTOs;

namespace TE_trsprt_remake.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDTO model);
        Task<string> RegisterAsync(RegisterDTO model);


    }
}
