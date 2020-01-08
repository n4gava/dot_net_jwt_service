using JwtService.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Business.Interfaces
{
    public interface ISignInBusiness
    {
        Task<Result> Login(string email, string password);
    }
}
