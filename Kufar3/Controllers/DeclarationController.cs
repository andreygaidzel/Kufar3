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
    public class DeclarationController : BaseController
    {
        [HttpGet]
        public ActionResult AddDeclaration()
        {
            var selectedIndex = 1;
            var categories = new SelectList(Context.Categories, "Id", "Name", selectedIndex);
            var subCategories = new SelectList(Context.SubCategories.Where(c => c.CategoryId == selectedIndex), "Id", "Name");
            ViewBag.categories = categories;
            ViewBag.subCategories = subCategories;

            var regions = new SelectList(Context.Regions, "Id", "Name", selectedIndex);
            var cities = new SelectList(Context.Cities.Where(c => c.RegionId == selectedIndex), "Id", "Name");
            ViewBag.regions = regions;
            ViewBag.cities = cities;

            return View();
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(Context.SubCategories.Where(c => c.CategoryId == id).ToList());
        }

        public ActionResult GetCities(int id)
        {
            return PartialView(Context.Cities.Where(c => c.RegionId == id).ToList());
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
            Context.Declarations.Add(newDeclaration);
            Context.SaveChanges();

            foreach (var img in declaration.Images)
            {
                if (!string.IsNullOrEmpty(img))
                {
                    Context.Images.Add(new Image
                    {
                        Name = img,
                        DeclarationId = newDeclaration.Id,
                    });
                }
            }
            Context.SaveChanges();

            return RedirectToAction("AddDeclaration");
        }

        [HttpPost]
        public JsonResult UploadImage(List<HttpPostedFileBase> file)
        {
            System.Threading.Thread.Sleep(2000);

            var k = 0;

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
                        var img = "/Images/" + fileName;
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