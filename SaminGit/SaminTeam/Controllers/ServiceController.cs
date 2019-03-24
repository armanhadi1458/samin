using SaminProject.Library;
using SaminProject.Models;
using SaminProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaminProject.Controllers
{
    public class ServiceController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authenticate]
        public ActionResult Index()
        {
            try
            {
                List<Service> list = unitOfWork.ServiceRepository.Get().OrderByDescending(x => x.ID).ToList();
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
                    return View(new Service());
                else
                {
                    Service model = unitOfWork.ServiceRepository.GetByID(Id);

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

        public ActionResult List()
        {
            try
            {
                PageInformation model = unitOfWork.PageInformationRepository.Get(x => x.UniqueTitle.ToLower() == "services").FirstOrDefault();
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult Detail(int? Id)
        {
            try
            {
                Service model = unitOfWork.ServiceRepository.GetByID(Id);
                if (model == null)
                    return View("NotFound");

                ViewBag.HeaderImage = unitOfWork.PageInformationRepository.Get(x => x.UniqueTitle.ToLower() == "services")?.FirstOrDefault()?.FileName;
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult _ServiceWrapper(int? pCount)
        {
            if (pCount == null)
                pCount = 0;

            List<Service> pList = unitOfWork.ServiceRepository.GetQueryabale().OrderByDescending(x => x.ID).Skip(pCount.Value * 9).Take(9).ToList();
            return PartialView("_ServicePartial", pList);
        }

        public ActionResult _RecentServices(int? Id)
        {
            try
            {
                List<Service> servicList = unitOfWork.ServiceRepository.GetQueryabale().Where(x => x.ID != Id).Take(4).OrderByDescending(x => x.ID).ToList();
                List<RecentViewModel> pList = servicList.Select(x => new RecentViewModel()
                {
                    Image = x.Base64Image,
                    Title = x.Title,
                    DateShamsi = x.ShamsiDate,
                    Href = "/Service/Detail/" + x.ID
                }).ToList();

                return PartialView("_RecentPartial", pList);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authenticate, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Service pModel)
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
                    pModel.Date = DateTime.Now;
                    unitOfWork.ServiceRepository.Insert(pModel);
                }
                else
                {
                    //Get Customer
                    Service model = unitOfWork.ServiceRepository.GetByID(pModel.ID);
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
                    model.Description = pModel.Description;
                    model.Icon = pModel.Icon;
                    unitOfWork.ServiceRepository.Update(model);
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
                var list = new List<Service>();
                Service model = unitOfWork.ServiceRepository.GetByID(Id);
                if (model == null)
                {
                    TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                    return RedirectToAction("Index");
                }

                //Remove Item From DataBase
                unitOfWork.ServiceRepository.Delete(model);
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

                Service model = unitOfWork.ServiceRepository.GetByID(pID);
                if (model == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("رکورد مورد نظر شما یافت نشد", System.Net.Mime.MediaTypeNames.Text.Plain);
                }

                model.Status = !model.Status;

                unitOfWork.ServiceRepository.Update(model);
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
                var listCount = unitOfWork.ServiceRepository.Get(x => x.ShowDashboard == true).Count();

                if (Id == null)
                {
                    if (listCount >= 3)
                        return Json(false, JsonRequestBehavior.AllowGet);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var model = unitOfWork.ServiceRepository.GetByID(Id);
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

                    unitOfWork.ServiceRepository.Update(model);
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




