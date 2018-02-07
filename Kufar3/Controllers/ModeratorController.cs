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
            var declarations = Context.Declarations.Where(x=>x.DeclarationType == DeclarationTypes.OnModeration).ToList();
            ViewBag.Declarations = declarations;

            return View();
        }

        public ActionResult DeclarationModeration(int? declarationId)
        {
            var declaration = Context.Declarations.FirstOrDefault(x => x.Id == declarationId);
            return View(declaration);
        }

        public ActionResult DeclarationSend(int declarationId, DeclarationTypes declarationType)
        {
            var declaration = Context.Declarations.First(x => x.Id == declarationId);

            declaration.DeclarationType = declarationType;
            Context.SaveChanges();

            return RedirectToAction("DeclarationList", "Moderator");
        }
    }
}