using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpdatedScholSystem.Models;
using UpdatedScholSystem.Service;

namespace UpdatedScholSystem.Controllers
{
    [MyAuthorization]
    public class SuperAdminController : Controller
    {
        // GET: SuperAdmin
        public ActionResult Index()
        {
           if(Session["username"].ToString() == "superadmin")
            {

            return View();
            }
            else
            {
                return RedirectToAction("LoginPage", "Home");
            }
        }

        public ActionResult Profile()
        {
            var Email = Session["username"];
            var details = BaseDbServices.Instance.GetData("Select * from tbluserdetail where Email='" + Email + "'", null).Rows[0]; 
            User user = new User()
            {
                UserID = int.Parse(details["UserID"].ToString()),
                Email = details["Email"].ToString()
            };
            return View(user);
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
           
            return RedirectToAction("Index");
        }

        public ActionResult CompanyManagement()
        {
            return View();
        }
    }
}