using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpdatedScholSystem.Service;
using UpdatedScholSystem.Models;
using WebMarkupMin.AspNet4.Mvc;
using System.Threading;
using System.Security.Claims;

namespace UpdatedScholSystem.Controllers
{
    [MyAuthorization]
    [CompressContent, MinifyHtml]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string token = null, string r = "#", int c = 0)
        {

            if (Session["user"] != null)
            {
                
                return View();
            }
            else if (token != null)
            {
                try
                {
                    Session["token"] = token;
                    Session["companyID"] = c;

                    ViewBag.Homepage = r;

                    var company = MasterDbAccess.CompanyDbService.Instance.GetById(c);
                    Session["company"] = company.CompanyName;
                    var cp = JwtManager.GetClaimsPrincipal(token);
                    Thread.CurrentPrincipal = cp ?? throw new Exception();

                    var email = cp.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                    var role = cp.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

                    Session["user"] = new { Email = email, Role = role };
                    Session["Role"] = role;
                    Session["Email"] = email;

                    return View();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return RedirectToAction("Logout");
        }


        [AllowAnonymous]
        public ActionResult LoginPage()
        {
            ViewBag.Message = TempData["LoginError"];
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password)
        {
            byte[] byts = System.Text.Encoding.UTF8.GetBytes(Password);
            var _password = Convert.ToBase64String(byts);
            bool type = BaseDbServices.Instance.Login(UserName, _password);
            if (type == true)
            {
                if (UserName == "superadmin")
                {
                    Session.Timeout = 60;
                    Session["username"] = UserName;
                    return RedirectToAction("Index");
                }
                else
                {
                    var Status = CompanyDbServices.Instance.CheckCompanyStatus(UserName);
                    if (Status == "Approved")
                    {

                        Session.Timeout = 60;
                        Session["username"] = UserName;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Session["username"] = null;
                        TempData["LoginError"] = "Company account has not been approved";
                        return RedirectToAction("LoginPage");
                    }
                }
            }
            else if(type == false)
            {
                //bool type1 = BaseDbServices.Instance.CompanyLogin(UserName, _password);
                //if(type1 == true)
                //{
                //    Session.Timeout = 60;
                //    Session["username"] = UserName;
                //    return RedirectToAction("Index");
                //}
                //else
                //{
                    Session["username"] = null;
                    TempData["LoginError"] = "Please provide correct username and password";
                    return RedirectToAction("LoginPage");
                    
                
            }

            //if (type == 1)
            //{
            //    Session.Timeout = 60;
            //    Session["username"] = UserName;

            //    return RedirectToAction("Index");
            //}
            //else if (type == 2)
            //{
            //    Session.Timeout = 60;
            //    Session["username"] = UserName;

            //    return RedirectToAction("Student");
            //}
            //else if (type == 3)
            //{
            //    Session.Timeout = 60;
            //    Session["username"] = UserName;

            //    return RedirectToAction("Teacher");
            //}
            else
            {
                Session["username"] = null;
                TempData["LoginError"] = "Please provide correct username and password";
                return RedirectToAction("LoginPage");
            }

        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Clear();
            return View();
        }

        public ActionResult UserInfo()
        {

            if (Session["Role"].ToString() != "Admin") return new HttpNotFoundResult("not allowed to access thgis page");
           
            return View();
        }



        public ActionResult GeneralSettings()
        {
            
            return View();
        }
        public ActionResult StudentManagement()
        {
            return View();
        }
        public ActionResult ScholarshipManagement()
        {
            return View();
        }

        public ActionResult BillingManagement()
        {
            return View();
        }

        public ActionResult ReceiptManagement()
        {
            return View();
        }

        public ActionResult StudentAttendance()
        {
            return View();
        }

        public ActionResult StaffAttendance()
        {
            return View();
        }

        public ActionResult FineManagement()
        {
            return View();
        }

        public ActionResult TeacherAttendance()
        {
            return View();
        }

        public ActionResult StaffManagement()
        {
            return View();
        }

        public ActionResult StudentForm(string id = null, string value = null)
        {
            var StudentNo = id != null;
            ViewBag.StudentId = StudentNo ? 1 : 0;
            ViewBag.StudentNo = id;
            ViewBag.value = value;
            return View();
        }

        public ActionResult StaffForm(int id = 0, string value = null)
        {
            var TeacherNo = id != 0;
            ViewBag.TeacherId = TeacherNo ? 1 : 0;
            ViewBag.TeacherNo = id;
            ViewBag.value = value;
            return View();
        }

        public ActionResult StudentAttendanceForm()
        {
            return View();
        }

        public ActionResult StaffAttendanceForm()
        {
            return View();
        }

        [Route("ViewStaffDetails/{id}")]
        public ActionResult ViewStaffDetails(int id)
        {
            return RedirectToAction("StaffForm", new { id = id });
        }

        [Route("Home/ViewStudentDetails/{id}")]
        public ActionResult ViewStudentDetails(string id)
        {
            return RedirectToAction("StudentForm", new { id = id });
        }
        [Route("Home/EditStudentDetails/{id}")]
        public ActionResult EditStudentDetails(string id)
        {

            return RedirectToAction("StudentForm", new { id = id, value = "edit" });
        }

        [Route("Home/editStaffDetails/{id}")]
        public ActionResult EditStaffDetails(int id)
        {
            return RedirectToAction("StaffForm", new { id = id, value = "edit" });
        }

        public ActionResult PrintBilling(int BillingId,string Month,string Batch)
        {
            ViewBag.Id = BillingId;
            ViewBag.month = Month;
            ViewBag.batch = Batch;
            return View();
        }

        public ActionResult PrintDueReport(string Batch,string Class,string StudentId)
        {
            ViewBag.Batch = Batch;
            ViewBag.Class = Class;
            ViewBag.StudentId = StudentId;
            return View();
        }

        public ActionResult PrintDueReport1(string Batch, string Class, string StudentId)
        {
            ViewBag.Batch = Batch;
            ViewBag.Class = Class;
            ViewBag.StudentId = StudentId;
            return View();
        }
        public ActionResult PrintReceipt(int ReceiptId,int BillingId)
        {
            ViewBag.ReceiptId = ReceiptId;
            ViewBag.BillingId = BillingId;
            return View();
        }
        public ActionResult DueReport()
        {
            return View();
        }
        public ActionResult TransportationManagement()
        {
            return View();
        }

        public ActionResult StudentTransportMapping()
        {
            return View();
        }
        public ActionResult StudentTransportMappingForm()
        {
            return View();
        }
        public ActionResult StudentExtraFeeMapping()
        {
            return View();
        }

        public ActionResult StudentExtraFeeMappingForm()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CompanyRegistration()
        {
            return View();
        }

        public ActionResult CompanyManagement()
        {
            return View();
        }
    }
}
