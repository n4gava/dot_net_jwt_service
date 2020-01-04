using JwtService.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using JwtService.Commons;
using System.Threading.Tasks;

namespace JwtService.Business.Implementations
{
    public class TokenBusiness : ITokenBusiness
    {
        AppSettings _appSettings;
        IUserBusiness _userBusiness;

        public TokenBusiness(
            IOptions<AppSettings> appSettings,
            IUserBusiness userBusiness)
        {
            _appSettings = appSettings.Value;
            _userBusiness = userBusiness;
        }

        public async Task<Result<string>> GenerateTokenByUser(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var result = new Result<string>();
            var resultClaimsIdentity = await _userBusiness.FindClaimsByUser(user);

            result.Add(resultClaimsIdentity);

            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            if (!result)
                return result;

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = resultClaimsIdentity.Value,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            string token = tokenHandle.WriteToken(tokenHandle.CreateToken(tokenDescription));
            return result.Ok(token);
        }
    }
}
