using MySql.Data.MySqlClient;
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


        [HttpPost]
        public ActionResult UserControl( UserControl userControl)
        {
            userControl.Company_ID = (int)Session["companyID"];
            var details = BaseDbServices.Instance.GetData("Select * from tblusercontrol where Group_ID='" + userControl.Group_ID + "' and Company_ID='" + userControl.Company_ID + "'", null);
            if(details.Rows.Count > 0)
            {
                BaseDbServices.Instance.RunQuery("Delete from tblusercontrol where Group_ID='" + userControl.Group_ID + "' and Company_ID='" + userControl.Company_ID + "'", null);
            }
            //var u = db.Payrolls.FirstOrDefault(x => x.ID == payroll.ID);
         
            if (userControl.ActionFeatures != null && userControl.ActionFeatures.Count > 0)
            {
                foreach (var m in userControl.ActionFeatures)
                {
                    foreach (var r in m.FeatureActions.ToList())
                    {
                        if (r.IsChecked == true)
                        {
                            userControl.Feature_ID = m.Feature_ID;
                            userControl.Action_ID = r.ID;
                            UserAccessDbServices.Instance.PostUserControl(userControl);
                        }
                    }
                }
            }
            return RedirectToAction("UserAccess");
        }
    }
}