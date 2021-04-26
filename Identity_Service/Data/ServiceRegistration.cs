using Common;
using Data.Contexts;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddDbContext<IdentityContext>(options =>

            options.UseSqlServer(settings.DataBaseConectionString, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "Identity")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {

                //Password Settings
                options.Password.RequireDigit = settings.PasswordRequireDigit;
                options.Password.RequiredLength = settings.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumic;
                options.Password.RequireUppercase = settings.PasswordRequireUppercase;
                options.Password.RequireLowercase = settings.PasswordRequireLowercase;

                //UserName Settings

                //Singin Settings
                //identityOptions.SignIn.RequireConfirmedEmail = false;
                //identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

                //Lockout Settings
                //identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                //identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //identityOptions.Lockout.AllowedForNewUsers = false;
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

        }
    }
}
