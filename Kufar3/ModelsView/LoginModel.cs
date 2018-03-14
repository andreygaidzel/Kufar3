using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kufar3.ModelsView
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Заполните поле")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните поле")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 12 символов")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}