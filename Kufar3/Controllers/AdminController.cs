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

            var role = UserRepository.GetAllRoles().ToList();
            ViewBag.Roles = role;

            return View();
        }

        public ActionResult UsersChangeRole(int userId)
        {
            // TODO: ПЕРЕДЕЛАТЬ

            //var user = UserRepository.GetById(userId);

            //switch (user.Role.UserRole)
            //{
            //    case UserRoles.Admin:
            //        user.RoleId = UserRepository.GetRoleIdByName(UserRoles.Moderator).Id;
            //        break;
            //    case UserRoles.Moderator:
            //        user.RoleId = UserRepository.GetRoleIdByName(UserRoles.User).Id;
            //        break;
            //    case UserRoles.User:
            //        user.RoleId = UserRepository.GetRoleIdByName(UserRoles.Admin).Id;
            //        break;
            //}
;
            return RedirectToAction("Users", "Admin");
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