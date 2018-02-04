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
    public class ModeratorController : BaseController
    {    
        public ActionResult DeclarationList()
        {
            var declarations = Context.Declarations.Where(x=>x.Moderation == false).ToList();
            ViewBag.declarations = declarations;

            return View();
        }

        [HttpGet]
        public ActionResult DeclarationModeration(int? declarationId)
        {
            var declaration = Context.Declarations.First(x => x.Id == declarationId);
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

            var categories = new SelectList(Context.Categories, "Id", "Name", selectedIndex);
            var subCategories = new SelectList(Context.SubCategories.Where(c => c.CategoryId == selectedIndex), "Id", "Name");

            ViewBag.categories = categories;
            ViewBag.subCategories = subCategories;

            selectedIndex = declaration.City.RegionId;

            var regions = new SelectList(Context.Regions, "Id", "Name", selectedIndex);
            var cities = new SelectList(Context.Cities.Where(c => c.RegionId == selectedIndex), "Id", "Name");

            ViewBag.regions = regions;
            ViewBag.cities = cities;

            return View(declaration);
        }

        [HttpPost]
        public ActionResult DeclarationUpdate(Declaration declaration)
        {
            var newDeclaration = Context.Declarations.First(x => x.Id == declaration.Id);

            newDeclaration.Name = declaration.Name;
            newDeclaration.Description = declaration.Description;
            newDeclaration.SubCategoryId = declaration.SubCategoryId;
            newDeclaration.Moderation = true;
            newDeclaration.CityId = declaration.CityId;
          
            Context.SaveChanges();

            return RedirectToAction("DeclarationList");
        }

        public JsonResult DeleteImage(string url)
        {
            var test = AppDomain.CurrentDomain.BaseDirectory;
            var templatePath = test + url;
            System.IO.File.Delete(templatePath);

            var delImg = Context.Images.First(x => x.Name == url);
            Context.Images.Remove(delImg);
            Context.SaveChanges();

            return Json("Удалено", JsonRequestBehavior.AllowGet);
        }
    }
}