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

        public string ServiceTitle { get; set; }

        public string ServiceSubTitle { get; set; }

        /// <summary>
        /// لیست محصولات
        /// </summary>
        public List<Product> Products { get; set; }

        public string ProductTitle { get; set; }

        public string ProductSubTitle { get; set; }

        /// <summary>
        /// لیست اخبار
        /// </summary>
        public List<News> News { get; set; }

        public string NewsTitle { get; set; }

        public string NewsSubTitle { get; set; }

        public List<Project> Projects { get; set; }
        public string ProjectTitle { get; set; }

        public string ProjectSubTitle { get; set; }

        public string HeaderImage { get; set; }

        public string DashboardTitle { get; set; }

        public string DashboardSubTitle { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string TotalProject { get; set; }

        public string TotalEmployees { get; set; }

        public string TotalClient { get; set; }

        public string TotalAgency { get; set; }

        public List<Customer> Customers { get; set; }

    }
}