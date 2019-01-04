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
    public class BaseInformationController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: BaseInformation
        [Authenticate]
        public ActionResult Edit()
        {
            try
            {
                BaseInformation model = unitOfWork.BaseInformationRepository.GetByID(1);
                if (model == null)
                {
                    TempData["Error"] = "خطا در روند اجرای برنامه";
                    return RedirectToAction("Index", "admin");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authenticate, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(BaseInformation pModel)
        {
            try
            {
                BaseInformation model = unitOfWork.BaseInformationRepository.GetByID(pModel.ID);
                if (model == null)
                {
                    TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                    return View(pModel);
                }
                if (!ModelState.IsValid)
                    return View(pModel);

                if (pModel.ContentFile != null && pModel.ContentFile.ContentLength / 1024 >= 200)
                {
                    TempData["Error"] = "حجم فایل انتخابی نباید بیشتر از 200kb باشد";
                    return View(pModel);
                }

                if (pModel.ContentFile != null)
                {
                    MemoryStream ms = new MemoryStream();
                    pModel.ContentFile.InputStream.CopyTo(ms);
                    model.Logo = ms.ToArray();
                }

                model.Title = pModel.Title;
                model.Address1 = pModel.Address1;
                model.Address2 = pModel.Address2;
                model.Email = pModel.Email;
                model.Phone = pModel.Phone;
                model.Mobile = pModel.Mobile;
                model.Content = pModel.Content;
                model.Description = pModel.Description;
                model.Instagram = pModel.Instagram;
                model.Telegram = pModel.Telegram;
                model.FaceBook = pModel.FaceBook;

                unitOfWork.BaseInformationRepository.Update(model);
                unitOfWork.Save();
                TempData["Success"] = "عملیات با موفقیت به اتمام رسید";
                return View(model);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        public string UploadImage()
        {
            HttpPostedFileBase file = null;
            string RenameFile = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                file = Request.Files[i];
                string fileExt = System.IO.Path.GetExtension(file.FileName);
                Guid randomFileName = Guid.NewGuid();
                RenameFile = randomFileName + fileExt;
                var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), RenameFile);
                file.SaveAs(path);
            }
            return @"/Content/Uploads/" + RenameFile;
        }
    }
}