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
    public class HomeController : Controller
    {
        private KufarContext _context;

        public HomeController()
        {
            _context = new KufarContext();
        }

        [ChildActionOnly]
        public ActionResult MenuLeft()
        {
            var categories = _context.Categories.ToList();
            return PartialView(categories);
        }

        [HttpGet]
        public ActionResult Index(int? idCategory, int? idSubCategory)
        {
            var title = string.Empty;
            var query = _context.Declarations.Where(x => x.Moderation == true);
           
            if (idCategory != null)
            {
                query = query.Where(x => x.SubCategory.CategoryId == idCategory);
                title = _context.Categories.First(x => x.Id == idCategory).Name;
            }
            else if (idSubCategory != null)
            {
                query = query.Where(x => x.SubCategoryId == idSubCategory);
                title = _context.SubCategories.First(x => x.Id == idSubCategory).Name;
            }

            ViewBag.title = title;
            ViewBag.Declarations = query.ToList();
            ViewBag.idCategory = idCategory;
            ViewBag.idSubCategory = idSubCategory;
            return View();
        }

        [HttpGet]
        public ActionResult Declarations(int? idCategory, int? idSubCategory)
        {
            return RedirectToAction("Index", new { idCategory , idSubCategory });
        }

        public ActionResult Declaration(int? declarationId)
        {
            var declaration = _context.Declarations.FirstOrDefault(x => x.Id == declarationId);
            ViewBag.declaration = declaration;
            return View();
        }
    }
}