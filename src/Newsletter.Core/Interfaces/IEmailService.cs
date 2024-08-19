namespace Newsletter.Core.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent = null);
    }
}