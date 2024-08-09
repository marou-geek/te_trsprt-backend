using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;

namespace TE_trsprt_remake.Services
{

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(
                _configuration["SmtpSettings:SenderName"],
                _configuration["SmtpSettings:SenderEmail"]
            ));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.None);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
