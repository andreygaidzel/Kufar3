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
    public class UserController : BaseController
    {
        public ActionResult MyDeclarations()
        {
            var userId = Convert.ToInt32(HttpContext.User.Identity.GetUserId());
            var query = Context.Declarations.Where(x =>x.UserId == userId);
            ViewBag.Declarations = query.ToList();

            return View();
        }

        // TODO: исправить
        [HttpGet]
        public ActionResult UserDeclaration(int? declarationId)
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
            InitDropDownItems(declaration.SubCategory.CategoryId, declaration.City.RegionId);

            return View(declaration);
        }

        [HttpPost]
        public ActionResult DeclarationUpdate(Declaration declaration)
        {
            var newDeclaration = Context.Declarations.First(x => x.Id == declaration.Id);

            newDeclaration.Name = declaration.Name;
            newDeclaration.Description = declaration.Description;
            newDeclaration.SubCategoryId = declaration.SubCategoryId;
            newDeclaration.Moderation = false;
            newDeclaration.CityId = declaration.CityId;

            Context.SaveChanges();

            return RedirectToAction("MyDeclarations");
        }

        public ActionResult DeleteImage(int? declarationId)
        {
            var declaration = Context.Declarations.First(x => x.Id == declarationId);
            Context.Declarations.Remove(declaration);
            Context.SaveChanges();

            return RedirectToAction("MyDeclarations");
        }
        /********************************************************************************************************/

        public ActionResult AccountEdit()
        {
            var userId = int.Parse(HttpContext.User.Identity.GetUserId());
            var model = Context.Users.First(u => u.Id == userId);

            return View(model);
        }

        [HttpPost]
        public ActionResult AccountEdit(User model)
        {
            var user = Context.Users.First(x => x.Id == model.Id);

            user.Email = model.Email;
            user.Name = model.Name;
            user.MobileNumber = model.MobileNumber;
            user.Password = model.Password;

            Context.SaveChanges();

            return RedirectToAction("AccountEdit", "User");
        }
    }
}