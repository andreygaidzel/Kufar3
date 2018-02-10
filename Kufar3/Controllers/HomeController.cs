using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.Models;
using Microsoft.SqlServer.Server;

namespace Kufar3.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
            ViewBag.Directory = DirectoryTypes.Home;
        }

        [ChildActionOnly]
        public ActionResult MenuLeft()
        {
            var categories = CategoryRepository.GetAllCategories().ToList();
            return PartialView(categories);
        }

        [HttpGet]
        public ActionResult Index(int? idCategory, int? idSubCategory)
        {
            var title = "Все категории";
            var query = DeclarationRepository.GetDeclarationsByDeclarationType(DeclarationTypes.Active);
           
            if (idCategory != null)
            {
                query = query.Where(x => x.SubCategory.CategoryId == idCategory);
                title = CategoryRepository.GetById(idCategory).Name;
            }
            else if (idSubCategory != null)
            {
                query = query.Where(x => x.SubCategoryId == idSubCategory);
                title = CategoryRepository.GetSubCategoryById(idSubCategory).Name;
            }

            ViewBag.Title = title;
            ViewBag.Declarations = query.ToList();
            ViewBag.IdCategory = idCategory;
            ViewBag.IdSubCategory = idSubCategory;
            return View();
        }

        [HttpGet]
        public ActionResult Declarations(int? idCategory, int? idSubCategory)
        {
            return RedirectToAction("Index", new { idCategory , idSubCategory });
        }

        public ActionResult Declaration(int? declarationId)
        {
            var declaration = DeclarationRepository.GetById(declarationId);
            return View(declaration);
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(CategoryRepository.GetSubCategoriesByCategoryId(id).ToList());
        }

        public ActionResult GetCities(int id)
        {
            return PartialView(RegionRepository.GetCitiesByRegionId(id).ToList());
        }
    }
}