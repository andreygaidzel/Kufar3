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
            ViewBag.declarations = declarations;

            return View();
        }

        public ActionResult DeclarationModeration(int? declarationId)
        {
            var declaration = Context.Declarations.FirstOrDefault(x => x.Id == declarationId);
            ViewBag.declaration = declaration;
            return View();
        }

        public ActionResult DeclarationSend(bool flag, int declarationId)
        {
            var declaration = Context.Declarations.FirstOrDefault(x => x.Id == declarationId);
            if (flag == true)
            {
                declaration.DeclarationType = DeclarationTypes.Active;
            }
            else
            {
                declaration.DeclarationType = DeclarationTypes.Rejected;
            }

            Context.SaveChanges();

            return RedirectToAction("DeclarationList", "Moderator");
        }
    }
}