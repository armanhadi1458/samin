using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaminProject.Models
{
    public class Customer
    {
        [Key]
        public short? ID { get; set; }

        [Display( Name = "نام")]
        [Required(ErrorMessage = "لطفا نام را وارد نمایید")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Name { get; set; }

        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }

        [Display(Name = "آدرس")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Address { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        public string Email { get; set; }

        [NotMapped]
        [Display(Name = "لوگو مشتری")]
        //[Required(ErrorMessage = "لطفا تصویر انتخاب نمایید")]
        public HttpPostedFileBase ContentFile { get; set; }

        public byte[] Logo { get; set; }

        [NotMapped]
        public string Base64Image
        {
            get
            {
                if (Logo != null)
                    return Convert.ToBase64String(Logo);
                else
                    return string.Empty;
            }
        }
    }
}