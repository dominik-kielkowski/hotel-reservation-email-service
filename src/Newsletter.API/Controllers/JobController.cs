using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Application.Jobs;
using Newsletter.Core.Interfaces;

namespace Newsletter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IEmailService _emailService;

        public JobController(IBackgroundJobClient backgroundJobClient, IEmailService emailService)
        {
            _backgroundJobClient = backgroundJobClient;
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendTestEmail()
        {
            _backgroundJobClient.Enqueue<EmailJob>(job => job.SendDailyEmail());
            return Ok();
        }

        [HttpPost("SendTestMail")]
        public async Task<IActionResult> SendEmail(string toEmail, string subject, string plainTextContent)
        {
            if (ModelState.IsValid)
            {
                await _emailService.SendEmailAsync(toEmail, subject, plainTextContent);
            }
            return Ok();
        }
    }
}
