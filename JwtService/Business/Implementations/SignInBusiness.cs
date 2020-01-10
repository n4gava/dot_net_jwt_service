using JwtService.Business.Interfaces;
using JwtService.Commons;
using JwtService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Business.Implementations
{
    public class SignInBusiness : ISignInBusiness
    {
        IUserBusiness _userBusiness;
        public SignInBusiness(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        public async Task<Result> Login(string email, string password)
        {
            var result = new Result();
            var user = (await _userBusiness.FindUserByEmail(email)).Value;
            
            if (user == null || user.Password != password)
                result.Add("Invalid username or password");
                
            return result;
        }
    }
}
