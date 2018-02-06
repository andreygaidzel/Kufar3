using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Kufar3.Helpers
{
    public static class ClaimsHelper
    {
        public static string GetClaimRole(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity) identity;
            var claims = claimsIdentity.Claims.ToList();
            var clam = claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType);

            return clam == null ? string.Empty : clam.Value;
        }
    }
}