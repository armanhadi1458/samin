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
    public class ProjectController : Controller
    {
        // GET: Project
        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authenticate]
        public ActionResult Index()
        {
            try
            {
                List<Project> pList = unitOfWork.ProjectRepository.Get().ToList();
                return View(pList);
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
                    Project model = unitOfWork.ProjectRepository.GetByID(Id);

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
        public ActionResult Create(Project pModel)
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

                if (pModel.Images.FirstOrDefault() != null)
                {
                    foreach (var image in pModel.Images)
                    {
                        Project project = new Project();
                        var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName).ToLower()}";
                        project.FileName = fileName;
                        project.MimeType = image.ContentType;
                        project.OrginalName = image.FileName;
                        project.ProjectName = pModel.ProjectName;
                        unitOfWork.ProjectRepository.Insert(project);
                        image.SaveAs(Path.Combine(Server.MapPath("~/Content/project-image/"), fileName));
                    }
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

        [HttpPost, Authenticate, ValidateAntiForgeryToken]
        public ActionResult EditTitle(int? Id, string pProjectName)
        {
            try
            {
                var model = unitOfWork.ProjectRepository.GetByID(Id);
                if (model == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Json(false, System.Net.Mime.MediaTypeNames.Text.Plain);
                }
                model.ProjectName = pProjectName;
                unitOfWork.ProjectRepository.Update(model);
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
        public ActionResult Delete(int? Id)
        {
            try
            {
                Project model = unitOfWork.ProjectRepository.GetByID(Id);
                if (model == null)
                {
                    TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                    return RedirectToAction("Index");
                }

                //Remove Item From DataBase
                unitOfWork.ProjectRepository.Delete(model);
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

        [HttpPost, Authenticate, ValidateAntiForgeryToken]
        public ActionResult ChangeDashboardShow(int? Id)
        {
            try
            {
                var listCount = unitOfWork.ProjectRepository.Get(x => x.ShowDashboard == true).Count();

                var model = unitOfWork.ProjectRepository.GetByID(Id);
                if (model == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Json(false, System.Net.Mime.MediaTypeNames.Text.Plain);
                }

                if (model.ShowDashboard == true)
                    model.ShowDashboard = false;
                else
                {
                    if (listCount >= 6)
                        return Json(false, JsonRequestBehavior.AllowGet);

                    model.ShowDashboard = true;
                }

                unitOfWork.ProjectRepository.Update(model);
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
                    if (image.ContentLength / 1024 > 3000)
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


    }
}