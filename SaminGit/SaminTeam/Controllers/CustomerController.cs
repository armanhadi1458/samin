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
    public class CustomerController : Controller
    {
        // GET: Customer
        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authenticate]
        public ActionResult Index()
        {
            try
            {
                List<Customer> list = unitOfWork.CustomerRepository.Get().ToList();
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
                    Customer model = unitOfWork.CustomerRepository.GetByID(Id);

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
        public ActionResult Create(Customer pModel)
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
                    unitOfWork.CustomerRepository.Insert(pModel);
                }
                else
                {
                    //Get Customer
                    Customer model = unitOfWork.CustomerRepository.GetByID(pModel.ID);
                    if (model == null)
                    {
                        TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                        return View(pModel);
                    }
                    if (model.ContentFile != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        pModel.ContentFile.InputStream.CopyTo(ms);
                        model.Logo = ms.ToArray();
                    }

                    //Edit DataBase Data
                    model.Name = pModel.Name;
                    model.Address = pModel.Address;
                    model.Email = pModel.Email;
                    model.PhoneNumber = pModel.PhoneNumber;
                    unitOfWork.CustomerRepository.Update(model);
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
                var list = new List<Customer>();
                Customer model = unitOfWork.CustomerRepository.GetByID(Id);
                if (model == null)
                {
                    TempData["Error"] = "رکورد مورد نظر شما یافت نشد";
                    return RedirectToAction("Index");
                }

                //Remove Item From DataBase
                unitOfWork.CustomerRepository.Delete(model);
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
    }
}