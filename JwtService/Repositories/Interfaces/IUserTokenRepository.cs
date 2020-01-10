using JwtService.Commons;
using JwtService.Entities;
using System.Threading.Tasks;

namespace JwtService.Repositories.Interfaces
{
    public interface IUserTokenRepository : IEntityRepository<UserToken>
    {
        Task<Result> DeleteByEmail(string email);
    }
}
