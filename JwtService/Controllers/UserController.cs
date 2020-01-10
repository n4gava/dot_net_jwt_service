using JwtService.Business.Interfaces;
using JwtService.Commons.Interfaces;
using JwtService.Controllers.Attributes;
using JwtService.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModelState]
    [ResultRequest]
    //[Authorize]
    public class UserController : ControllerBase
    {
        IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet("{userId}")]
        public async Task<IResult> Get(long userId)
        {
            return await _userBusiness.FindUserById(userId);
        }

        [HttpPost]
        public async Task<IResult> Post(UserVO user)
        {
            return await _userBusiness.Save(user);

        }

        [HttpPost("{userId}")]
        public async Task<IResult> Post(long userId, UserVO user)
        {
            return await _userBusiness.Save(userId, user);

        }
    }
}
