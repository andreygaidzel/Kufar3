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

            List<Category> categories = _context.Categories.ToList();
            ViewBag.categories = categories;
        }

        [HttpGet]
        public ActionResult AddDeclaration()
        {
            int selectedIndex = 1;
            SelectList categories = new SelectList(_context.Categories, "Id", "Name", selectedIndex);
            ViewBag.cats = categories;
            SelectList subCat = new SelectList(_context.SubCategories.Where(c => c.CategoryId == selectedIndex), "Id", "Name");
            ViewBag.subCat = subCat;

            SelectList regions = new SelectList(_context.Regions, "Id", "Name", selectedIndex);
            ViewBag.regions = regions;
            SelectList cities = new SelectList(_context.Cities.Where(c => c.RegionId == selectedIndex), "Id", "Name");
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
            string userId = HttpContext.User.Identity.GetUserId();
            int intId = Convert.ToInt32(userId);

            Declaration newDeclaration = new Declaration
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

            foreach (string img in declaration.Images)
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

            int k = 0;

            // TODO: img - всунуть в цикл
            string img = "";
            List<string> imag = new List<string>();
            if (file != null)
            {
                foreach (var f in file)
                {
                    if (f != null)
                    {
                        k++;
                        var random = Guid.NewGuid().ToString("n");
                        // получаем имя файла
                        string fileName = "IMG" + random + "-Num" + k + ".jpg";
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