using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Kufar3.Helpers;
using Kufar3.Models;
using Microsoft.SqlServer.Server;

namespace Kufar3.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
            ViewBag.Directory = DirectoryTypes.Home;
        }

        [ChildActionOnly]
        public ActionResult MenuLeft()
        {
            var categories = CategoryRepository.GetAllCategories().ToList();
            return PartialView(categories);
        }

        [HttpGet]
        public ActionResult Index(int? idCategory, int? idSubCategory, int page = 1,
            SortTypes sortType = SortTypes.ByDate)
        {
            var title = "Все категории";
            var query = DeclarationRepository.GetDeclarationsByDeclarationType(DeclarationTypes.Active);
            int pageSize = 5; // количество элементов на странице 

            if (idCategory != null)
            {
                query = query.Where(x => x.SubCategory.CategoryId == idCategory);
                title = CategoryRepository.GetById(idCategory).Name;
            }
            else if (idSubCategory != null)
            {
                query = query.Where(x => x.SubCategoryId == idSubCategory);
                title = CategoryRepository.GetSubCategoryById(idSubCategory).Name;
            }

            var count = query.Count();

            switch (sortType)
            {
                case SortTypes.ByDate:
                    query = query.OrderBy(x => x.CreatedDate);
                    break;
                case SortTypes.PriceAsc:
                    query = query.OrderBy(x => x.Price);
                    break;
                case SortTypes.PriceDesc:
                    query = query.OrderByDescending(x => x.Price);
                    break;
            }

            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var sortItems = new List<SelectListItem>
            {
                new SelectListItem {Value = SortTypes.ByDate.ToString(), Text = "По дате добавления"},
                new SelectListItem {Value = SortTypes.PriceAsc.ToString(), Text = "Сначала дешевые"},
                new SelectListItem {Value = SortTypes.PriceDesc.ToString(), Text = "Сначала дорогие"}
            };
            var sortList = new SelectList(sortItems, "Value", "Text", sortType);

            ViewBag.SortList = sortList;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
            ViewBag.Count = count;
            ViewBag.Title = title;
            ViewBag.Declarations = items;
            ViewBag.IdCategory = idCategory;
            ViewBag.IdSubCategory = idSubCategory;

            TempData["IdCategory"] = idCategory;
            TempData["idSubCategory"] = idSubCategory;

            return View();
        }

        [HttpGet]
        public ActionResult Declarations(int? idCategory, int? idSubCategory)
        {
            return RedirectToAction("Index", new {idCategory, idSubCategory});
        }

        public ActionResult Declaration(int? declarationId)
        {
            var declaration = DeclarationRepository.GetById(declarationId);
            return View(declaration);
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(CategoryRepository.GetSubCategoriesByCategoryId(id).ToList());
        }

        public ActionResult GetCities(int id)
        {
            return PartialView(RegionRepository.GetCitiesByRegionId(id));
        }

        public JsonResult SearchValue(string searchWord, int pageSearch = 1)
        {
             var searchPageSize = 10;

            var searchDeclarations = DeclarationRepository.SearchDeclarations(searchWord)
                .OrderByDescending(x => x.CreatedDate);

             var countSearchItems = searchDeclarations.Count();

            var items = searchDeclarations
                .Skip((pageSearch - 1) * searchPageSize)
                .Take(searchPageSize)
                .ToList();
            var html = this.RenderRazorViewToString("SearchValue", items);

            return Json(new { Data = html, SearchPageSize = searchPageSize, PageSearch = pageSearch, CountSearchItems = countSearchItems });
        }
    }
}