using JwtService.Commons;
using JwtService.Database;
using JwtService.Entities;
using JwtService.Repositories.Bases;
using JwtService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Repositories.Implementations
{
    public class UserTokenRepository : BaseRepository<UserToken>, IUserTokenRepository
    {
        AppDbContext _dbContext;
        public UserTokenRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> DeleteByEmail(string email)
        {
            return await Result.DoAndReturnResultAsync(async () =>
            {
                var queryTokens = _dbContext.Tokens.Where(token => token.User.Email == email);

                foreach (var userToken in await queryTokens.ToListAsync())
                {
                    _dbContext.Remove(userToken);
                }
            });
        }
    }
}
