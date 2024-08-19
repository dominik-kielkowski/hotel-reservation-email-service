using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Application.Jobs;
using Newsletter.Core.Interfaces;

namespace Newsletter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController(IBackgroundJobClient backgroundJobClient, IEmailService emailService) : ControllerBase
    {
        [HttpPost]
        public IActionResult SendTestEmail()
        {
            backgroundJobClient.Enqueue<EmailJob>(job => job.SendDailyEmail());
            return Ok();
        }

        [HttpPost("SendTestMail")]
        public async Task<IActionResult> SendEmail(string toEmail, string subject, string plainTextContent)
        {
            if (ModelState.IsValid)
            {
                await emailService.SendEmailAsync(toEmail, subject, plainTextContent);
            }
            return Ok();
        }
    }
}
