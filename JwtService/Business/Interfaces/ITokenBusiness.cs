using JwtService.Commons;
using JwtService.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace JwtService.Business.Interfaces
{
    public interface ITokenBusiness
    {
        Task<Result<string>> GenerateTokenByUser(User user);
    }
}
