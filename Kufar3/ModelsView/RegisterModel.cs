using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Kufar3.ModelsView
{
    public class RegisterModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 15 символов")]
        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [RegularExpression(@"[0-9._%+-]", ErrorMessage = "Некорректный номер")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Поле должно быть от 6 до 15 символов")]
        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Номер")]
        public string MobileNumber { get; set; }

        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 12 символов")]
        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}