using SaminProject.Library;
using SaminProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SaminProject.Controllers
{
    public class AdminController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();



        // GET: Admin
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User pModel)
        {
            try
            {
                User user = unitOfWork.UserRepository.Get(x => x.UserName.ToLower() == pModel.UserName.ToLower()).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("UserName", "کاربری با این نام کاربری یافت نشد");
                    return View(pModel);
                }

                var pass = Hash(pModel.Password);
                if (user.Password != pass)
                {
                    ModelState.AddModelError("Password", "کلمه عبور اشتباه است");
                    return View(pModel);
                }

                Session["UserID"] = user.ID;
                return View("Index");

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        /// <summary>
        /// تغییر رمز عبور کاربر
        /// </summary>
        /// <param name="pModel"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangeUserPassword(ChangePassViewModel pModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? userID = (int?)Session["UserID"];

                    if (userID == null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        return Content("شما مجوز این کار را ندارید", System.Net.Mime.MediaTypeNames.Text.Plain);
                    }

                    User user = unitOfWork.UserRepository.GetByID(userID);

                    if (user == null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        return Content("رکورد مورد نظر یافت نشد", System.Net.Mime.MediaTypeNames.Text.Plain);
                    }

                    string oldPass = Hash(pModel.OldPassword);

                    //بررسی درست وارد شدن رمز عبور فعلی
                    if (oldPass != user.Password)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        return Content("رمز عبور فعلی را اشتباه وارد کردید", System.Net.Mime.MediaTypeNames.Text.Plain);
                    }

                    string newHashPassword = Hash(pModel.NewPassword);

                    user.Password = newHashPassword;
                    unitOfWork.Save();

                    return Json(true, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("اطلاعات وارد شده نا معتبر است", System.Net.Mime.MediaTypeNames.Text.Plain);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content("خطایی در سیستم رخ داده است", System.Net.Mime.MediaTypeNames.Text.Plain);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            try
            {
                Session.Abandon();              
                return View("Login");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        private static string Hash(string input)
        {
            try
            {
                var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
                return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

    }
}