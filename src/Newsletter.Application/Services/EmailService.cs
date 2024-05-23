using Newsletter.Core.Interfaces;

namespace Newsletter.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailService _emailService;

        public EmailService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string plainTextContent)
        {
            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent);
        }
    }
}
