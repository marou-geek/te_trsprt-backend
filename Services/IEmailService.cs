namespace TE_trsprt_remake.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);

    }
}
