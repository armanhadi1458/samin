using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaminProject.ViewModels
{
    public class HeaderViewModel
    {
        public string LogoBase64 { get; set; }
        public Dictionary<string, string> Products { get; set; }
        public Dictionary<string, string> Services { get; set; }
    }
}