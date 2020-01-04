using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtService.Authentication.AuthFilter
{
    public class ClaimAuthFilter : IAuthorizationFilter
    {
        Claim _claim;

        public ClaimAuthFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
                context.Result = new StatusCodeResult(401);
            else if (!ValidateClaim(context.HttpContext.User.Claims))
                context.Result = new StatusCodeResult(403);
        }

        private bool ValidateClaim(IEnumerable<Claim> userClaims)
        {
            return userClaims.Any(claim => claim.Type == _claim.Type && claim.Value == _claim.Value);
        }
    }
}
