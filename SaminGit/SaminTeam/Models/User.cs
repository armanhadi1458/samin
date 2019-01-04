using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaminProject.Models
{
    public class User
    {
        public int? ID { get; set; }

        [Display(Name = "نام کاربری")]
        [MaxLength(20, ErrorMessage = "حداکثر 20 کارکتر")]
        [Required(ErrorMessage = "وارد کردن نام کاربری الزامی است")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد کردن کلمه عبور الزامی است")]
        public string Password { get; set; }

    }
}