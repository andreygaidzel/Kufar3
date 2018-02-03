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
    public class UserController : Controller
    {
        // GET: User
        private KufarContext _context;

        public UserController()
        {
            _context = new KufarContext();
        }

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
            newDeclaration.Moderation = false;
            newDeclaration.CityId = declaration.CityId;

            _context.SaveChanges();

            return RedirectToAction("MyDeclorations");
        }

        public ActionResult DeleteImage(int? declarationId)
        {
            // TODO: FirstOrDefault ???
            Declaration declaration = _context.Declarations.FirstOrDefault(x => x.Id == declarationId);
            _context.Declarations.Remove(declaration);
            _context.SaveChanges();

            return RedirectToAction("MyDeclorations");
        }
        /********************************************************************************************************/

        public ActionResult AccountEdit()
        {
            // TODO: всегда используй int.Parse() - делает тоже самое, но выкинет ошибку если строка имеет не верный формат
            int userId = Convert.ToInt32(HttpContext.User.Identity.GetUserId());

            // TODO: Include ???
            User model = _context.Users
                .Include(u => u.Role)
                .First(u => u.Id == userId);

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