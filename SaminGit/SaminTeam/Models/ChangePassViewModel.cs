using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaminProject.Models
{
    public class ChangePassViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "لطفا رمز عبور فعلی را وارد نماپید")]
        [MinLength(6, ErrorMessage = "رمز عبور فعلی حداقل 6 کاراکتر می باشد")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا رمز عبور جدید را وارد نماپید")]
        [MinLength(6, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد")]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا تکرار رمز عبور جدید را وارد نماپید")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تکرار رمز عبور جدید با هم یکسان نیستند")]
        public string ReNewPassword { get; set; }




    }
}