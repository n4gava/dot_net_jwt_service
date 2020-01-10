using JwtService.Commons;
using JwtService.Commons.Interfaces;
using JwtService.Entities;
using JwtService.Models.User;
using System.Threading.Tasks;

namespace JwtService.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<Result<User>> Save(UserVO user);

        Task<Result<User>> Save(long id, UserVO userVO);

        Task<Result<User>> FindUserByEmail(string email);

        Task<Result<User>> FindUserById(long id);
        Task<IResult> Delete(long userId);
    }
}
