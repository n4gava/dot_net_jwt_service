using JwtService.Commons;
using JwtService.Database;
using JwtService.Entities;
using JwtService.Repositories.Bases;
using JwtService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JwtService.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Save(User entity, string password)
        {
            entity.Password = password;
            return await this.Save(entity);
        }

        public async Task<Result<User>> FindByEmail(string email)
        {
            var result = new Result<User>();
            User user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            
            if (user == null)
                return result.Add("User was not registered.");

            return user.ToResult();
        }
    }
}
