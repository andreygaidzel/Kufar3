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
            var declarations = _context.Declarations.Where(x=>x.Moderation == false).ToList();
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
            var declaration = _context.Declarations.First(x => x.Id == declarationId);
            ViewBag.declaration = declaration;
            
            var countEmptyImages = 6 - declaration.Images.Count;
            for (var i = 0; i < countEmptyImages; i++)
            {
                declaration.Images.Add(new Image
                {
                    Name = "/Images/null.jpg",
                    DeclarationId = declaration.Id,
                });
            }

            var selectedIndex = declaration.SubCategory.CategoryId;

            var categories = new SelectList(_context.Categories, "Id", "Name", selectedIndex);
            var subCategories = new SelectList(_context.SubCategories.Where(c => c.CategoryId == selectedIndex), "Id", "Name");

            ViewBag.categories = categories;
            ViewBag.subCategories = subCategories;

            selectedIndex = declaration.City.RegionId;

            var regions = new SelectList(_context.Regions, "Id", "Name", selectedIndex);
            var cities = new SelectList(_context.Cities.Where(c => c.RegionId == selectedIndex), "Id", "Name");

            ViewBag.regions = regions;
            ViewBag.cities = cities;

            return View(declaration);
        }

        [HttpPost]
        public ActionResult DeclarationUpdate(Declaration declaration)
        {
            var newDeclaration = _context.Declarations.First(x => x.Id == declaration.Id);

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

            var delImg = _context.Images.First(x => x.Name == url);
            _context.Images.Remove(delImg);
            _context.SaveChanges();

            return Json("Удалено", JsonRequestBehavior.AllowGet);
        }
    }
}