using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Repositories
{
    public class CategoryRepository : BaseRepository
    {
        public Category GetById(long? id)
        {
            return Context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public SubCategory GetSubCategoryById(long? id)
        {
            return Context.SubCategories.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<SubCategory> GetSubCategoriesByCategoryId(long? id)
        {
            return Context.SubCategories.Where(x => x.CategoryId == id);
        }

        public IQueryable<Category> GetAllCategories()
        {
            return Context.Categories;
        }
    }
}