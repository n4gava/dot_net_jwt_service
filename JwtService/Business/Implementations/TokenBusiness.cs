using JwtService.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using JwtService.Commons;
using System.Threading.Tasks;
using JwtService.Repositories.Interfaces;
using System.Security.Claims;
using JwtService.Entities;

namespace JwtService.Business.Implementations
{
    public class TokenBusiness : ITokenBusiness
    {
        AppSettings _appSettings;
        IUserTokenRepository _userTokenRepository;

        public TokenBusiness(
            IOptions<AppSettings> appSettings,
            IUserTokenRepository userTokenRepository)
        {
            _appSettings = appSettings.Value;
            _userTokenRepository = userTokenRepository;
        }

        public async Task<Result<string>> GenerateTokenByUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var result = new Result<string>();

            if (!result)
                return result;

            var tokenDescription = GetSecurityTokenDescriptor();

            string token = GenerateToken(tokenDescription);

            var resultDeleteTokens = await _userTokenRepository.DeleteByEmail(user.Email);
            if (!resultDeleteTokens)
                return resultDeleteTokens.Cast<string>();

            result.Add(await _userTokenRepository.Save(GetUserToken(user, token)));

            return result ? result.Ok(token) : result;
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor()
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescription;
        }

        private string GenerateToken(SecurityTokenDescriptor tokenDescription)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            return tokenHandle.WriteToken(tokenHandle.CreateToken(tokenDescription));
        }

        private UserToken GetUserToken(User user, string token)
        {
            return new UserToken()
            {
                CreatedOn = DateTimeOffset.UtcNow,
                Email = user.Email,
                ExpirationMinutes = _appSettings.ExpirationMinutes,
                Token = token
            };
        }

    }
}
