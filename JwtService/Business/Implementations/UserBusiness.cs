using JwtService.Business.Interfaces;
using JwtService.Commons;
using JwtService.Entities;
using JwtService.Repositories.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtService.Business.Implementations
{
    public class UserBusiness : IUserBusiness
    {
        IUserRepository _userRepository;
        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<User>> CreateUser(string email, string password)
        {
            var result = new Result<User>();

            var findExistingUser = await FindUserByEmail(email);
            if (findExistingUser)
                return result.Add("Email already registered.");

            var user = new User()
            {
                Username = email,
                Email = email
            };

            result = (await _userRepository.Save(user, password)).Cast<User>();

            if (!result)
                return result;

            return result ? result.Ok(user) : result;
        }

        public async Task<Result<User>> FindUserByEmail(string email)
        {
            return await _userRepository.FindByEmail(email);
        }
    }
}
