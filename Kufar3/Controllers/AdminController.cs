using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kufar3.Models;
using Kufar3.Models;
using Kufar3.ModelsView;
using Microsoft.Owin.Security;

namespace Kufar3.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private KufarContext _context;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AdminController()
        {
            _context = new KufarContext();
        }
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            List<User> users = _context.Users.ToList();
            ViewBag.Users = users;

            List<Role> role = _context.Roles.ToList();
            ViewBag.roles = role;

            return View();
        }

        public ActionResult UsersChangeRole(int userId)
        {
            // TODO: Include ???
            //User user = _context.Users.First(x => x.Id == userId);
            User user = _context.Users
                .Include(u => u.Role)
                .First(u => u.Id == userId);

            if (user.Role.Name == "admin")
            {
                user.RoleId = _context.Roles.First(x => x.Name == "moderator").Id;
            }
            if (user.Role.Name == "moderator")
            {
                user.RoleId = _context.Roles.First(x => x.Name == "user").Id;
            }
            if (user.Role.Name == "user")
            {
                user.RoleId = _context.Roles.First(x => x.Name == "admin").Id;
            }

            _context.SaveChanges();

            return RedirectToAction("Users", "Admin");
        }

        public ActionResult UserRemove(int userId)
        {
            User user = _context.Users.First(u => u.Id == userId);

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Users", "Admin");
        }

        public ActionResult UserChange(int userId)
        {
            // TODO: Include ???
            User model = _context.Users
                .Include(u => u.Role)
                .First(u => u.Id == userId);

            return View(model);
        }

        [HttpPost]
        public ActionResult UserChange(User model)
        {
            User user = _context.Users.First(x => x.Id == model.Id);
           
            user.Email = model.Email;
            user.Name = model.Name;
            user.MobileNumber = model.MobileNumber;
            user.Password = model.Password;

            _context.SaveChanges();

            return RedirectToAction("Users", "Admin");
        }
    }
}