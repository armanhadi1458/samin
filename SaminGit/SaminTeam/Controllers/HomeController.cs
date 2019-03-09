using SaminProject.Library;
using SaminProject.Models;
using SaminProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaminProject.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Home
        public ActionResult Index()
        {
            try
            {
                IndexViewModel model = new IndexViewModel();
                model.Services = unitOfWork.ServiceRepository.Get(x => x.ShowDashboard == true).Take(3).ToList();
                model.Products = unitOfWork.ProductRepository.Get(x => x.ShowDashboard == true).Take(3).ToList();
                model.News = unitOfWork.NewsRepository.Get(x => x.ShowDashboard == true).Take(3).ToList();
                model.Projects = unitOfWork.ProjectRepository.Get(x => x.ShowDashboard == true).Take(6).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult _HeaderPartial()
        {
            try
            {
                Dictionary<string, string> products = new Dictionary<string, string>();
                Dictionary<string, string> services = new Dictionary<string, string>();

                var productsID = unitOfWork.ProductRepository.GetQueryabale().Select(x => new { x.Title, x.ID }).ToArray();
                foreach (var item in productsID)
                    products.Add(item.Title, $"/Product/Detail/{item.ID}");

                var servicesID = unitOfWork.ServiceRepository.GetQueryabale().Select(x => new { x.Title, x.ID }).ToArray();
                foreach (var item in servicesID)
                    services.Add(item.Title, $"/Service/Detail/{item.ID}");

                byte[] logo = unitOfWork.BaseInformationRepository.GetQueryabale().Select(x => x.Logo).FirstOrDefault();

                HeaderViewModel model = new HeaderViewModel();
                model.Products = products;
                model.Services = services;
                model.LogoBase64 = (logo != null) ? Convert.ToBase64String(logo) : string.Empty;

                return PartialView(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult _FooterPartial()
        {
            try
            {
                BaseInformation baseModel = unitOfWork.BaseInformationRepository.GetByID(1);
                FooterViewModel model = new FooterViewModel();
                model.AboutUs = baseModel.Description;
                model.FacebookLink = baseModel.FaceBook;
                model.InstagramLink = baseModel.Instagram;
                model.TelegramLink = baseModel.Telegram;

                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


    }
}