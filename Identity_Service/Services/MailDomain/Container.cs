using Microsoft.Extensions.DependencyInjection;
using Services.MailDomain.Abstraction;
using Services.MailDomain.Implementation;

namespace Services.MailDomain
{
    public static class Container
    {
        public static IServiceCollection AddMailService(this IServiceCollection services)
        {
            services.AddTransient(typeof(ISendEmailService), typeof(SendEmailService));

            return services;
        }
    }
}
