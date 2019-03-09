using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaminProject.Models
{
    public class PageInformation
    {
        [Key]
        public short? ID { get; set; }

        [MaxLength(100, ErrorMessage = "حداکثر مجاز 100 کارکتر می باشد")]
        public string UniqueTitle { get; set; }

        [Display(Name = "تیتر")]
        [MaxLength(200, ErrorMessage = "حداکثر مجاز 200 کارکتر می باشد")]
        [Required(ErrorMessage = "وارد کردن تیتر الزامی است")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Title { get; set; }

        [Display(Name = "تیتر فرعی")]
        [MaxLength(200, ErrorMessage = "حداکثر مجاز 200 کارکتر می باشد")]
        [Required(ErrorMessage = "وارد کردن تیتر فرعی الزامی است")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string SubTitle { get; set; }
        public string FileName { get; set; }
        public string OrginalFileName { get; set; }

        [NotMapped]
        public HttpPostedFileBase HeaderImage { get; set; }

        [NotMapped]
        public string PageTitle
        {
            get
            {
                switch (UniqueTitle)
                {
                    case "DashBoard":
                        return "صفحه اصلی";
                    case "Services":
                        return "خدمات";
                    case "Products":
                        return "محصولات";
                    case "Projects":
                        return "پروژه ها";
                    case "News":
                        return "اخبار";
                    case "Contact":
                        return "تماس";
                    default:
                        return "نا معتبر";
                }
            }
        }
    }
}