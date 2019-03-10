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
                switch (UniqueTitle.ToLower())
                {
                    case "dashboard":
                        return "صفحه اصلی";
                    case "services":
                        return "خدمات";
                    case "products":
                        return "محصولات";
                    case "projects":
                        return "پروژه ها";
                    case "news":
                        return "اخبار";
                    case "contact":
                        return "تماس";
                    case "aboutme":
                        return "درباره ما";
                    default:
                        return "نا معتبر";
                }
            }
        }
    }
}