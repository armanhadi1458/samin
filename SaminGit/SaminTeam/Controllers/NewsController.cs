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
    public class NewsController : Controller
    {
        // GET: News
        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authenticate]
        public ActionResult Index()
        {
            try
            {
                List<News> list = unitOfWork.NewsRepository.Get().OrderByDescending(x => x.ID).ToList();
                return View(list);
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        [Authenticate]
        public ActionResult Create(int? Id)
        {
            try
            {
                if (Id == null)
                    return View();
                else
                {
                    News model = unitOfWork.NewsRepository.GetByID(Id);

                    if (model == null || model.ID == null)
                    {
                        TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                        return View();
                    }

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authenticate, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(News pModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = " خطا در اطلاعات وارد شده کاربر لطفا مجددا تلاش نمایید";
                    return View(pModel);
                }
                if (pModel.ContentFile != null && pModel.ContentFile.ContentLength / 1024 >= 200)
                {
                    TempData["Error"] = "حجم فایل انتخابی نباید بیشتر از 200kb باشد";
                    return View(pModel);
                }
                if (pModel.ID == null)
                {
                    MemoryStream ms = new MemoryStream();
                    pModel.ContentFile.InputStream.CopyTo(ms);
                    pModel.Logo = ms.ToArray();
                    pModel.Date = DateTime.Now.Date;
                    unitOfWork.NewsRepository.Insert(pModel);
                }
                else
                {
                    //Get Customer
                    News model = unitOfWork.NewsRepository.GetByID(pModel.ID);
                    if (model == null)
                    {
                        TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                        return View(pModel);
                    }
                    if (pModel.ContentFile != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        pModel.ContentFile.InputStream.CopyTo(ms);
                        model.Logo = ms.ToArray();
                    }

                    //Edit DataBase Data
                    model.Title = pModel.Title;
                    model.Content = pModel.Content;
                    model.Status = pModel.Status;
                    model.ShowDashboard = pModel.ShowDashboard;
                    model.Writer = pModel.Writer;
                    model.Description = pModel.Description; unitOfWork.NewsRepository.Update(model);
                }

                unitOfWork.Save();
                TempData["Success"] = "عملیات با موفقیت به اتمام رسید";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authenticate, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int? Id)
        {
            try
            {
                var list = new List<News>();
                News model = unitOfWork.NewsRepository.GetByID(Id);
                if (model == null)
                {
                    TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                    return RedirectToAction("Index");
                }

                //Remove Item From DataBase
                unitOfWork.NewsRepository.Delete(model);
                unitOfWork.Save();

                //Get List For View
                TempData["Success"] = "عملیات با موفقیت به اتمام رسید";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authenticate, HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int? pID)
        {
            try
            {
                if (pID == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("خطا در دریافت اطلاعات از کاربر", System.Net.Mime.MediaTypeNames.Text.Plain);
                }

                News model = unitOfWork.NewsRepository.GetByID(pID);
                if (model == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("رکورد مورد نظر شما یافت نشد", System.Net.Mime.MediaTypeNames.Text.Plain);
                }

                model.Status = !model.Status;

                unitOfWork.NewsRepository.Update(model);
                unitOfWork.Save();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content(ex.ToString(), System.Net.Mime.MediaTypeNames.Text.Plain);
            }
        }

        [HttpPost, Authenticate, ValidateAntiForgeryToken]
        public ActionResult ChangeDashboardShow(int? Id)
        {
            try
            {
                var listCount = unitOfWork.NewsRepository.Get(x => x.ShowDashboard == true).Count();

                if (Id == null)
                {
                    if (listCount >= 3)
                        return Json(false, JsonRequestBehavior.AllowGet);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var model = unitOfWork.NewsRepository.GetByID(Id);
                    if (model == null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        return Json(false, System.Net.Mime.MediaTypeNames.Text.Plain);
                    }

                    if (model.ShowDashboard)
                        model.ShowDashboard = false;
                    else
                    {
                        if (listCount >= 3)
                            return Json(false, JsonRequestBehavior.AllowGet);

                        model.ShowDashboard = true;
                    }

                    unitOfWork.NewsRepository.Update(model);
                    unitOfWork.Save();

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content(ex.ToString(), System.Net.Mime.MediaTypeNames.Text.Plain);
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