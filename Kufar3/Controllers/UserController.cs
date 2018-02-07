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
        public ActionResult MyDeclarations(DeclarationTypes declarationTypes = DeclarationTypes.Active)
        {
            var query = Context.Declarations.Where(x =>x.UserId == UserId);
            var activCount = query.Count(x => x.DeclarationType == DeclarationTypes.Active);
            var moderCount = query.Count(x => x.DeclarationType == DeclarationTypes.OnModeration);
            var rejCount = query.Count(x => x.DeclarationType == DeclarationTypes.Rejected);
            var countDeclaration = new List<int> {activCount, moderCount, rejCount};

            switch (declarationTypes)
            {
                case DeclarationTypes.Active:
                    query = query.Where(x => x.DeclarationType == DeclarationTypes.Active);
                    break;
                case DeclarationTypes.OnModeration:
                    query = query.Where(x => x.DeclarationType == DeclarationTypes.OnModeration);
                    break;
                case DeclarationTypes.Rejected:
                    query = query.Where(x => x.DeclarationType == DeclarationTypes.Rejected);
                    break;
            }
            ViewBag.Declarations = query.ToList();

            return View(countDeclaration);
        }
        
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
                    Name = "/ContentImages/null.jpg",
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
            newDeclaration.DeclarationType = DeclarationTypes.OnModeration;
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
            var model = Context.Users.First(u => u.Id == UserId);

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