using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });
                
            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));

            services.AddTransient(
               typeof(IPipelineBehavior<,>),
               typeof(ValidationBehavior<,>));

            services.AddTransient(
               typeof(IPipelineBehavior<,>),
               typeof(PerformanceBehavior<,>));

            services.AddTransient(
               typeof(IPipelineBehavior<,>),
               typeof(UnitOfWorkBehavior<,>));

            return services;
        }
    }
}
