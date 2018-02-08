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
        public ActionResult MyDeclarations(DeclarationTypes declarationType = DeclarationTypes.Active)
        {
            var query = DeclarationRepository.GetDeclarationsByUserId(UserId);
            ViewBag.ActivCount = query.Count(x => x.DeclarationType == DeclarationTypes.Active);
            ViewBag.ModerCount = query.Count(x => x.DeclarationType == DeclarationTypes.OnModeration);
            ViewBag.RejCount = query.Count(x => x.DeclarationType == DeclarationTypes.Rejected);

            switch (declarationType)
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

            var declarations = query.ToList();
            ViewBag.Declarations = declarations;

            return View();
        }
        
        [HttpGet]
        public ActionResult UserDeclaration(int? declarationId)
        {
            var declaration = DeclarationRepository.GetById(declarationId);
            ViewBag.Declaration = declaration;

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
            DeclarationRepository.Update(declaration);

            return RedirectToAction("MyDeclarations");
        }

        public ActionResult DeleteDeclaration(int? declarationId)
        {
            DeclarationRepository.Remove(declarationId);

            return RedirectToAction("MyDeclarations");
        }
        /********************************************************************************************************/

        public ActionResult AccountEdit()
        {
            var model = UserRepository.GetById(UserId);

            return View(model);
        }

        [HttpPost]
        public ActionResult AccountEdit(User model)
        {
           UserRepository.Edit(model);

            return RedirectToAction("AccountEdit", "User");
        }
    }
}