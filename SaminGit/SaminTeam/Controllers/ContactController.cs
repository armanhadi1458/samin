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
    public class ContactController : Controller
    {
        // GET: Contact
        UnitOfWork _uow = new UnitOfWork();
        public ActionResult Index()
        {
            try
            {
                var BaseInfo = _uow.BaseInformationRepository.GetByID(1);
                var pageInfo = _uow.PageInformationRepository.Get(x => x.UniqueTitle.ToLower() == "contact").FirstOrDefault();
                ContactViewModel model = new ContactViewModel()
                {
                    Address = BaseInfo.Address1,
                    Address2 = BaseInfo.Address2,
                    City = BaseInfo.City,
                    Country = BaseInfo.Country,
                    Emial = BaseInfo.Email,
                    Longtidue = BaseInfo.Longtiude,
                    Latiude = BaseInfo.Latiude,
                    Mobile = BaseInfo.Mobile,
                    Phone = BaseInfo.Phone,
                    WorkTime = BaseInfo.WorkTime,
                    EmailCaption = pageInfo.Title,
                    MobileCaption = pageInfo.SubTitle,
                    HeaderImage = $"/Content/Pages-image/{pageInfo.FileName}"
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create(Contact pModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = string.Join(",", ModelState.Values.SelectMany(v => v.Errors));
                    return RedirectToAction("Index");
                }

                pModel.Date = DateTime.Now;
                _uow.ContactRepository.Insert(pModel);
                _uow.Save();
                TempData["Success"] = "پیام شما با موفقیت ارسال گردید";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authenticate]
        public ActionResult List()
        {
            try
            {
                List<Contact> pList = _uow.ContactRepository.Get().ToList();
                return View(pList);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authenticate, HttpGet]
        public ActionResult FindMessage(int? Id)
        {
            try
            {
                if (Id == null || Id == 0)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("خطا در دریافت اطلاعات از سمت کاربر", System.Net.Mime.MediaTypeNames.Text.Plain);
                }

                var model = _uow.ContactRepository.GetByID(Id);
                if(model == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("رکورد مورد نظر شما یافت نشد", System.Net.Mime.MediaTypeNames.Text.Plain);
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content(ex.ToString(), System.Net.Mime.MediaTypeNames.Text.Plain);
            }
        }

        [Authenticate, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int? Id)
        {
            try
            {
                var list = new List<News>();
                Contact model = _uow.ContactRepository.GetByID(Id);
                if (model == null)
                {
                    TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                    return RedirectToAction("Index");
                }

                //Remove Item From DataBase
                _uow.ContactRepository.Delete(model);
                _uow.Save();

                //Get List For View
                TempData["Success"] = "عملیات با موفقیت به اتمام رسید";
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


    }
}