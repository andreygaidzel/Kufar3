using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.Filters;
using Kufar3.Helpers;
using Kufar3.Models;
using Kufar3.ModelsView;
using Microsoft.Owin.Security;

namespace Kufar3.Controllers
{
    [MyAuthorize(UserRoles.Admin, UserRoles.Moderator)]
    public class ModeratorController : BaseController
    {
        public ModeratorController()
        {
             ViewBag.Directory = DirectoryTypes.Moderator;
        }

        public ActionResult DeclarationList(int page = 1)
        {
            var declarations = DeclarationRepository.GetDeclarationsByDeclarationType(DeclarationTypes.OnModeration).ToList();
            int pageSize = 5;   // количество элементов на странице 

            var count = declarations.Count();

            var items = declarations
                .OrderBy(x => x.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
            ViewBag.Count = count;
            ViewBag.Declarations = items;

            return View();
        }

        public ActionResult DeclarationModeration(int? declarationId)
        {
            var declaration = DeclarationRepository.GetById(declarationId);
            return View(declaration);
        }

        public ActionResult DeclarationSend(int declarationId, DeclarationTypes declarationType)
        {
            DeclarationRepository.EditDeclarationType(declarationId, declarationType);

            return RedirectToAction("DeclarationList", "Moderator");
        }
    }
}