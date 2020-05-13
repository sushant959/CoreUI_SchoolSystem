using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpdatedScholSystem.Models;
using UpdatedScholSystem.Service;

namespace UpdatedScholSystem.Controllers
{
    [RoutePrefix("Settings")]
    [MyAuthorization]
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return RedirectToAction("Group");
        }

        public ActionResult Group()
        {
            return View();
        }

        public ActionResult Feature()
        {
            return View();
        }

        public ActionResult FeatureAction()
        {
            return View();
        }
        public ActionResult UserAccess()
        {
            int c = (int)Session["CompanyID"];
            var groups = BaseDbServices.Instance.GetData("select * from tblgroup where Company_ID='" + c + "'", null);
            List<Group> lst = new List<Group>();
            for(int i=0; i<groups.Rows.Count;i++)
            {
                Group group = new Group();
                group.ID = Convert.ToInt32(groups.Rows[i]["ID"]);
                group.UserRole = groups.Rows[i]["UserRole"].ToString();
                group.Company_ID = Convert.ToInt32(groups.Rows[i]["Company_ID"]);
                lst.Add(group);
            }

            ViewBag.Group = lst;
            return View();
        }
    }
}