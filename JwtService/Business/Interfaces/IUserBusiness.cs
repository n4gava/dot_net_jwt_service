using JwtService.Commons;
using JwtService.Entities;
using System.Threading.Tasks;

namespace JwtService.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<Result<User>> CreateUser(string email, string password);
        Task<Result<User>> FindUserByEmail(string email);
    }
}
