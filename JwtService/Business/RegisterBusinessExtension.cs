using JwtService.Business.Implementations;
using JwtService.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JwtService.Business
{
    public static class RegisterBusinessExtension
    {
        public static void RegisterBusiness(this IServiceCollection services)
        {
            services.AddScoped<ITokenBusiness, TokenBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<ISignInBusiness, SignInBusiness>();
            
        }
    }
}
