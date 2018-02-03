using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.Models;
using Kufar3.ModelsView;
using Microsoft.Owin.Security;

namespace Kufar3.Controllers
{
    [Authorize(Roles = "moderator")]
    public class ModeratorController : Controller
    {    
        private KufarContext _context;

        public ModeratorController()
        {
            _context = new KufarContext();
        }

        public ActionResult DeclarationList()
        {
            List<Declaration> declarations = _context.Declarations.Where(x=>x.Moderation == false).ToList();
            ViewBag.declarations = declarations;

            return View();
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(_context.SubCategories.Where(c => c.CategoryId == id).ToList());
        }

        public ActionResult GetCities(int id)
        {
            return PartialView(_context.Cities.Where(c => c.RegionId == id).ToList());
        }

        [HttpGet]
        public ActionResult DeclarationModeration(int? declarationId)
        {
            Declaration declaration = _context.Declarations.First(x => x.Id == declarationId);
            ViewBag.declaration = declaration;

            int colIm0 = 6 - declaration.Images.Count;
            for (int i = 0; i < colIm0; i++)
            {
                declaration.Images.Add(new Image
                {
                    Name = "/Images/null.jpg",
                    DeclarationId = declaration.Id,
                });
            }

            int selectedIndex = declaration.SubCategory.CategoryId;

            SelectList categories = new SelectList(_context.Categories, "Id", "Name", selectedIndex);
            SelectList subCat = new SelectList(_context.SubCategories.Where(c => c.CategoryId == selectedIndex), "Id", "Name");

            ViewBag.cats = categories;
            ViewBag.subCat = subCat;

            selectedIndex = declaration.City.RegionId;

            SelectList regions = new SelectList(_context.Regions, "Id", "Name", selectedIndex);
            SelectList cities = new SelectList(_context.Cities.Where(c => c.RegionId == selectedIndex), "Id", "Name");

            ViewBag.regions = regions;
            ViewBag.cities = cities;

            return View(declaration);
        }

        [HttpPost]
        public ActionResult DeclarationUpdate(Declaration declaration)
        {
            Declaration newDeclaration = _context.Declarations.First(x => x.Id == declaration.Id);

            newDeclaration.Name = declaration.Name;
            newDeclaration.Description = declaration.Description;
            newDeclaration.SubCategoryId = declaration.SubCategoryId;
            newDeclaration.Moderation = true;
            newDeclaration.CityId = declaration.CityId;
          
            _context.SaveChanges();

            return RedirectToAction("DeclarationList");
        }

        public JsonResult DeleteImage(string url)
        {
            var test = AppDomain.CurrentDomain.BaseDirectory;
            var templatePath = test + url;
            System.IO.File.Delete(templatePath);

            Image delImg = _context.Images.First(x => x.Name == url);
            _context.Images.Remove(delImg);
            _context.SaveChanges();

            return Json("Удалено", JsonRequestBehavior.AllowGet);
        }
    }
}