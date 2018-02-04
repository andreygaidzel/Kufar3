using Kufar3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.ModelsView;
using Microsoft.AspNet.Identity;

namespace Kufar3.Controllers
{
    public class DeclarationController : Controller
    {
        // GET: Declaration
        private KufarContext _context;

        public DeclarationController()
        {
            _context = new KufarContext();

            var categories = _context.Categories.ToList();
            ViewBag.menuCategories = categories;
        }

        [HttpGet]
        public ActionResult AddDeclaration()
        {
            var selectedIndex = 1;
            var categories = new SelectList(_context.Categories, "Id", "Name", selectedIndex);
            var subCategories = new SelectList(_context.SubCategories.Where(c => c.CategoryId == selectedIndex), "Id", "Name");
            ViewBag.categories = categories;
            ViewBag.subCategories = subCategories;

            var regions = new SelectList(_context.Regions, "Id", "Name", selectedIndex);
            var cities = new SelectList(_context.Cities.Where(c => c.RegionId == selectedIndex), "Id", "Name");
            ViewBag.regions = regions;
            ViewBag.cities = cities;

            return View();
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(_context.SubCategories.Where(c => c.CategoryId == id).ToList());
        }

        public ActionResult GetCities(int id)
        {
            return PartialView(_context.Cities.Where(c => c.RegionId == id).ToList());
        }

        [HttpPost]
        public ActionResult AddDeclaration(DeclarationModel declaration)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var intId = Convert.ToInt32(userId);

            var newDeclaration = new Declaration
            {
                Name = declaration.Name,
                Description = declaration.Description,
                SubCategoryId = declaration.SubCategoryId,
                Moderation = false,
                UserId = intId,
                CreateTime = DateTime.Now,
                CityId = declaration.CityId,
            };
            _context.Declarations.Add(newDeclaration);
            _context.SaveChanges();

            foreach (var img in declaration.Images)
            {
                if (!string.IsNullOrEmpty(img))
                {
                    _context.Images.Add(new Image
                    {
                        Name = img,
                        DeclarationId = newDeclaration.Id,
                    });
                }
            }
            _context.SaveChanges();

            return RedirectToAction("AddDeclaration");
        }

        [HttpPost]
        public JsonResult UploadImage(List<HttpPostedFileBase> file)
        {
            System.Threading.Thread.Sleep(2000);

            var k = 0;

            // TODO: img - всунуть в цикл
            var img = "";
            var imag = new List<string>();
            if (file != null)
            {
                foreach (var f in file)
                {
                    if (f != null)
                    {
                        k++;
                        var random = Guid.NewGuid().ToString("n");
                        // получаем имя файла
                        var fileName = "IMG" + random + "-Num" + k + ".jpg";
                        // сохраняем файл в папку Files в проекте
                        f.SaveAs(Server.MapPath("~/Images/" + fileName));
                        img = "/Images/" + fileName;
                        imag.Add(img);
                    }
                }
            }
            return Json(imag);
        }

        public JsonResult DeleteImage(string url)
        {
            var test = AppDomain.CurrentDomain.BaseDirectory;
            var templatePath = test + url;

            System.IO.File.Delete(templatePath);

            return Json("Удалено", JsonRequestBehavior.AllowGet);
        }
    }
}