using Common;
using Data;
using Data.Repositores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using WebFramework.Configuration;
using WebFramework.Middlewares;
using WebFramework.Swagger;

namespace Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly SiteSettings _SiteSettings;

        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
            _SiteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityInfrastructure(_SiteSettings.IdentitySettings);
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddRepositoryServices();
            services.AddApplicationServices();
            services.AddControllersWithViews();
            services.AddJwtAuthentication(_SiteSettings.JwtSettings);
            services.AddSwagger();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();
            app.UseSwaggerAndUI();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{urlCallBack?}");
            });
        }
    }
}
