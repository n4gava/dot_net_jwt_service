using JwtService.Business.Interfaces;
using JwtService.Commons;
using JwtService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtService.Business.Implementations
{
    public class UserBusiness : IUserBusiness
    {
        UserManager<IdentityUser> _userManager;

        public UserBusiness(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<IdentityUser>> CreateUser(string email, string password)
        {
            var user = new IdentityUser()
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);

            var userStatus = new Result<IdentityUser>();

            if (!result.Succeeded)
                return userStatus.Add(result.Errors.Select(e => e.Description));
            else
                return userStatus.Ok(user);
        }

        public async Task<Result<IdentityUser>> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var resultFindUser = new Result<IdentityUser>();
            return user != null ? resultFindUser.Ok(user) : resultFindUser.Add("E-mail was not registered.");
        }

        public async Task<Result<ClaimsIdentity>> FindClaimsByEmail(string email)
        {
            var resultFindUser = await FindUserByEmail(email);
            if (!resultFindUser)
                return new Result<ClaimsIdentity>().Add(resultFindUser);

            return await FindClaimsByUser(resultFindUser.Value);
        }

        public async Task<Result<ClaimsIdentity>> FindClaimsByUser(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var claimsIdentity = new ClaimsIdentity(claims);

            return new Result<ClaimsIdentity>().Ok(claimsIdentity);
        }
    }
}
