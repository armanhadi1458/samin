using SaminProject.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaminProject.Models
{
    public class News
    {
        [Key]
        public short? ID { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Title { get; set; }

        [Display(Name = "محتوا")]
        [Required(ErrorMessage = "وارد کردن محتوا الزامی است")]
        [AllowHtml]
        public string Content { get; set; }

        [NotMapped]
        [Display(Name = "عکس خبر")]
        public HttpPostedFileBase ContentFile { get; set; }

        [Display(Name = "شرح")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Description { get; set; }

        [Display(Name = "وضعیت")]
        [Required]
        public bool Status { get; set; }

        [Display(Name = "نمایش در داشبورد")]
        [Required]
        public bool ShowDashboard { get; set; }

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

        public DateTime Date { get; set; }

        [NotMapped]
        public string ShamsiDate
        {
            get
            {
                if (Date != null)
                    return Utility.GetShamsiDate(Date);
                return string.Empty;
            }
            set { }
        }

        [Display(Name = "نویسنده")]
        [Required(ErrorMessage = "لطفا نام نویسنده را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "حداکثر 100 کارکتر")]
        public string Writer { get; set; }
    }
}