using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Helpers
{
    public static class EnumHelper
    {
        public static UserRoles StringToEnumRole(this string str)
        {
            return (UserRoles) Enum.Parse(typeof(UserRoles), str);
        }
    }
}