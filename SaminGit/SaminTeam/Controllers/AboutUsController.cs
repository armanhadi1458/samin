using SaminProject.Library;
using SaminProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaminProject.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: AboutUs
        UnitOfWork _uow = new UnitOfWork();
        public ActionResult Index()
        {
            try
            {
                AboutUsViewModel model = new AboutUsViewModel();
                var pageInfo = _uow.PageInformationRepository.Get(x => x.UniqueTitle.ToLower() == "aboutme").FirstOrDefault();
                model.Content = _uow.BaseInformationRepository.GetQueryabale().Where(x => x.ID == 1).Select(x => x.Content).FirstOrDefault();
                model.Title = pageInfo.Title;
                model.SubTitle = pageInfo.SubTitle;
                model.HeaderImage = $"/Content/Pages-image/{pageInfo.FileName}";
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}