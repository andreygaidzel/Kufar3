using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.Models;
using Microsoft.SqlServer.Server;
using Microsoft.AspNet.Identity;

namespace Kufar3.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        private KufarContext _context;

        public UserController()
        {
            _context = new KufarContext();
        }

        // TODO: DeclOOOOrations ???
        public ActionResult MyDeclorations()
        {
            int userId = Convert.ToInt32(HttpContext.User.Identity.GetUserId());
            IQueryable<Declaration> query = _context.Declarations.Where(x =>x.UserId == userId);
            ViewBag.Declarations = query.ToList();

            return View();
        }

        [HttpGet]
        public ActionResult UserDeclaration(int? declarationId)
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
            newDeclaration.Moderation = false;
            newDeclaration.CityId = declaration.CityId;

            _context.SaveChanges();

            return RedirectToAction("MyDeclorations");
        }

        public ActionResult DeleteImage(int? declarationId)
        {
            Declaration declaration = _context.Declarations.First(x => x.Id == declarationId);
            _context.Declarations.Remove(declaration);
            _context.SaveChanges();

            return RedirectToAction("MyDeclorations");
        }
        /********************************************************************************************************/

        public ActionResult AccountEdit()
        {
            int userId = int.Parse(HttpContext.User.Identity.GetUserId());
            User model = _context.Users.First(u => u.Id == userId);

            return View(model);
        }

        [HttpPost]
        public ActionResult AccountEdit(User model)
        {
            User user = _context.Users.First(x => x.Id == model.Id);

            user.Email = model.Email;
            user.Name = model.Name;
            user.MobileNumber = model.MobileNumber;
            user.Password = model.Password;

            _context.SaveChanges();

            return RedirectToAction("AccountEdit", "User");
        }
    }
}