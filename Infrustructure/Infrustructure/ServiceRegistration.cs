using Application.Common.Interfaces;
using Infrustructure.Services;
using Infrustructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrustructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrustructure(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>()
            ?? new EmailSettings();
            services.AddSingleton(emailSettings);

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
