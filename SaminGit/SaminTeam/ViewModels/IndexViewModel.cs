using SaminProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaminProject.ViewModels
{
    public class IndexViewModel
    {
        /// <summary>
        /// لیست خدمات برای نمایش
        /// </summary>
        public List<Service> Services { get; set; }

        /// <summary>
        /// لیست محصولات
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// لیست اخبار
        /// </summary>
        public List<News> News { get; set; }

        public List<Project> Projects { get; set; }
    }
}