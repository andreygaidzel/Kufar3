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
            var users = Context.Users.ToList();
            ViewBag.Users = users;

            var role = Context.Roles.ToList();
            ViewBag.roles = role;

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult UsersChangeRole(int userId)
        {
            var user = Context.Users.First(u => u.Id == userId);

            if (user.Role.Name == "admin")
            {
                user.RoleId = Context.Roles.First(x => x.Name == "moderator").Id;
            }
            if (user.Role.Name == "moderator")
            {
                user.RoleId = Context.Roles.First(x => x.Name == "user").Id;
            }
            if (user.Role.Name == "user")
            {
                user.RoleId = Context.Roles.First(x => x.Name == "admin").Id;
            }

            Context.SaveChanges();

            return RedirectToAction("Users", "Admin");
        }

        [Authorize(Roles = "admin")]
        public ActionResult UserRemove(int userId)
        {
            var user = Context.Users.First(u => u.Id == userId);

            Context.Users.Remove(user);
            Context.SaveChanges();

            return RedirectToAction("Users", "Admin");
        }

        [Authorize(Roles = "admin")]
        public ActionResult UserChange(int userId)
        {
            var model = Context.Users.First(u => u.Id == userId);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult UserChange(User model)
        {
            var user = Context.Users.First(x => x.Id == model.Id);
           
            user.Email = model.Email;
            user.Name = model.Name;
            user.MobileNumber = model.MobileNumber;
            user.Password = model.Password;

            Context.SaveChanges();

            return RedirectToAction("Users", "Admin");
        }
    }
}