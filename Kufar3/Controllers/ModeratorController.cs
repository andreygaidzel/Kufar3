using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.Helpers;
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
            var declarations = DeclarationRepository.GetDeclarationsByDeclarationType(DeclarationTypes.OnModeration).ToList();
            ViewBag.Declarations = declarations;

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