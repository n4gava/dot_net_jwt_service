using JwtService.Entities;
using JwtService.Repositories.Bases;
using JwtService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JwtService.Repositories.Implementations
{
    public class UserTokenRepository : BaseRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
