using JwtService.Business.Interfaces;
using JwtService.Commons;
using JwtService.Commons.Interfaces;
using JwtService.Controllers.Attributes;
using JwtService.Entities;
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
    [Authorize]
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

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="user">Dados do usuário.</param>
        /// <returns>Result com os dados do usuário atualizado.</returns>
        /// <response code="200">Retorna os dados do usuário.</response>
        /// <response code="400">Retorna o status com a mensagem de erro</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Result<User>), 200)]
        [ProducesResponseType(typeof(Result<User>), 400)]
        public async Task<IResult> Post(UserVO user)
        {
            return await _userBusiness.Save(user);
        }

        [HttpPost("{userId}")]
        public async Task<IResult> Post(long userId, UserVO user)
        {
            return await _userBusiness.Save(userId, user);

        }

        [HttpDelete("{userId}")]
        public async Task<IResult> Post(long userId)
        {
            return await _userBusiness.Delete(userId);

        }
    }
}
