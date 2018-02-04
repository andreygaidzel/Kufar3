using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.Models;
using Microsoft.Owin.Security;

namespace Kufar3.Controllers
{
    public class BaseController : Controller
    {
        public KufarContext Context;
        public IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public BaseController()
        {
            Context = new KufarContext();
        }

    }
}