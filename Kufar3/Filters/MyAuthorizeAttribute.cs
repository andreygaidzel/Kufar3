using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.Helpers;
using Kufar3.Models;

namespace Kufar3.Filters
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly List<string> _userRoles = new List<string>();

        public MyAuthorizeAttribute(params UserRoles[] userRoles)
        {
            userRoles
                .ToList()
                .ForEach(userRole => _userRoles.Add(userRole.ToString()));
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var identity = httpContext.User.Identity;

            if (!_userRoles.Any() && identity.IsAuthenticated)
            {
                return true;
            }

            var thisUserRole = identity.GetClaimRole();
            
            return _userRoles.Any(userRole => userRole == thisUserRole);
        }
    }
}