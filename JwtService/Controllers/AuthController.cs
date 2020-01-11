using System.Threading.Tasks;
using JwtService.Business.Interfaces;
using JwtService.Commons;
using JwtService.Commons.Interfaces;
using JwtService.Controllers.Attributes;
using JwtService.Models;
using JwtService.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace JwtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModelState]
    [ResultRequest]
    public class AuthController : ControllerBase
    {
        ITokenBusiness _tokenBusiness;
        IUserBusiness _userBusiness;
        ISignInBusiness _signInBusiness;

        public AuthController(
            ITokenBusiness tokenBusiness,
            IUserBusiness userBusiness,
            ISignInBusiness signInBusiness
            )
        {
            _tokenBusiness = tokenBusiness;
            _userBusiness = userBusiness;
            _signInBusiness = signInBusiness;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Result<AuthResponse>), 200)]
        [ProducesResponseType(typeof(Result<AuthResponse>), 400)]
        public async Task<IResult> Login(LoginUser loginUser)
        {
            var result = await _signInBusiness.Login(loginUser.Email, loginUser.Password);

            if (!result)
                return result;

            return await GetAuthResponse(loginUser.Email);
        }

        private async Task<Result<AuthResponse>> GetAuthResponse(string userEmail)
        {
            var resultFindUser = await _userBusiness.FindUserByEmail(userEmail);

            if (!resultFindUser)
                return resultFindUser.Cast<AuthResponse>();

            var user = resultFindUser.Value;
            var resultGenerateToken = await _tokenBusiness.GenerateTokenByUser(user);

            if (!resultGenerateToken)
                return resultGenerateToken.Cast<AuthResponse>();

            var authResponse = new AuthResponse()
            {
                Email = user.Email,
                Token = resultGenerateToken.Value
            };

            return authResponse.ToResult();
        }
    }
}
