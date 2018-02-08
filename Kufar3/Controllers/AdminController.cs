using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kufar3.Models;
using Microsoft.Owin.Security;

namespace Kufar3.Controllers
{
    [Authorize(Roles = "admin, moderator")]
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Users()
        {
            var users = UserRepository.List();
            ViewBag.Users = users;

            var role = UserRepository.GetAllRoles().ToList();
            ViewBag.Roles = role;

            return View();
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        public ActionResult UserRemove(int userId)
        {
            UserRepository.Remove(userId);

            return RedirectToAction("Users", "Admin");
        }

        [Authorize(Roles = "admin")]
        public ActionResult UserChange(int userId)
        {
            var model = UserRepository.GetById(userId);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult UserChange(User model)
        {
            UserRepository.Update(model);

            return RedirectToAction("Users", "Admin");
        }
    }
}