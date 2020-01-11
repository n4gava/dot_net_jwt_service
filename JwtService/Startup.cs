using JwtService.Business;
using JwtService.Business.Interfaces;
using JwtService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JwtService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = services.ConfigureAppSettings(Configuration.GetSection("AppSettings"));

            services.ConfigureSwagger();
            services.ConfigureDbContext(Configuration.GetConnectionString("SqlServerDatabase"));
            services.ConfigureAuthentication(appSettings);

            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                // Supress Model State validation from ApiControllerAttribute
                opt.SuppressModelStateInvalidFilter = true;
            });

            services.RegisterBusiness();
            services.RegisterRepositories();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JwtService");
            });

            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}