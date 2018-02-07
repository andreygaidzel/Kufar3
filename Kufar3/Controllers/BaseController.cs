using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kufar3.Helpers;
using Kufar3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Kufar3.Controllers
{
    public class BaseController : Controller
    {
        public KufarContext Context;
        public IAuthenticationManager AuthenticationManager => System.Web.HttpContext.Current.GetOwinContext().Authentication;
        private IPrincipal Principal => System.Web.HttpContext.Current.User;
        public int UserId => Principal.Identity.GetUserId<int>();
        public bool IsAuthenticated => Principal.Identity.IsAuthenticated;
        public string UserName => Principal.Identity.Name;
        public string UserRole => Principal.Identity.GetClaimRole();

        public BaseController()
        {
            Context = new KufarContext();
            ViewBags();
        }

        public void ViewBags()
        {
            ViewBag.IsAuthenticated = IsAuthenticated;
            ViewBag.UserName = UserName;
            ViewBag.UserRole = UserRole;
        }

        public bool IsinRole(string role)
        {
            return Principal.IsInRole(role);
        }

        public void InitDropDownItems(int selectedSubCategory = 1, int selectedCity = 1)
        {
            var categories = new SelectList(Context.Categories, "Id", "Name", selectedSubCategory);
            var subCategories = new SelectList(Context.SubCategories.Where(c => c.CategoryId == selectedSubCategory), "Id", "Name");

            var regions = new SelectList(Context.Regions, "Id", "Name", selectedCity);
            var cities = new SelectList(Context.Cities.Where(c => c.RegionId == selectedCity), "Id", "Name");

            ViewBag.Categories = categories;
            ViewBag.SubCategories = subCategories;

            ViewBag.Regions = regions;
            ViewBag.Cities = cities;
        }
    }
}