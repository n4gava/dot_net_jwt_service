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
        IUserBusiness _userBusiness;
        IUserTokenRepository _userTokenRepository;

        public TokenBusiness(
            IOptions<AppSettings> appSettings,
            IUserBusiness userBusiness,
            IUserTokenRepository userTokenRepository)
        {
            _appSettings = appSettings.Value;
            _userBusiness = userBusiness;
            _userTokenRepository = userTokenRepository;
        }

        public async Task<Result<string>> GenerateTokenByUser(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var result = new Result<string>();
            var resultClaimsIdentity = await _userBusiness.FindClaimsByUser(user);

            result.Add(resultClaimsIdentity);

            if (!result)
                return result;

            var tokenDescription = GetSecurityTokenDescriptor(resultClaimsIdentity.Value);

            string token = GenerateToken(tokenDescription);

            _userTokenRepository.Save(GetUserToken(user, token));

            return result.Ok(token);
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(ClaimsIdentity subject)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = subject,
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

        private UserToken GetUserToken(IdentityUser user, string token)
        {
            return new UserToken()
            {
                CreatedOn = DateTimeOffset.UtcNow,
                Username = user.UserName,
                ExpirationMinutes = _appSettings.ExpirationMinutes,
                Token = token
            };
        }

    }
}
