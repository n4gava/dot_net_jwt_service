using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtService.Business.Interfaces;
using JwtService.Commons;
using JwtService.Commons.Interfaces;
using JwtService.Controllers.Attributes;
using JwtService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModelState]
    [ResultRequest]
    public class AuthController : ControllerBase
    {
        SignInManager<IdentityUser> _signInManager;
        ITokenBusiness _tokenBusiness;
        IUserBusiness _userBusiness;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            ITokenBusiness tokenBusiness,
            IUserBusiness userBusiness
            )
        {
            _signInManager = signInManager;
            _tokenBusiness = tokenBusiness;
            _userBusiness = userBusiness;
        }

        [HttpPost("register")]
        public async Task<IResult> Register(RegisterUser registerUser)
        {
            var createUser = await _userBusiness.CreateUser(registerUser.Email, registerUser.Password);

            if (!createUser)
                return createUser;

            var user = createUser.Value;

            await _signInManager.SignInAsync(user, false);
            return await GetAuthResponse(user.Email);
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginUser loginUser)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (!result.Succeeded) 
                return new Result("Invalid username or password");

            return await GetAuthResponse(loginUser.Email);
        }

        private async Task<Result<AuthResponse>> GetAuthResponse(string userEmail)
        {
            var resultAuthResponse = new Result<AuthResponse>();
            var resultFindUser = await _userBusiness.FindUserByEmail(userEmail);

            if (!resultFindUser)
                return resultAuthResponse.Add(resultFindUser);

            var user = resultFindUser.Value;
            var resultGenerateToken = await _tokenBusiness.GenerateTokenByUser(user);

            if (!resultGenerateToken)
                return resultAuthResponse.Add(resultGenerateToken);

            var authResponse = new AuthResponse()
            {
                Email = user.Email,
                Username = user.UserName,
                Token = resultGenerateToken.Value
            };

            return resultAuthResponse.Ok(authResponse);
        }
    }
}
