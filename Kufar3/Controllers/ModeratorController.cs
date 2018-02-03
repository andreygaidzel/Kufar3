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

        //TODO: накой это тут?
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

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
            // TODO: 2 раза извлекается одно и тоже
            // TODO: FirstOrDefault ???
            Declaration declaration = _context.Declarations.FirstOrDefault(x => x.Id == declarationId);
            ViewBag.declaration = declaration;
            // TODO: FirstOrDefault ???
            Declaration model = _context.Declarations.FirstOrDefault(u => u.Id == declarationId);

            // TODO: регистр
            int ColIm0 = 6 - model.Images.Count;
            for (int i = 0; i < ColIm0; i++)
            {
                model.Images.Add(new Image
                {
                    Name = "/Images/null.jpg",
                    DeclarationId = model.Id,
                });
            }

            int selectedIndex = model.SubCategory.CategoryId;

            SelectList categories = new SelectList(_context.Categories, "Id", "Name", selectedIndex);
            SelectList subCat = new SelectList(_context.SubCategories.Where(c => c.CategoryId == selectedIndex), "Id", "Name");

            ViewBag.cats = categories;
            ViewBag.subCat = subCat;

            selectedIndex = model.City.RegionId;

            SelectList regions = new SelectList(_context.Regions, "Id", "Name", selectedIndex);
            SelectList cities = new SelectList(_context.Cities.Where(c => c.RegionId == selectedIndex), "Id", "Name");

            ViewBag.regions = regions;
            ViewBag.cities = cities;

            return View(model);
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

            // TODO: шо это?
            //for (int i = 0; i < declaration.Images.Count; i++)
            //{
            //    if (declaration.Images[i] != null && declaration.Images[i] != "")
            //    {
            //        _context.Images.Add(new Image
            //        {
            //            Name = declaration.Images[i],
            //            DeclarationId = newDeclaration.Id,
            //        });
            //    }
            //}
            //_context.SaveChanges();

            return RedirectToAction("DeclarationList");
        }

        public JsonResult DeleteImage(string url)
        {
            var test = AppDomain.CurrentDomain.BaseDirectory;
            var templatePath = test + url;
            System.IO.File.Delete(templatePath);

            // TODO: FirstOrDefault?
            Image delImg = _context.Images.FirstOrDefault(x => x.Name == url);
            _context.Images.Remove(delImg);
            _context.SaveChanges();

            return Json("Удалено", JsonRequestBehavior.AllowGet);
        }
    }
}