using SaminProject.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaminProject.Models
{
    public class Contact
    {
        [Key]
        public int? ID { get; set; }

        [Display(Name = "نام کامل")]
        [MaxLength(100, ErrorMessage = "حداکثر مجاز 100 کارکتر می باشد")]
        [Required(ErrorMessage = "وارد کردن نام کامل الزامی است")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "حداکثر مجاز 200 کارکتر می باشد")]
        [Required(ErrorMessage = "وارد کردن ایمیل الزامی است")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Email { get; set; }

        [Display(Name = "موضوع")]
        [MaxLength(100, ErrorMessage = "حداکثر مجاز 100 کارکتر می باشد")]
        [Required(ErrorMessage = "وارد کردن موضوع الزامی است")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Subject { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = "وارد کردن پیام الزامی است")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Message { get; set; }

        public DateTime? Date { get; set; }

        [NotMapped]
        [Display(Name = "تاریخ ارسال")]
        public string ShamsiDate
        {
            get
            {
                if (Date != null)
                    return Utility.GetShamsiDate(Date.Value);
                return string.Empty;
            }
            set { }
        }
    }
}