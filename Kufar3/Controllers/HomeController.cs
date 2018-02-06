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
        [ChildActionOnly]
        public ActionResult MenuLeft()
        {
            var categories = Context.Categories.ToList();
            return PartialView(categories);
        }

        [HttpGet]
        public ActionResult Index(int? idCategory, int? idSubCategory)
        {
            var title = "Все категории";
            var query = Context.Declarations.Where(x => x.DeclarationType == DeclarationTypes.Active);
           
            if (idCategory != null)
            {
                query = query.Where(x => x.SubCategory.CategoryId == idCategory);
                title = Context.Categories.First(x => x.Id == idCategory).Name;
            }
            else if (idSubCategory != null)
            {
                query = query.Where(x => x.SubCategoryId == idSubCategory);
                title = Context.SubCategories.First(x => x.Id == idSubCategory).Name;
            }

            ViewBag.title = title;
            ViewBag.Declarations = query.ToList();
            ViewBag.idCategory = idCategory;
            ViewBag.idSubCategory = idSubCategory;
            //return View();
            return RedirectToAction("Declarations");
        }

        [HttpGet]
        public ActionResult Declarations(int? idCategory, int? idSubCategory)
        {
            return RedirectToAction("Index", new { idCategory , idSubCategory });
        }

        public ActionResult Declaration(int? declarationId)
        {
            var declaration = Context.Declarations.FirstOrDefault(x => x.Id == declarationId);
            ViewBag.declaration = declaration;
            return View();
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(Context.SubCategories.Where(c => c.CategoryId == id).ToList());
        }

        public ActionResult GetCities(int id)
        {
            return PartialView(Context.Cities.Where(c => c.RegionId == id).ToList());
        }
    }
}