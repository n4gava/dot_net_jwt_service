using JwtService.Business.Interfaces;
using JwtService.Commons;
using JwtService.Entities;
using JwtService.Models.User;
using JwtService.Repositories.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using JwtService.Commons.Interfaces;

namespace JwtService.Business.Implementations
{
    public class UserBusiness : IUserBusiness
    {
        IUserRepository _userRepository;
        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<User>> Save(UserVO userVO)
        {
            if (userVO == null)
                throw new ArgumentNullException(nameof(userVO));

            var findExistingUser = await FindUserByEmail(userVO.Email);

            if (findExistingUser)
                return new Result<User>("User is already registered.");

            var user = new User();

            var result = (await Save(user, userVO)).Cast<User>();

            return result ? result.Ok(user) : result;
        }

        public async Task<Result<User>> Save(long id, UserVO userVO)
        {
            if (userVO == null)
                throw new ArgumentNullException(nameof(userVO));

            var findExistingUser = await FindUserById(id);
            if (!findExistingUser)
                return new Result<User>("User was not registered.");

            var user = findExistingUser.Value;
            var result = (await Save(user, userVO)).Cast<User>();
            return result ? result.Ok(user) : result;
        }

        public async Task<Result<User>> FindUserByEmail(string email)
        {
            return await _userRepository.FindByEmail(email);
        }

        public Task<Result<User>> FindUserById(long id)
        {
            return _userRepository.FindById(id);
        }

        private async Task<Result> Save(User user, UserVO userVO)
        {
            user.Email = userVO.Email;
            user.FirstName = userVO.FirstName;
            user.LastName = userVO.LastName;
            user.Password = userVO.Password;

            var result = await _userRepository.Save(user);
            return result;
        }

        public async Task<IResult> Delete(long userId)
        {
            return await _userRepository.Delete(userId);
        }
    }
}
