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
    public class Service
    {
        [Key]
        public int? ID { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Title { get; set; }

        [Display(Name = "محتوا")]
        [Required(ErrorMessage = "وارد کردن محتوا الزامی است")]
        [AllowHtml]
        public string Content { get; set; }

        [NotMapped]
        [Display(Name = "عکس خدمات")]
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

        [Display(Name = "آیکون")]
        [Required(ErrorMessage = "لطفا آیکون را انتخاب نمایید")]
        public string Icon { get; set; }

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
                    return Utility.GetPersianDateString(Date);
                return string.Empty;
            }
            set { }
        }

    }
}