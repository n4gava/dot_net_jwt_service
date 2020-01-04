using JwtService.Authentication.AuthFilter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtService.Authentication.Attributes
{
    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimAuthFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
