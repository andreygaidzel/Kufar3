using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kufar3.Filters;
using Kufar3.Models;
using Microsoft.Owin.Security;

namespace Kufar3.Controllers
{
    [MyAuthorize(UserRoles.Admin)]
    public class AdminController : BaseController
    {
        [OverrideFilter]
        [MyAuthorize(UserRoles.Admin, UserRoles.Moderator)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            var users = UserRepository.List();

            ViewBag.Users = users;

            return View();
        }

        [HttpGet]
        public ActionResult UsersChangeRole(int id, string role)
        {
            UserRepository.EditRole(id, role)
;
            return Json("blablasd", JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserRemove(int userId)
        {
            UserRepository.Remove(userId);

            return RedirectToAction("Users", "Admin");
        }

        public ActionResult UserChange(int userId)
        {
            var model = UserRepository.GetById(userId);

            return View(model);
        }

        [HttpPost]
        public ActionResult UserChange(User model)
        {
            UserRepository.Update(model);

            return RedirectToAction("Users", "Admin");
        }
    }
}