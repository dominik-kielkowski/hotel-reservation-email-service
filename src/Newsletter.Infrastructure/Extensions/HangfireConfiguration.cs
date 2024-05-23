using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsletter.Infrastructure.Extensions
{
    public class HangfireConfiguration
    {
        public static void ConfigureHangfire(IServiceCollection services, string connectionString)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            services.AddHangfireServer();

        }
    }
}
