using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kufar3.ModelsView
{
    public class DeclarationModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long SubCategoryId { get; set; }
        public long CityId { get; set; }
        public List<string> Images  { get; set; }
    }
}