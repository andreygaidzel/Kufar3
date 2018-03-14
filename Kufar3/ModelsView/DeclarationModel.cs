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

        [Required(ErrorMessage = "Заполните поле")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 20 символов")]
        [Display(Name = "Название")]
        public string NameD { get; set; }

        [Required(ErrorMessage = "Заполните поле")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Описание должно быть от 10 до 100 символов")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public long SubCategoryId { get; set; }
        public long CityId { get; set; }
        public List<string> Images  { get; set; }
    }
}