using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaminProject.Models
{
    public class Product
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

        /// <summary>
        /// لیست عکس ها
        /// </summary>
        [NotMapped]
        public List<HttpPostedFileBase> Images { get; set; }

        [Display(Name = "شرح")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "استفاده از کاراکتر(های) غیر مجاز")]
        public string Description { get; set; }

        [Display(Name = "وضعیت")]
        [Required]
        public bool Status { get; set; }

        [Display(Name = "نمایش در داشبورد")]
        public bool ShowDashboard { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

    }
}