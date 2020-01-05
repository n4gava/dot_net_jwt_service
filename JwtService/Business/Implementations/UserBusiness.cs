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
            var user = new User()
            {
                Username = email,
                Email = email
            };

            var result = (await _userRepository.Save(user, password)).Cast<User>();

            return result ? result.Ok(user) : result;
        }

        public async Task<Result<User>> FindUserByEmail(string email)
        {
            return await _userRepository.FindByEmail(email);
        }

        public async Task<Result<ClaimsIdentity>> FindClaimsByEmail(string email)
        {
            var resultFindUser = await FindUserByEmail(email);
            if (!resultFindUser)
                return new Result<ClaimsIdentity>().Add(resultFindUser);

            return await FindClaimsByUser(resultFindUser.Value);
        }

        public async Task<Result<ClaimsIdentity>> FindClaimsByUser(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var claimsIdentity = new ClaimsIdentity(claims);

            return new Result<ClaimsIdentity>().Ok(claimsIdentity);
        }
    }
}
