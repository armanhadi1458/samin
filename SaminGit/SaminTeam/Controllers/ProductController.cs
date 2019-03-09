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
    public class ProductController : Controller
    {
        // GET: Product
        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authenticate]
        public ActionResult Index()
        {
            try
            {
                List<Product> list = unitOfWork.ProductRepository.Get().OrderByDescending(x => x.ID).ToList();
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
                    Product model = unitOfWork.ProductRepository.GetByID(Id);

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
        public ActionResult Create(Product pModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = " خطا در اطلاعات وارد شده کاربر لطفا مجددا تلاش نمایید";
                    return View(pModel);
                }
                if (pModel.Images.FirstOrDefault() != null)
                {
                    string msg = CheckImage(pModel.Images);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        TempData["Error"] = msg;
                        return View(pModel);
                    }
                }
                if (pModel.ID == null)
                {
                    pModel = unitOfWork.ProductRepository.Insert(pModel);
                    unitOfWork.Save();
                    if (pModel.Images.FirstOrDefault() != null)
                    {
                        foreach (var image in pModel.Images)
                        {
                            ProductImage img = new ProductImage();
                            //img.Image = Utility.GetResizedImage(image, 360, 250);
                            using (Stream inputStream = image.InputStream)
                            {
                                MemoryStream memoryStream = inputStream as MemoryStream;
                                if (memoryStream == null)
                                {
                                    memoryStream = new MemoryStream();
                                    inputStream.CopyTo(memoryStream);
                                }
                                img.Image = memoryStream.ToArray();
                            }
                            img.MimeType = image.ContentType;
                            img.Name = image.FileName;
                            img.ProductID = pModel.ID;
                            unitOfWork.ProductImageRepository.Insert(img);
                        }
                    }
                }
                else
                {
                    //Get Customer
                    Product model = unitOfWork.ProductRepository.GetByID(pModel.ID);
                    if (model == null)
                    {
                        TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                        return View(pModel);
                    }
                    if (pModel.Images.FirstOrDefault() != null)
                    {
                        foreach (var image in pModel.Images)
                        {
                            ProductImage img = new ProductImage();
                            //img.Image = Utility.GetResizedImage(image, 360, 250);
                            using (Stream inputStream = image.InputStream)
                            {
                                MemoryStream memoryStream = inputStream as MemoryStream;
                                if (memoryStream == null)
                                {
                                    memoryStream = new MemoryStream();
                                    inputStream.CopyTo(memoryStream);
                                }
                                img.Image = memoryStream.ToArray();
                            }
                            img.MimeType = image.ContentType;
                            img.Name = image.FileName;
                            img.ProductID = model.ID;
                            unitOfWork.ProductImageRepository.Insert(img);
                        }
                    }

                    //Edit DataBase Data
                    model.Title = pModel.Title;
                    model.Content = pModel.Content;
                    model.Status = pModel.Status;
                    model.ShowDashboard = pModel.ShowDashboard;
                    model.Description = pModel.Description;
                    unitOfWork.ProductRepository.Update(model);
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

        [HttpGet, Authenticate]
        public ActionResult _ProductImage(int? pID)
        {
            try
            {
                List<ProductImage> list = unitOfWork.ProductImageRepository.Get(x => x.ProductID == pID).ToList();
                return PartialView("_ProducctImage", list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authenticate, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int? Id)
        {
            try
            {
                var list = new List<Product>();
                Product model = unitOfWork.ProductRepository.GetByID(Id);
                if (model == null)
                {
                    TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                    return RedirectToAction("Index");
                }

                List<ProductImage> pList = unitOfWork.ProductImageRepository.Get(x => x.ProductID == model.ID).ToList();
                foreach (var image in pList)
                    unitOfWork.ProductImageRepository.Delete(image.ID);

                //Remove Item From DataBase
                unitOfWork.ProductRepository.Delete(model);
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
        public ActionResult DeleteAttachment(int? pID)
        {
            try
            {
                ProductImage model = unitOfWork.ProductImageRepository.GetByID(pID);
                if (model == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("رکورد مورد نظر شما یافت نشد", System.Net.Mime.MediaTypeNames.Text.Plain);
                }

                //Remove Item From DataBase
                unitOfWork.ProductImageRepository.Delete(model);
                unitOfWork.Save();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content(ex.ToString(), System.Net.Mime.MediaTypeNames.Text.Plain);
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

                Product model = unitOfWork.ProductRepository.GetByID(pID);
                if (model == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("رکورد مورد نظر شما یافت نشد", System.Net.Mime.MediaTypeNames.Text.Plain);
                }

                model.Status = !model.Status;

                unitOfWork.ProductRepository.Update(model);
                unitOfWork.Save();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content(ex.ToString(), System.Net.Mime.MediaTypeNames.Text.Plain);
            }
        }

        private string CheckImage(List<HttpPostedFileBase> pList)
        {
            try
            {
                string msg = "";
                foreach (var image in pList)
                {
                    if (!image.ContentType.Contains("image"))
                    {
                        msg = "فایل انتخابی باید عکس باشد";
                        break;
                    }
                    if (image.ContentLength/1024 > 3000)
                    {
                        msg = "سایز تصویر انتخابی نبایذ بیشتر از 3MB باشد";
                        break;
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpPost, Authenticate, ValidateAntiForgeryToken]
        public ActionResult ChangeDashboardShow(int? Id)
        {
            try
            {
                var listCount = unitOfWork.ProductRepository.Get(x => x.ShowDashboard == true).Count();

                if (Id == null)
                {
                    if (listCount >= 3)
                        return Json(false, JsonRequestBehavior.AllowGet);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var model = unitOfWork.ProductRepository.GetByID(Id);
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

                    unitOfWork.ProductRepository.Update(model);
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