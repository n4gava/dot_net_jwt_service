using JwtService.Commons;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace JwtService.Business.Interfaces
{
    public interface ITokenBusiness
    {
        Task<Result<string>> GenerateTokenByUser(IdentityUser user);
    }
}
