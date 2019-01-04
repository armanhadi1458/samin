using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SaminProject.Models
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = HttpContext.Current.Session["UserID"];
            if (session == null)
            {
                var url = filterContext.Controller.ControllerContext.HttpContext.Request.RawUrl;
                filterContext.Controller.TempData["Message"] = "برای دسترسی به این صفحه باید وارد سیستم شوید";
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                            { "controller", "Admin" },
                            { "action", "Login" },
                        }
                    );
            }
        }
    }
}



