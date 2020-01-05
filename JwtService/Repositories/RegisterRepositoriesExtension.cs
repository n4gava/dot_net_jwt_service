using JwtService.Repositories.Implementations;
using JwtService.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JwtService.Repositories
{
    public static class RegisterRepositoriesExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        }
    }
}
