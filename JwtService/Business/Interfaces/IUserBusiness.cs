using JwtService.Commons;
using JwtService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtService.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<Result<IdentityUser>> CreateUser(string email, string password);

        Task<Result<IdentityUser>> FindUserByEmail(string email);

        Task<Result<ClaimsIdentity>> FindClaimsByEmail(string email);

        Task<Result<ClaimsIdentity>> FindClaimsByUser(IdentityUser user);
    }
}
