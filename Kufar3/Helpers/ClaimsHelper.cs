using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Helpers
{
    public static class ClaimsHelper
    {
        public static UserRoles GetClaimRole(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity) identity;
            var claims = claimsIdentity.Claims.ToList();
            var clam = claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType);

            if (clam == null)
            {
                return UserRoles.None;
            }
            
            return (UserRoles) Enum.Parse(typeof(UserRoles), clam.Value);
        }
    }
}