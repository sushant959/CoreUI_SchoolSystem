using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UpdatedScholSystem.Service
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class MyAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            dynamic user = HttpContext.Current.Session["user"];
            if (user == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Logout");
                return;
            }

            bool hasAccessRole = Roles == "";
            if (!hasAccessRole)
            {
                string[] roles = Roles.Split(',');
                foreach (var r in roles)
                {
                    if (r == user.Role)
                    {
                        hasAccessRole = true;
                        break;
                    }
                }
            }
            
            if (!hasAccessRole)
                filterContext.Result = new RedirectResult("~/Home/Logout");


        }

    }
    
}