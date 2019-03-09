using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaminProject.Models
{
    public class Project
    {
        public int? ID { get; set; }

        [Display(Name = "نام فایل")]
        public string OrginalName { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        [Display(Name = "نمایش در داشبورد")]
        public bool? ShowDashboard { get; set; }

        [Display(Name = "نام پروژه")]
        [Required(ErrorMessage = "لطفا نام پروژه را وارد نمایید")]
        [MaxLength(ErrorMessage = "حداکثر 150 کارکتر")]
        public string ProjectName { get; set; }

        [NotMapped]
        [Display(Name = "عکس ها")]
        public List<HttpPostedFileBase> Images { get; set; }
    }
}