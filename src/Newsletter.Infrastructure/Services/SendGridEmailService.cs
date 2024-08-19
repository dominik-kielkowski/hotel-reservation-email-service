using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Newsletter.Core.Interfaces;

public class SendGridEmailService : IEmailService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly EmailAddress _from;

    public SendGridEmailService(string apiKey, string fromEmail, string fromName)
    {
        _sendGridClient = new SendGridClient(apiKey);
        _from = new EmailAddress(fromEmail, fromName);
    }

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent = null)
    {
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(_from, to, subject, plainTextContent, htmlContent ?? plainTextContent);

        var response = await _sendGridClient.SendEmailAsync(msg);


        return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;
    }
}
