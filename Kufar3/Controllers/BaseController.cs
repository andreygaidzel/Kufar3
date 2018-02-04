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

        public void InitDropDownItems(int selectedSubCategory = 1, int selectedCity = 1)
        {
            var categories = new SelectList(Context.Categories, "Id", "Name", selectedSubCategory);
            var subCategories = new SelectList(Context.SubCategories.Where(c => c.CategoryId == selectedSubCategory), "Id", "Name");

            var regions = new SelectList(Context.Regions, "Id", "Name", selectedCity);
            var cities = new SelectList(Context.Cities.Where(c => c.RegionId == selectedCity), "Id", "Name");

            ViewBag.categories = categories;
            ViewBag.subCategories = subCategories;

            ViewBag.regions = regions;
            ViewBag.cities = cities;
        }
    }
}