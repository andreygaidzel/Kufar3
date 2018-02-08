﻿using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kufar3.Helpers;
using Kufar3.Models;
using Kufar3.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Kufar3.Controllers
{
    public class BaseController : Controller
    {
        public KufarContext Context;
        public UserRepository UserRepository;
        public DeclarationRepository DeclarationRepository;
        public CategoryRepository CategoryRepository;
        public RegionRepository RegionRepository;
        public ImageRepository ImageRepository;

        public IAuthenticationManager AuthenticationManager => System.Web.HttpContext.Current.GetOwinContext().Authentication;
        private IPrincipal Principal => System.Web.HttpContext.Current.User;
        public int UserId => Principal.Identity.GetUserId<int>();
        public bool IsAuthenticated => Principal.Identity.IsAuthenticated;
        public string UserName => Principal.Identity.Name;
        public string UserRole => Principal.Identity.GetClaimRole();

        public BaseController()
        {
            Context = new KufarContext();
            UserRepository = new UserRepository();
            DeclarationRepository = new DeclarationRepository();
            CategoryRepository = new CategoryRepository();
            RegionRepository = new RegionRepository();
            ImageRepository = new ImageRepository();
            ViewBags();
        }

        public void ViewBags()
        {
            ViewBag.IsAuthenticated = IsAuthenticated;
            ViewBag.UserId = UserId;
            ViewBag.UserName = UserName;
            ViewBag.UserRole = UserRole;
        }

        public bool IsinRole(string role)
        {
            return Principal.IsInRole(role);
        }

        public void InitDropDownItems(int selectedSubCategory = 1, int selectedCity = 1)
        {
            var categories = new SelectList(CategoryRepository.GetAllCategories(), "Id", "Name", selectedSubCategory);
            var subCategories = new SelectList(CategoryRepository.GetSubCategoriesByCategoryId(selectedSubCategory), "Id", "Name");

            var regions = new SelectList(RegionRepository.GetAllRegions(), "Id", "Name", selectedCity);
            var cities = new SelectList(RegionRepository.GetCitiesByRegionId(selectedCity), "Id", "Name");

            ViewBag.Categories = categories;
            ViewBag.SubCategories = subCategories;

            ViewBag.Regions = regions;
            ViewBag.Cities = cities;
        }
    }
}