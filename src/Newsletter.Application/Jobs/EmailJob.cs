using Newsletter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsletter.Application.Jobs
{
    public class EmailJob
    {
        private readonly IEmailService _emailService;

        public EmailJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendDailyEmail()
        {
            await _emailService.SendEmailAsync("recipient@example.com", "Daily Newsletter", "This is the plain text content");
        }
    }
}
