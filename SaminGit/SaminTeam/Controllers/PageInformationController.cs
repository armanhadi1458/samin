using SaminProject.Library;
using SaminProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaminProject.Controllers
{
    public class PageInformationController : Controller
    {
        // GET: PageInformation
        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authenticate]
        public ActionResult Index()
        {
            try
            {
                var list = unitOfWork.PageInformationRepository.Get().ToList();
                return View(list);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authenticate, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(PageInformation pModel)
        {
            try
            {
                PageInformation model = unitOfWork.PageInformationRepository.GetByID(pModel.ID);
                if (model == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("رکورد مورد نظر شما یافت نشد", System.Net.Mime.MediaTypeNames.Text.Plain);
                }
                if (!ModelState.IsValid)
                    return View(pModel);

                if (pModel.HeaderImage != null)
                {
                    string msg = CheckImage(pModel.HeaderImage);
                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        return Content("حجم فایل انتخابی نباید بیشتر از 3mb باشد", System.Net.Mime.MediaTypeNames.Text.Plain);
                    }
                }

                if (pModel.HeaderImage != null)
                {
                    //Delete Old Image
                    if(!string.IsNullOrEmpty(model.FileName))
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Pages-image/"), model.FileName));

                    //Add New Image
                    var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(pModel.HeaderImage.FileName).ToLower()}";
                    model.FileName = fileName;
                    model.OrginalFileName = pModel.HeaderImage.FileName;
                    pModel.HeaderImage.SaveAs(Path.Combine(Server.MapPath("~/Content/Pages-image/"), fileName));
                }

                model.Title = pModel.Title;
                model.SubTitle = pModel.SubTitle;

                unitOfWork.PageInformationRepository.Update(model);
                unitOfWork.Save();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content(ex.ToString(), System.Net.Mime.MediaTypeNames.Text.Plain);
            }
        }

        private string CheckImage(HttpPostedFileBase image)
        {
            try
            {
                string msg = "";

                if (!image.ContentType.Contains("image"))
                {
                    msg = "فایل انتخابی باید عکس باشد";
                    return msg;
                }
                if (image.ContentLength / 1024 > 3000)
                {
                    msg = "سایز تصویر انتخابی نبایذ بیشتر از 3MB باشد";
                    return msg;
                }
                return msg;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}