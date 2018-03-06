using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.Filters;
using Kufar3.Models;
using Microsoft.SqlServer.Server;
using Microsoft.AspNet.Identity;

namespace Kufar3.Controllers
{
    [MyAuthorize]
    public class PersonalController : BaseController
    {
        public PersonalController()
        {
            ViewBag.Directory = DirectoryTypes.PersonalDeclarations;
        }

        public ActionResult MyDeclarations(DeclarationTypes declarationType = DeclarationTypes.Active, int page = 1)
        {
            var query = DeclarationRepository.GetDeclarationsByUserId(UserId);
            int pageSize = 5;   // количество элементов на странице 
            ViewBag.ActiveCount = query.Count(x => x.Type == DeclarationTypes.Active);
            ViewBag.ModerationCount = query.Count(x => x.Type == DeclarationTypes.OnModeration);
            ViewBag.RejectedCount = query.Count(x => x.Type == DeclarationTypes.Rejected);

            switch (declarationType)
            {
                case DeclarationTypes.Active:
                    query = query.Where(x => x.Type == DeclarationTypes.Active);
                    break;
                case DeclarationTypes.OnModeration:
                    query = query.Where(x => x.Type == DeclarationTypes.OnModeration);
                    break;
                case DeclarationTypes.Rejected:
                    query = query.Where(x => x.Type == DeclarationTypes.Rejected);
                    break;
            }

            var count = query.Count();

            var items = query
                .OrderBy(x => x.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.DeclarationType = declarationType;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
            ViewBag.Count = count;
            ViewBag.Declarations = items;

            return View();
        }

        [HttpGet]
        public ActionResult UserDeclaration(long? declarationId)
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
            ViewBag.Directory = DirectoryTypes.PersonalSettings;

            return View(model);
        }

        [HttpPost]
        public ActionResult AccountEdit(User model)
        {
            UserRepository.Edit(model);
            ViewBag.Directory = DirectoryTypes.PersonalSettings;

            return View();
        }
    }
}