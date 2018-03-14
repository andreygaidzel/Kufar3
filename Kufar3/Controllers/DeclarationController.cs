using Kufar3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kufar3.Helpers;
using Kufar3.ModelsView;
using Kufar3.Repositories;
using Microsoft.AspNet.Identity;

namespace Kufar3.Controllers
{
    public class DeclarationController : BaseController
    {
        [HttpGet]
        public ActionResult AddDeclaration()
        {
            InitDropDownItems();
            ViewBag.Directory = DirectoryTypes.Home;

            return View();
        }

        [HttpPost]
        public ActionResult AddDeclaration( DeclarationModel declaration)
        {
            ViewBag.Directory = DirectoryTypes.Home;
            InitDropDownItems(CategoryRepository.GetCategoryIdBySubCategoryId(declaration.SubCategoryId), RegionRepository.GetRegionIdByCityyId(declaration.CityId));
            if (ModelState.IsValid)
            {
                var newDeclaration = new Declaration
                {
                    Name = declaration.NameD,
                    Description = declaration.Description,
                    SubCategoryId = declaration.SubCategoryId,
                    Type = DeclarationTypes.OnModeration,
                    UserId = UserId,
                    CityId = declaration.CityId,
                };

                DeclarationRepository.Add(newDeclaration);

                foreach (var img in declaration.Images)
                {
                    if (!string.IsNullOrEmpty(img))
                    {
                        ImageRepository.Add(new Image
                        {
                            Name = img,
                            DeclarationId = newDeclaration.Id,
                        });
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            return View(declaration);
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
            FilesHelper.DeleteImage(url);

            return Json("Удалено", JsonRequestBehavior.AllowGet);
        }
    }
}