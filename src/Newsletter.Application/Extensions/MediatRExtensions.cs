using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Newsletter.Application.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
