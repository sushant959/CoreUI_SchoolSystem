using UpdatedScholSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using UpdatedScholSystem.Models;
using System.Data;
using MySql.Data.MySqlClient;
using NepaliDateConverter;
using System.Web.Http.Cors;


namespace UpdatedScholSystem.Controllers
{
    [RoutePrefix("api")]
    [MyAuthorization]
    public class DataController : ApiController
    {
       
        [Route("GetSettingDetails"), HttpGet]
        public IHttpActionResult GetSettingDetails()
        {
            var data = HttpContext.Current.Request;
            var tblname = "tbl" + data["urlName"];
            var companyid = Convert.ToInt32(data["CompanyId"]);
            var details = new DataTable();
            if (tblname == "tblbatchdetails")
            {
                details = BaseDbServices.Instance.GetData("select * from " + tblname + " where IsDeleted =0 and CompanyId='"+ companyid + "' order by FromYear DESC");
                removeUnwantedColumns(ref details, tblname);
            }
            else if (tblname == "tblclassdetails" || tblname == "tblfacultydetails" || tblname == "tblsectiondetails" || tblname == "tblfeestructuredetails" || tblname == "tblcountrydetails" || tblname == "tblstatedetails" || tblname == "tblstudenttype" || tblname == "tblresponsetext" || tblname =="tblstartdate")
            {
                details = BaseDbServices.Instance.GetData("select * from " + tblname + " where CompanyId='"+companyid+"'",null);
            }
            else if (tblname == "tblfeedetails")
            {
                details = BaseDbServices.Instance.GetData("Select * from " + tblname + " where IsDeleted =0 and CompanyId='" +companyid+"'",null);
                removeUnwantedColumns(ref details, tblname);
            }
            else if (tblname == "tblclassfeedetails")
            {
                details = GeneralSettingDbServices.Instance.GetAllBatchClassFee(companyid);
            }
            else if(tblname == "tbldayoff")
            {
                details = BaseDbServices.Instance.GetData("select * from tbldayoffdetails" +
                    " inner join tbldayoff on tbldayoff.DayOffId = tbldayoffdetails.DayOffId" +
                    " where tbldayoff.CompanyId='" + companyid + "'",null);
                details.Columns.Add("TotalDays", typeof(Int32));
                for(int i =0; i <details.Rows.Count;i++)
                {
                    DateTime d1 = Convert.ToDateTime(details.Rows[i]["DateFrom"]);
                    DateTime d2 = Convert.ToDateTime(details.Rows[i]["DateTo"]);
                    TimeSpan span = d2 - d1;
                    int counter = span.Days + 1;
                    details.Rows[i]["TotalDays"] = counter;
                }
            }
            return Ok(details);
        }

        [Route("GetSelectedDeptFaculty"),HttpGet]
        public IHttpActionResult GetSelectedDeptFaculty()
        {
            var data = HttpContext.Current.Request;
            var Id = data["Id"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Info = new List<MySqlParameter>()
            {
                new MySqlParameter("@id", Id),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var dept = BaseDbServices.Instance.GetData("select Distinct Department from tbldayoffdetails where DayOffId=@id and CompanyId=@CompanyId", Info);
            var faculty = BaseDbServices.Instance.GetData("select Distinct Faculty from tbldayoffdetails where DayOffId=@id and Faculty !='Null' and CompanyId=@CompanyId", Info);
            var _class = BaseDbServices.Instance.GetData("select Distinct Class from tbldayoffdetails where DayOffId=@id and Class !='Null' and CompanyId=@CompanyId", Info);

            List<DataTable> lst = new List<DataTable>()
            {
                dept,
                faculty,
                _class
            };
            return Ok(lst);
        }

        [Route("UserControlPermission"),HttpGet]
        public IHttpActionResult UserControlPermission()
        {
            var data = HttpContext.Current.Request;
            var companyId = data["Company_ID"];
            var id = data["id"];
            var rolePermission = BaseDbServices.Instance.GetData("select * from tblusercontrol where Group_ID='" + id + "' and Company_ID='" + companyId + "'", null);
            if(rolePermission.Rows.Count > 0)
            {
                List<UserControl> lstUserControl = new List<UserControl>();
                for (int i = 0; i < rolePermission.Rows.Count; i++)
                {
                    UserControl userControl = new UserControl();
                    userControl.ID = Convert.ToInt32(rolePermission.Rows[i]["ID"]);
                    userControl.Group_ID = Convert.ToInt32(rolePermission.Rows[i]["Group_ID"]);
                    userControl.Feature_ID = Convert.ToInt32(rolePermission.Rows[i]["Feature_ID"]);
                    userControl.Action_ID = Convert.ToInt32(rolePermission.Rows[i]["Action_ID"]);
                    userControl.Company_ID = Convert.ToInt32(rolePermission.Rows[i]["Company_ID"]);
                    lstUserControl.Add(userControl);
                }
                return Json(lstUserControl);
            }
            else
            {
                return Json("");
            }
           
        }
        [Route("GetFeatureAndAction"), HttpGet]
        public IHttpActionResult GetFeatureAndAction()
        {
            var data = HttpContext.Current.Request;
            var companyId = data["Company_ID"];
            var features = BaseDbServices.Instance.GetData("select * from tblfeature where Company_ID='" + companyId + "'", null);
            var actions = BaseDbServices.Instance.GetData("select * from tblfeatureaction where Company_ID='" + companyId + "'", null);
            List<Feature> lstFeature = new List<Feature>();
            for (int i = 0; i < features.Rows.Count; i++)
            {
                Feature feature = new Feature();
                feature.ID = Convert.ToInt32(features.Rows[i]["ID"]);
                feature.Name = features.Rows[i]["Name"].ToString();
                feature.Company_ID = Convert.ToInt32(features.Rows[i]["Company_ID"]);
                lstFeature.Add(feature);
            }
            List<FeatureAction> lstFeatureAction = new List<FeatureAction>();
            for (int i = 0; i < actions.Rows.Count; i++)
            {
                FeatureAction featureaction = new FeatureAction();
                featureaction.ID = Convert.ToInt32(actions.Rows[i]["ID"]);
                featureaction.Name = actions.Rows[i]["Name"].ToString();
                featureaction.Company_ID = Convert.ToInt32(actions.Rows[i]["Company_ID"]);
                lstFeatureAction.Add(featureaction);
            }
            return Json(new { lstFeature, lstFeatureAction });
        }
        [Route("GetSuperAdminPassword"),HttpGet]
        public IHttpActionResult GetSuperAdminPassword()
        {
            var data = HttpContext.Current.Request;
            var Email = data["Email"];
            var Password = data["Password"];
            var password = BaseDbServices.Instance.GetData("select Password from tbluserdetail where Email='" + Email + "'", null);
           
            byte[] byts = System.Text.Encoding.UTF8.GetBytes(Password);
            var _password = Convert.ToBase64String(byts);
            if(password.Rows[0]["Password"].ToString() == _password)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [Route("UpdateSuperAdmin"),HttpPost]
        public IHttpActionResult UpdateSuperAdmin(User obj)
        {
            if(obj.Password !=null)
            {
                byte[] byts = System.Text.Encoding.UTF8.GetBytes(obj.Password);
                var _password = Convert.ToBase64String(byts);
                obj.Password = _password;
            }
            SuperAdminDbService.Instance.UpdateSuperAdmin(obj);
            return Ok("");
        }
        [Route("GetSpecialSelectedFee"), HttpGet]
        public IHttpActionResult GetSpecialSelectedFee()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var _class = data["Class"];
            var faculty = data["Faculty"];
            var studentType = data["StudentType"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            List<MySqlParameter> Fee = new List<MySqlParameter>()
            {
                new MySqlParameter("@batch", batch),
                new MySqlParameter("@class",_class),
                new MySqlParameter("@faculty",faculty),
                new MySqlParameter("@studentType",studentType),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var id = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@batch and Faculty=@faculty and Class=@class and CompanyId=@CompanyId", Fee);
            if (id.Rows.Count == 0)
            {
                return Ok(false);
            }
            else
            {
                var ids = id.Rows[0]["BatchClassId"];
                var FeeDetails = new DataTable();
                List<DataTable> lst = new List<DataTable>();
                
                var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId=@CompanyId", Fee);

                for (int i = 0; i < FeeStructure.Rows.Count; i++)
                {
                    if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == "Extra")
                    {

                    }
                    else
                    {
                        if (studentType == "General")
                        {
                            FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=" + id.Rows[0]["BatchClassId"] + " and StudentType=@studentType and CompanyId=@CompanyId", Fee);
                        }
                        else if (studentType == "Day-Boarder")
                        {
                            FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=" + id.Rows[0]["BatchClassId"] + " and (StudentType=@studentType or StudentType='General') and CompanyId=@CompanyId", Fee);
                        }
                        else if (studentType == "Boarder")
                        {
                            FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=" + id.Rows[0]["BatchClassId"] + " and (StudentType=@studentType or StudentType='General') and CompanyId=@CompanyId", Fee);
                        }

                       
                        lst.Add(FeeDetails);
                    }
                }
                return Ok(lst);
            }
        }

        [Route("GetSearchedFollowUp"),HttpGet]
        public IHttpActionResult GetSearchedFollowUp()
        {
            var data = HttpContext.Current.Request;
            var fromDate = data["FromDate"];
            var toDate = data["ToDate"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = DueDbService.Instance.GetSearchedFollowUp(fromDate, toDate,CompanyId);

            return Ok(details);
        }

        [Route("GetSearchedExpectedDate"), HttpGet]
        public IHttpActionResult GetSearchedExpectedDate()
        {
            var data = HttpContext.Current.Request;
            var fromDate = data["FromDate"];
            var toDate = data["ToDate"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = DueDbService.Instance.GetSearchedExpectedDate(fromDate, toDate,CompanyId);
            return Ok(details);
        }

        //[Route("GetDueDetailsByStudentId"),HttpGet]
        //public IHttpActionResult GetDueDetailsByStudentId()
        //{
        //    var data = HttpContext.Current.Request;
        //    var StudentId = data["StudentId"];
        //    var Batch = data["Batch"];
        //    var details = DueDbService.Instance.GetDueDetailsByStudentId(StudentId, Batch);
        //    var info = DueDbService.Instance.GetPartialDueByStudentId(StudentId, Batch);
        //    var dtAll = new DataTable();
        //    if(info.Rows.Count > 0)
        //    {
        //    var partialdue = BaseDbServices.Instance.GetData("select min(DueAmount) as due from tblreceipt where BillingId='" + info.Rows[0]["BillingId"] + "'", null);

        //    info.Rows[0]["due"] = partialdue.Rows[0]["due"];
        //    dtAll.Merge(details);
        //    dtAll.Merge(info);
        //    dtAll.DefaultView.Sort = "Month asc";
        //    dtAll = dtAll.DefaultView.ToTable();
        //   // dtAll.Columns.Add("Discount");
        //   // dtAll.Columns.Add("Fine");
        //     var DueDetails = new DataTable();
        //       // var Discount = new DataTable();
        //    List<DataTable> lst = new List<DataTable>();
        //    for(int i =0; i < dtAll.Rows.Count;i++)
        //    {
                
        //        DueDetails = BaseDbServices.Instance.GetData("select * from tblbillingdetails where BillingId='" + dtAll.Rows[i]["BillingId"] + "'", null);
        //        lst.Add(DueDetails);
        //    }
        //    return Ok(new { dtAll, lst});
        //    }
        //    else
        //    {
        //        dtAll.Merge(details);
        //        var DueDetails = new DataTable();
        //        List<DataTable> lst = new List<DataTable>();
        //        for (int i = 0; i < dtAll.Rows.Count; i++)
        //        {
        //            DueDetails = BaseDbServices.Instance.GetData("select * from tblbillingdetails where BillingId='" + dtAll.Rows[i]["BillingId"] + "'", null);
        //            lst.Add(DueDetails);
        //        }
        //        return Ok(new { dtAll, lst });
        //    }
            
        //}

        [Route("GetDueDetailsByStudentId1"), HttpGet]
        public IHttpActionResult GetDueDetailsByStudentId1()
        {
            var data = HttpContext.Current.Request;
            var StudentId = data["StudentId"];
            var Batch = data["Batch"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = DueDbService.Instance.GetDueDetailsByStudentId(StudentId, Batch,CompanyId);
            details.Columns.Add("Discount");
            details.Columns.Add("Fine");
            details.Columns.Add("Paid");
            details.Columns.Add("Due");
            DataRow _row = details.NewRow();

            for (int i = 0; i < details.Rows.Count; i++)
            {
                var checkReceipt = BaseDbServices.Instance.GetData("select * from tblreceipt where BillingId='" + details.Rows[i]["BillingId"] + "' and CompanyId='"+CompanyId+"'", null);
                if (checkReceipt.Rows.Count > 0)
                {
                    var duelist = BaseDbServices.Instance.GetData("select sum(Discount) as discount,sum(Fine) as fine,sum(PaidAmount) as paid from tblreceipt where BillingId='" + details.Rows[i]["BillingId"] + "' and CompanyId='"+ CompanyId + "'", null);

                    details.Rows[i]["Discount"] = duelist.Rows[0]["discount"];

                    details.Rows[i]["Fine"] = duelist.Rows[0]["fine"];

                    details.Rows[i]["Paid"] = duelist.Rows[0]["paid"];

                    details.Rows[i]["Due"] = Convert.ToInt32(details.Rows[i]["Total"]) + Convert.ToInt32(details.Rows[i]["Fine"]) - Convert.ToInt32(details.Rows[i]["Discount"]) - Convert.ToInt32(details.Rows[i]["Paid"]);

                }
                else
                {
                    details.Rows[i]["Discount"] = 0;

                    details.Rows[i]["Fine"] = 0;

                    details.Rows[i]["Paid"] = 0;

                    details.Rows[i]["Due"] = Convert.ToInt32(details.Rows[i]["Total"]) + Convert.ToInt32(details.Rows[i]["Fine"]) - Convert.ToInt32(details.Rows[i]["Discount"]) - Convert.ToInt32(details.Rows[i]["Paid"]);
                }
            }

            //var info = DueDbService.Instance.GetPartialDueByStudentId(StudentId, Batch);
            //var dtAll = new DataTable();
            //if (info.Rows.Count > 0)
            //{
            //    var partialdue = BaseDbServices.Instance.GetData("select min(DueAmount) as due from tblreceipt where BillingId='" + info.Rows[0]["BillingId"] + "'", null);

            //    info.Rows[0]["due"] = partialdue.Rows[0]["due"];
            //    dtAll.Merge(details);
            //    dtAll.Merge(info);
            //    dtAll.DefaultView.Sort = "Month asc";
            //    dtAll = dtAll.DefaultView.ToTable();
            //    // dtAll.Columns.Add("Discount");
            //    // dtAll.Columns.Add("Fine");
                var DueDetails = new DataTable();
                // var Discount = new DataTable();
                List<DataTable> lst = new List<DataTable>();
                for (int i = 0; i < details.Rows.Count; i++)
                {

                    DueDetails = BaseDbServices.Instance.GetData("select * from tblbillingdetails where BillingId='" + details.Rows[i]["BillingId"] + "' and CompanyId='"+CompanyId+"'", null);
                    lst.Add(DueDetails);
                }
                return Ok(new { details, lst });
            //}
            //else
            //{
            //    dtAll.Merge(details);
            //    var DueDetails = new DataTable();
            //    List<DataTable> lst = new List<DataTable>();
            //    for (int i = 0; i < dtAll.Rows.Count; i++)
            //    {
            //        DueDetails = BaseDbServices.Instance.GetData("select * from tblbillingdetails where BillingId='" + dtAll.Rows[i]["BillingId"] + "'", null);
            //        lst.Add(DueDetails);
            //    }
            //    return Ok(new { dtAll, lst });
            //}

        }

        [Route("GetSelectedFee"), HttpGet]
        public IHttpActionResult GetSelectedFee()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var _class = data["Class"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Fee = new List<MySqlParameter>()
            {
                new MySqlParameter("@batch", batch),
                new MySqlParameter("@class",_class),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var id = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@batch and Class=@class and CompanyId=@CompanyId", Fee);
            if (id.Rows.Count == 0)
            {
                return Ok(false);
            }
            else
            {
                var ids = id.Rows[0]["BatchClassId"];
                var FeeDetails = new DataTable();
                List<DataTable> lst = new List<DataTable>();
               
                var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId=@CompanyId", Fee);

                for (int i = 0; i < FeeStructure.Rows.Count; i++)
                {
                    if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == "Extra")
                    {

                    }
                    else
                    {
                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + id.Rows[0]["BatchClassId"] + "' and CompanyId=@CompanyId" , Fee);


                        lst.Add(FeeDetails);
                    }
                }
                return Ok(lst);
            }
        }
        [Route("GetAllFeatureActions"), HttpGet]
        public IHttpActionResult GetAllFeatureActions()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["Company_ID"]);
            var details = BaseDbServices.Instance.GetData("Select * from tblfeatureaction where Company_ID='" + CompanyId + "'", null);
            return Json(details);
        }

        [Route("GetAllGroups"),HttpGet]
        public IHttpActionResult GetAllGroups()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["Company_ID"]);
            var details = BaseDbServices.Instance.GetData("Select * from tblgroup where Company_ID='" + CompanyId + "'", null);
            return Json(details);
        }
        [Route("GetAllFeatures"), HttpGet]
        public IHttpActionResult GetAllFeatures()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["Company_ID"]);
            var details = BaseDbServices.Instance.GetData("Select * from tblfeature where Company_ID='" + CompanyId + "'", null);
            return Json(details);
        }
        [Route("GetDueInformation"),HttpGet]
        public IHttpActionResult GetDueInformation()
        {
            var data = HttpContext.Current.Request;
            var name = data["urlName"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            if (name == "duereport")
            {
                var activeBatch = BaseDbServices.Instance.GetData("select FromYear,ToYear from tblbatchdetails where Status='Active' and CompanyId='"+CompanyId+"'", null);

                var batch = activeBatch.Rows[0]["FromYear"] + "-" + activeBatch.Rows[0]["ToYear"];

                var details = DueDbService.Instance.GetAllDueDetails(batch,CompanyId);

                for (int i = 0; i < details.Rows.Count; i++)
                {
                    var info = DueDbService.Instance.GetPartialDueByStudentId(details.Rows[i]["StudentId"].ToString(), details.Rows[i]["Batch"].ToString(),CompanyId);
                    if (info.Rows.Count > 0)
                    {
                        var totalbillings = BaseDbServices.Instance.GetData("select sum(Amount) as total from tblbillingdetails " +
                           " inner join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId " +
                           " where tblbilling.StudentId='" + details.Rows[i]["StudentId"].ToString() + "' and tblbilling.Batch='" + details.Rows[i]["Batch"].ToString() + "' and tblbilling.CompanyId='"+CompanyId+"' ", null);
                        var partialdue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as Dis,sum(Fine) as fine from tblreceipt where StudentId='" + details.Rows[i]["StudentId"].ToString() + "' and Batch='" + details.Rows[i]["Batch"].ToString() + "' and CompanyId='"+CompanyId+"'", null);
                        var total = Convert.ToInt32(totalbillings.Rows[0]["total"]) + Convert.ToInt32(partialdue.Rows[0]["fine"]) - Convert.ToInt32(partialdue.Rows[0]["dis"]);
                        var due = total - Convert.ToInt32(partialdue.Rows[0]["paid"]);

                        details.Rows[i]["due"] = due;
                    }
                    else
                    {

                    }

                }
                    if (details.Rows.Count > 0)
                    {
                        return Ok(details.AsEnumerable().Where(R => Convert.ToInt32(R["Due"]) != 0).CopyToDataTable());

                    }
            }
            else if (name == "followup" || name == "expectedpayment")
            {
                var details = DueDbService.Instance.GetAllFollowUp(CompanyId);
                return Ok(details);
            }
          
            return Ok("");
        }

        [Route("GetScholarshipInformation"), HttpGet]
        public IHttpActionResult GetScholarshipInformation()
        {
            var data = HttpContext.Current.Request;            
            var tblname = "tbl" + data["urlName"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = new DataTable();
            if (tblname == "tblscholarshipname")
            {
                details = BaseDbServices.Instance.GetData("select * from tblscholarshipname where CompanyId='" + CompanyId + "'");

            }
            else if (tblname == "tblscholarshipdetails")
            {
                details = ScholarshipDbServices.Instance.GetScholarshipDetails(CompanyId);
            }
            return Ok(details);
        }

        [Route("GetAllReceiptDetails")]
        public IHttpActionResult GetAllReceiptDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Month = data["Month"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = ReceiptDbServices.Instance.GetAllReceiptDetails(Batch, Class, Month,CompanyId);
            //int from = Convert.ToInt32(Batch.Substring(0, 4));
            //int to = Convert.ToInt32(Batch.Substring(5, 4));
            //details.Columns.Add("Status");
            //for (int i = 0; i < details.Rows.Count;i++)
            //{
            //    var Monthstn = "";
            //    var MonthInt = Convert.ToInt32(details.Rows[i]["Month"]);
            //    if (MonthInt == 12)
            //    {
            //         Monthstn = "01";
            //        from = from + 1;
            //        to = to + 1;
            //        var batch = (from + "-" + to).ToString();
            //        var info = BaseDbServices.Instance.GetData("select * from tblreceipt where StudentId='" + details.Rows[i]["Month"] + "' and Month='" + Monthstn + "' and Batch='" + batch + "'", null);
            //        if(info.Rows.Count > 0)
            //        {
            //            details.Rows[i]["Status"] = "0";
            //        }
            //        else
            //        {
            //            details.Rows[i]["Status"] = "1";
            //        }
            //    }
            //    else
            //    {
            //        var incMonth = MonthInt + 1;
            //         Monthstn = incMonth.ToString();
            //        if (Monthstn == "1")
            //        {
            //            Monthstn = "01";
            //        }
            //        else if (Monthstn == "2") { Monthstn = "02"; }
            //        else if (Monthstn == "3") { Monthstn = "03"; }
            //        else if (Monthstn == "4") { Monthstn = "04"; }
            //        else if (Monthstn == "5") { Monthstn = "05"; }
            //        else if (Monthstn == "6") { Monthstn = "06"; }
            //        else if (Monthstn == "7") { Monthstn = "07"; }
            //        else if (Monthstn == "8") { Monthstn = "08"; } else if (Monthstn == "9") { Monthstn = "09"; }
            //        var info = BaseDbServices.Instance.GetData("select * from tblreceipt where StudentId='" + details.Rows[i]["StudentId"] + "' and Month='" + Monthstn + "' and Batch='" + details.Rows[i]["Batch"] + "'", null);
            //        if (info.Rows.Count > 0)
            //        {
            //            details.Rows[i]["Status"] = "0";
            //        }
            //        else
            //        {
            //            details.Rows[i]["Status"] = "1";
            //        }
            //    }
               
            //    //var info=BaseDbServices.Instance.GetData("")
            //}
            return Ok(details);
        }

        [Route("GetAllBillingDtails/{pageNo}/{pageSize}")]
        public IHttpActionResult GetAllBillingDtails(int pageNo = 1, int pageSize = 10)
        {
            var details = BillingDbServices.Instance.GetAllBillingDetails(pageNo, pageSize);
            removeUnwantedColumns(ref details, "BillingInfo");
            return Ok(details);
        }

        [Route("GetAllDemoBillingDetails")]
        public IHttpActionResult GetAllDemoBillingDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Month = data["Month"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = BillingDbServices.Instance.GetAllDemoBillingDetails(Batch,Class,Month,CompanyId);
            return Ok(details);
        }

        [Route("GetFeeStructure"), HttpGet,AllowAnonymous]
        public IHttpActionResult GetFeeStructure()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = data["CompanyId"];
            var result = BaseDbServices.Instance.GetData("Select * from tblfeestructuredetails where CompanyId='"+ CompanyId + "'", null);
            return Json(result);
        }

        [Route("GetFeeDetails"), HttpGet, AllowAnonymous]
        public IHttpActionResult GetFeeDetails()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = data["CompanyId"];
            var FeeDetails = new DataTable();
            List<DataTable> lst = new List<DataTable>();
         
            var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where FeeStructureName !='Extra' and CompanyId='"+ CompanyId + "'", null);
            var first = FeeStructure.Rows.Count;
            for (int i = 0; i < FeeStructure.Rows.Count; i++)
            {
                FeeDetails = BaseDbServices.Instance.GetData("Select * from tblfeedetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and IsDeleted =0 and CompanyId='" + CompanyId + "'", null);
              
                lst.Add(FeeDetails);
            }
            return Json(lst);
        }

        [Route("GetAllCountryList"), HttpGet]
        public IHttpActionResult GetAllCountryList()
        {
            var data = HttpContext.Current.Request;
            var companyId = data["CompanyId"];
            var Country = BaseDbServices.Instance.GetData("Select * from tblcountrydetails where CompanyId='"+ companyId + "'", null);
            return Ok(Country);
        }

        [Route("GetAllResponseText"), HttpGet]
        public IHttpActionResult GetAllResponseText()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var Response = BaseDbServices.Instance.GetData("Select * from tblresponsetext where CompanyId='"+ CompanyId + "'", null);
            return Ok(Response);
        }

        [Route("GetAllScholarshipName"), HttpGet]
        public IHttpActionResult GetAllScholarshipName()
        {
            var data = HttpContext.Current.Request;
            var CompanyId =Convert.ToInt32(data["CompanyId"]);
            var Scholarship = BaseDbServices.Instance.GetData("Select * from tblscholarshipname where CompanyId='" + CompanyId + "'", null);
            return Ok(Scholarship);
        }

        [Route("GetScholarshipAndStudentType"), HttpGet]
        public IHttpActionResult GetScholarshipAndStudentType()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var Scholarship = new DataTable();
            var StudentType = new DataTable();
            Scholarship = BaseDbServices.Instance.GetData("Select ScholarshipName from tblscholarshipname where CompanyId='"+ CompanyId + "'", null);
            StudentType = BaseDbServices.Instance.GetData("Select StudentTypeName from tblstudenttype where CompanyId='" + CompanyId + "'", null);
            List<DataTable> lst = new List<DataTable>
            {
                Scholarship,
                StudentType
            };
            return Ok(lst);
        }

        [Route("GetAllDesignation"),HttpGet]
        public  IHttpActionResult GetAllDesignation()
        {
            var Department = new DataTable();
            var Designation = new DataTable();
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            Department = BaseDbServices.Instance.GetData("select Department from tbldepartment where CompanyId='"+CompanyId+"'", null);
            Designation = BaseDbServices.Instance.GetData("select Designation from tbldesignation where CompanyId='"+ CompanyId + "'", null);
            List<DataTable> lst = new List<DataTable>
            {
               Department,
               Designation
            };
            return Ok(lst);
        }

        [Route("GetFaculty"),HttpGet]
        public IHttpActionResult GetFaculty()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var Faculty = BaseDbServices.Instance.GetData("Select FacultyId, FacultyName from tblfacultydetails where CompanyId='"+ CompanyId + "'", null);
            return Ok(Faculty);
        }  

        [Route("CheckCompanyEmailId"),HttpPost]
        public IHttpActionResult CheckCompanyEmailId()
        {
            var data = HttpContext.Current.Request;
            var Email = data["EmailId"];
            List<MySqlParameter> EmailId = new List<MySqlParameter>()
            {
                new MySqlParameter("@email", Email)
            };
            var details = BaseDbServices.Instance.GetData("select Email from tbluserdetail where Email=@email", EmailId);
            if(details.Rows.Count > 0)
            {
                return Ok(false);
            }
            else
            {
                return Ok(true);
            }
        }

        //[Route("GetClassList"),HttpGet]
        //public IHttpActionResult GetClassList()
        //{
        //    var data = HttpContext.Current.Request;
        //    var faculty = data["Faculty"];
        //    List<MySqlParameter> Info = new List<MySqlParameter>()
        //    {
        //        new MySqlParameter("@faculty", faculty)
        //    };
        //    var id = BaseDbServices.Instance.GetData("select FacultyId from tblfacultydetails where FacultyName=@faculty", Info);
        //    var details = BaseDbServices.Instance.GetData("select ClassName from tblclassdetails where FacultyId='" + id.Rows[0]["FacultyId"] + "'", null);
        //    return Ok(details);
        //}

        [Route("GetStartingPoint"),HttpGet]
        public IHttpActionResult GetStartingPoint()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = BaseDbServices.Instance.GetData("select Place from tblstartpoint where CompanyId='"+ CompanyId + "'", null);
            return Ok(details);
        }

        [Route("GetAllDestination"),HttpGet]
        public IHttpActionResult GetAllDestination()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
           var PlaceTo = BaseDbServices.Instance.GetData("select PlaceTo from tbltransportation where CompanyId='"+ CompanyId + "'", null);
            return Ok(PlaceTo);
        }

        [Route("GetStudentType"),HttpGet]
        public IHttpActionResult GetStudentType()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = BaseDbServices.Instance.GetData("select * from tblstudenttype where CompanyId='" + CompanyId + "'", null);
            return Ok(details);
        }


        [Route("GetCompanyName"),HttpGet]
        public IHttpActionResult GetCompanyName()
        {
            var CompanyName = new DataTable();
            var CompanyEmail = new DataTable();

            CompanyName = BaseDbServices.Instance.GetData("select Name from tblcompanydetails", null);
            CompanyEmail = BaseDbServices.Instance.GetData("select Email from tbluserdetail where Role='Admin'", null);

            List<DataTable> lst = new List<DataTable>
            {
                CompanyName,
                CompanyEmail
            };
            return Ok(lst);
        }

        [Route("GetClassBatchFaculty"), HttpGet, AllowAnonymous]
        //[EnableCors("*", "*", "*")]
        public IHttpActionResult GetClassBatchFaculty()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var Class = new DataTable();
            var Batch = new DataTable();
            var Faculty = new DataTable();
            var Section = new DataTable();

            Class = BaseDbServices.Instance.GetData("Select ClassName from tblclassdetails where CompanyId='" + CompanyId + "'", null);
            Batch = BaseDbServices.Instance.GetData("Select FromYear,ToYear from tblbatchdetails where CompanyId='" + CompanyId + "' order by BatchId DESC", null);
            Faculty = BaseDbServices.Instance.GetData("Select FacultyName from tblfacultydetails where CompanyId='" + CompanyId + "'", null);
            Section = BaseDbServices.Instance.GetData("Select SectionName from tblsectiondetails where CompanyId='" + CompanyId + "'", null);

            List<DataTable> lst = new List<DataTable>
                {
                   Class,
                   Batch,
                   Faculty,
                   Section
                };
            return Json(lst);
        }

        private void removeUnwantedColumns(ref DataTable data, string name)
        {
            try
            {
                data.Columns.Remove("IsDeleted");
            }
            catch (Exception)
            { }
            switch (name)
            {
                //case "StudentDetails":
                //    data.Columns.Remove("IsDeleted");
                //    break;
                case "pdfFile":
                    data.Columns.Remove("Status");
                    data.Columns.Remove("StudentId");
                    break;
                
                case "SearchedScholarship":
                    data.Columns.Remove("SpecialScholarshipId");
                    break;
                case "BillingInfo":
                    data.Columns.Remove("row");
                    break;
               
            }
        }

        [Route("EditStudentTransportDetails"),HttpPost]
        public IHttpActionResult EditStudentTransportDetails()
        {
            var data = HttpContext.Current.Request;
            var Id = Convert.ToInt32(data["Id"]);
            var Batch = data["Batch"];
            var To = data["PlaceTo"];
            var Type = data["Type"];
            var Amount = Convert.ToInt32(data["Amount"]);
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            StudentTransportDbServices.Instance.EditStudentTransportDetails(Id, Batch,To, Type, Amount,CompanyId);
            return Ok("");
        }

        [Route("SavePaidStatus"),HttpPost]
        public IHttpActionResult SavePaidStatus()
        {
            var data = HttpContext.Current.Request;
            var billingId = data["BillingId"];
            var studentId = data["StudentId"];
            var text = data["text"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            List<MySqlParameter> Info = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@billingid",billingId),
                        new MySqlParameter("@studentid",studentId),
                        new MySqlParameter("@CompanyId",CompanyId)
                    };
          if(text == "Paid")
            {
                BaseDbServices.Instance.RunQuery("Update tblbilling set Status ='Paid' where StudentId=@studentid and BillingId=@billingid and CompanyId=@CompanyId", Info);
            }
          else if(text == "PartialPaid")
            {
                BaseDbServices.Instance.RunQuery("Update tblbilling set Status ='Partial Paid' where StudentId=@studentid and BillingId=@billingid and CompanyId=@CompanyId", Info);
            }
          
            return Ok("");
        }

        [Route("PostBatchDetails"), HttpPost]
        public IHttpActionResult PostBatchDetails()
        {
            var data = HttpContext.Current.Request;
            var from = Convert.ToInt32(data["FromYear"]);
            var to = Convert.ToInt32(data["ToYear"]);
            var companyid = Convert.ToInt32(data["CompanyId"]);
            BaseDbServices.Instance.RunQuery("Update tblbatchdetails set Status='Disable' where CompanyId='"+companyid+"'", null);

            GeneralSettingDbServices.Instance.SaveBatchDetails(from, to,companyid);
            return Ok("");
        }
        [Route("PostGroup"),HttpPost]
        public IHttpActionResult PostGroup([FromBody] Group group)
        {
            if (group.ID != 0)
            {
                UserAccessDbServices.Instance.PostGroup(group);
            }
            else
            {
                var allGroup = BaseDbServices.Instance.GetData("select * from tblgroup where Company_ID='" + group.Company_ID + "'");
                if (allGroup.Rows.Count > 0)
                {
                    for (int i = 0; i < allGroup.Rows.Count; i++)
                    {
                        if (allGroup.Rows[i]["UserRole"].ToString() == group.UserRole)
                        {
                            return Json("");
                        }
                    }

                }
                UserAccessDbServices.Instance.PostGroup(group);
            }
                return Ok("");
            
        }
        [Route("PostFeatureAction"),HttpPost]
        public IHttpActionResult PostFeatureAction([FromBody] FeatureAction featureAction)
        {
            if(featureAction.ID !=0)
            {
                UserAccessDbServices.Instance.PostFeatureAction(featureAction);
            }
            else
            {
                var allFeatureAction = BaseDbServices.Instance.GetData("select * from tblfeatureaction where Company_ID='" + featureAction.Company_ID + "'");
                if (allFeatureAction.Rows.Count > 0)
                {
                    for (int i = 0; i < allFeatureAction.Rows.Count; i++)
                    {
                        if (allFeatureAction.Rows[i]["Name"].ToString() == featureAction.Name)
                        {
                            return Json("");
                        }
                    }

                }
                UserAccessDbServices.Instance.PostFeatureAction(featureAction);
                
            }
            return Ok("");
        }

        [Route("PostFeature"), HttpPost]
        public IHttpActionResult PostFeature([FromBody] Feature feature)
        {
            if(feature.ID !=0)
            {
                UserAccessDbServices.Instance.PostFeature(feature);
            }
            else
            {
                var allFeature = BaseDbServices.Instance.GetData("select * from tblfeature where Company_ID='" + feature.Company_ID + "'");
                if (allFeature.Rows.Count > 0)
                {
                    for (int i = 0; i < allFeature.Rows.Count; i++)
                    {
                        if (allFeature.Rows[i]["Name"].ToString() == feature.Name)
                        {
                            return Json("");
                        }
                    }

                }
            UserAccessDbServices.Instance.PostFeature(feature);
            }
            return Ok("");
        }

        [Route("PostDayOff"), HttpPost]
        public IHttpActionResult PostDayOff([FromBody] DayOff obj)
        {
            int id = 0;
            List<MySqlParameter> Info = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",obj.DayOffId),
                new MySqlParameter("@DateFrom",obj.DateFrom),
                new MySqlParameter("@Companyid",obj.CompanyId)
            };
            if (obj.DayOffId != 0)
            {
                id = DayOffDbServices.Instance.SaveDayOff(obj);
                 BaseDbServices.Instance.RunQuery("Delete from tbldayoffdetails where DayOffId=@id and CompanyId=@Companyid", Info);
                //DayOffDbServices.Instance.SaveDayOffDetails(obj.DayOffId,obj);
            }
            else
            {
                 id = DayOffDbServices.Instance.SaveDayOff(obj);
            }
             if (obj.Department == null)
                {

                    var classlist = BaseDbServices.Instance.GetData("select tblfacultydetails.FacultyName,tblclassdetails.ClassName" +
                        " FROM tblclassdetails" +
                        " inner join tblfacultydetails on tblclassdetails.FacultyId = tblfacultydetails.FacultyId " +
                        " where tblfacultydetails.CompanyId=@Companyid and tblclassdetails.CompanyId=@Companyid", Info);
                    for (int i = 0; i < classlist.Rows.Count; i++)
                    {
                        BaseDbServices.Instance.RunQuery("delete tbldayoffdetails from tbldayoffdetails " +
                            " inner join tbldayoff on tbldayoffdetails.DayOffId=tbldayoff.DayOffId" +
                            " where Department='Student' and Faculty='" + classlist.Rows[i]["FacultyName"].ToString() + "' " +
                            " and Class='" + classlist.Rows[i]["ClassName"].ToString() + "' and DateFrom=@DateFrom" +
                            " and tbldayoffdetails.CompanyId=@Companyid and tbldayoff.CompanyId=@Companyid", Info);
                        DayOffDbServices.Instance.SaveDayOffDetails(id, "Student", classlist.Rows[i]["FacultyName"].ToString(), classlist.Rows[i]["ClassName"].ToString(),Convert.ToInt32(obj.CompanyId));
                    }
                    var departmentList = BaseDbServices.Instance.GetData("select Department from tbldepartment where Department !='Student' and CompanyId=@Companyid",Info);
                    for (int i = 0; i < departmentList.Rows.Count; i++)
                    {
                        if (departmentList.Rows[i]["Department"].ToString() == "Teacher")
                        {
                            foreach (var m in obj.Faculty)
                            {
                                BaseDbServices.Instance.RunQuery("delete tbldayoffdetails from tbldayoffdetails " +
                                    " inner join tbldayoff on tbldayoffdetails.DayOffId=tbldayoff.DayOffId " +
                                    " where Department='" + departmentList.Rows[i]["Department"] + "' and Faculty='" + m + "' and DateFrom=@DateFrom and tbldayoffdetails.CompanyId=@Companyid and tbldayoff.CompanyId=@Companyid", Info);
                                DayOffDbServices.Instance.SaveDayOffDetails(id, departmentList.Rows[i]["Department"].ToString(), m, null, Convert.ToInt32(obj.CompanyId));
                            }

                        }
                        else
                        {
                            BaseDbServices.Instance.RunQuery("delete tbldayoffdetails from tbldayoffdetails " +
                                " inner join tbldayoff on tbldayoffdetails.DayOffId=tbldayoff.DayOffId " +
                                " where Department='" + departmentList.Rows[i]["Department"] + "' and DateFrom=@DateFrom and tbldayoffdetails.CompanyId=@Companyid and tbldayoff.CompanyId=@Companyid", Info);
                            DayOffDbServices.Instance.SaveDayOffDetails(id, departmentList.Rows[i]["Department"].ToString(), null, null, Convert.ToInt32(obj.CompanyId));
                        }
                    }
                }
                else
                {
                    foreach(var r in obj.Department)
                    {
                        if(r == "Student")
                        {
                            if (obj.Faculty == null)
                            {
                                var classlist = BaseDbServices.Instance.GetData("select tblfacultydetails.FacultyName,tblclassdetails.ClassName" +
                               " FROM tblclassdetails" +
                              " inner join tblfacultydetails on tblclassdetails.FacultyId = tblfacultydetails.FacultyId " +
                              "where tblfacultydetails.CompanyId=@Companyid and tblclassdetails.CompanyId=@Companyid",Info);
                                for (int i = 0; i < classlist.Rows.Count; i++)
                                {
                                    BaseDbServices.Instance.RunQuery("delete tbldayoffdetails from tbldayoffdetails " +
                                        " inner join tbldayoff on tbldayoffdetails.DayOffId=tbldayoff.DayOffId " +
                                        " where Department='Student' and Faculty='" + classlist.Rows[i]["FacultyName"].ToString() + "' " +
                                    " and Class='" + classlist.Rows[i]["ClassName"].ToString() + "' and DateFrom=@DateFrom " +
                                    " and tbldayoffdetails.CompanyId=@Companyid and tbldayoff.CompanyId=@Companyid", Info);

                                    DayOffDbServices.Instance.SaveDayOffDetails(id,"Student", classlist.Rows[i]["FacultyName"].ToString(), classlist.Rows[i]["ClassName"].ToString(), Convert.ToInt32(obj.CompanyId));
                                }

                            }
                            else
                            {
                                if(obj.Class == null)
                                {
                                    foreach(var m in obj.Faculty)
                                    {
                                        var facultyid = BaseDbServices.Instance.GetData("select FacultyId from tblfacultydetails where FacultyName='" + m + "' and CompanyId=@Companyid",Info);
                                        var list= BaseDbServices.Instance.GetData("select ClassName from tblclassdetails where FacultyId='" + facultyid.Rows[0]["FacultyId"] + "' and CompanyId=@Companyid", Info);
                                        for (int i = 0; i < list.Rows.Count; i++)
                                        {
                                            BaseDbServices.Instance.RunQuery("delete tbldayoffdetails from tbldayoffdetails " +
                                                " inner join tbldayoff on tbldayoffdetails.DayOffId=tbldayoff.DayOffId " +
                                                " where Department='Student' and Faculty='" + m + "' " +
                                            " and Class='" + list.Rows[i]["ClassName"].ToString() + "' and DateFrom=@DateFrom " +
                                            " and tbldayoffdetails.CompanyId=@Companyid and tbldayoff.CompanyId=@Companyid", Info);

                                            DayOffDbServices.Instance.SaveDayOffDetails(id,"Student", m , list.Rows[i]["ClassName"].ToString(), Convert.ToInt32(obj.CompanyId));
                                        }
                                    }
                                }
                                else
                                {
                                    foreach(var n in obj.Class)
                                    {
                                        var facultyId= BaseDbServices.Instance.GetData("select FacultyId from tblclassdetails where ClassName='" + n + "' and CompanyId=@Companyid", Info);
                                    var faculty= BaseDbServices.Instance.GetData("select FacultyName from tblfacultydetails where FacultyId='" + facultyId.Rows[0]["FacultyId"] + "' and CompanyId=@Companyid", Info);
                                    BaseDbServices.Instance.RunQuery("delete tbldayoffdetails from tbldayoffdetails " +
                                            " inner join tbldayoff on tbldayoffdetails.DayOffId=tbldayoff.DayOffId " +
                                            " where Department='Student' and Faculty='" + faculty.Rows[0]["FacultyName"].ToString() + "' " +
                                            " and Class='" + n + "' and DateFrom=@DateFrom " +
                                            " and tbldayoffdetails.CompanyId=@Companyid and tbldayoff.CompanyId=@Companyid", Info);
                                    DayOffDbServices.Instance.SaveDayOffDetails(id,"Student",faculty.Rows[0]["FacultyName"].ToString(),n,Convert.ToInt32(obj.CompanyId));
                                    }
                                }

                            }

                        }
                        else if(r == "Teacher")
                        {
                        if (obj.Faculty == null)
                        {
                            var facultyList = BaseDbServices.Instance.GetData("Select FacultyName from tblfacultydetails where CompanyId=@Companyid", Info);
                            for (int i = 0; i < facultyList.Rows.Count; i++)
                            {
                                BaseDbServices.Instance.RunQuery("delete tbldayoffdetails from tbldayoffdetails " +
                                    " inner join tbldayoff on tbldayoffdetails.DayOffId=tbldayoff.DayOffId " +
                                    " where Department='Teacher' and Faculty='" + facultyList.Rows[i]["FacultyName"].ToString() + "' and DateFrom=@DateFrom " +
                                    " and tbldayoffdetails.CompanyId = @Companyid and tbldayoff.CompanyId = @Companyid", Info);
                                DayOffDbServices.Instance.SaveDayOffDetails(id, "Teacher", facultyList.Rows[i]["FacultyName"].ToString(), null, Convert.ToInt32(obj.CompanyId));
                            }
                        }
                        else
                        {
                            foreach (var m in obj.Faculty)
                            {
                                BaseDbServices.Instance.RunQuery("delete tbldayoffdetails from tbldayoffdetails " +
                                    " inner join tbldayoff on tbldayoffdetails.DayOffId=tbldayoff.DayOffId " +
                                    " where Department='Teacher' and Faculty='" + m + "' and DateFrom=@DateFrom " +
                                    " and tbldayoffdetails.CompanyId = @Companyid and tbldayoff.CompanyId = @Companyid", Info);
                                DayOffDbServices.Instance.SaveDayOffDetails(id, "Teacher", m, null, Convert.ToInt32(obj.CompanyId));
                            }
                        }
                        }
                        else
                        {
                            BaseDbServices.Instance.RunQuery("delete tbldayoffdetails from tbldayoffdetails " +
                                " inner join tbldayoff on tbldayoffdetails.DayOffId=tbldayoff.DayOffId " +
                                " where Department='" + r + "' and DateFrom=@DateFrom " +
                                " and tbldayoffdetails.CompanyId = @Companyid and tbldayoff.CompanyId = @Companyid", Info);
                            DayOffDbServices.Instance.SaveDayOffDetails(id,r,null,null,Convert.ToInt32(obj.CompanyId));

                        }
                    }
                }
              
           

            return Ok("");
        }

        [Route("SaveAdvancedPaid"),HttpPost]
        public IHttpActionResult SaveAdvancedPaid()
        {
            var data = HttpContext.Current.Request;
            var StudentId = data["StudentId"];
            var Amount = Convert.ToInt32(data["Amount"]);
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var ReceiptId = Convert.ToInt32(data["ReceiptId"]);
            ReceiptDbServices.Instance.SaveAdvancedPaid(StudentId, Amount,CompanyId,ReceiptId);
            return Ok("");
        }

        [Route("PostClassDetails"), HttpPost]
        public IHttpActionResult PostClassDetails([FromBody] Class obj)
        {
            GeneralSettingDbServices.Instance.SaveClassDetails(obj);
            return Ok("");
        }

        [Route("PostUserInfo"),HttpPost]
        public IHttpActionResult PostUserInfo([FromBody] User obj)
        {
            byte[] byts = System.Text.Encoding.UTF8.GetBytes(obj.Password);
            obj.Password = Convert.ToBase64String(byts);
            UserDbServices.Instance.SaveUserInfo(obj);
            return Ok("");
        }

        [Route("PostFollowUpDetails"), HttpPost]
        public IHttpActionResult PostFollowUpDetails([FromBody] FollowUp obj)
        {
            DueDbService.Instance.SaveFollowUpDetails(obj);
            return Ok("");
        }

        [Route("PostTransportDetails"),HttpPost]
        public IHttpActionResult PostTransportDetails([FromBody] Transportation obj)
        {
            obj.PlaceTo = obj.PlaceTo.ToLower();
            List<MySqlParameter> transport = new List<MySqlParameter>()
            {
                new MySqlParameter("@placeto",obj.PlaceTo),
                new MySqlParameter("@CompanyId",obj.CompanyId)
            };
            if(obj.TransportationId !=0)
            {
                TransportDbServices.Instance.PostTransportDetails(obj);
            }
            else
            {
                var details = BaseDbServices.Instance.GetData("Select * from tbltransportation where PlaceTo=@placeto and CompanyId=@CompanyId", transport);
                if(details.Rows.Count > 0)
                {
                    BaseDbServices.Instance.RunQuery("delete from tbltransportation where PlaceTo=@placeto and CompanyId=@CompanyId", transport);
                }
                TransportDbServices.Instance.PostTransportDetails(obj);
            }
            return Ok("");
        }

        [Route("PostFineDetails"),HttpPost]
        public IHttpActionResult PostFineDetails([FromBody] Fine obj)
        {
            FineDbServices.Instance.PostFineDetails(obj);
            return Ok("");
        }

        [Route("DeleteFineDetails"),HttpPost]
        public IHttpActionResult DeleteFineDetails()
        {
            var data = HttpContext.Current.Request;
            var FineId = data["FineId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                 new MySqlParameter("@id",FineId),
                 new MySqlParameter("@companyId",CompanyId)
            };
            BaseDbServices.Instance.RunQuery("Delete from tblfine where FineId=@id and CompanyId=@companyId", Id);
            return Ok("");
        }

        [Route("DeleteTransportDetails"), HttpPost]
        public IHttpActionResult DeleteTransportDetails()
        {
            var data = HttpContext.Current.Request;
            var id = data["Id"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                 new MySqlParameter("@id",id),
                 new MySqlParameter("@companyid",CompanyId)
            };
            BaseDbServices.Instance.RunQuery("Delete from tbltransportation where TransportationId=@id and CompanyId=@companyid", Id);
            return Ok("");
        }

        [Route("GetAllFineDetails"),HttpGet]
        public IHttpActionResult GetAllFineDetails()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = BaseDbServices.Instance.GetData("select * from tblfine where CompanyId='"+CompanyId+"'", null);
            return Ok(details);
        }

        [Route("GetAllTransportInfo"), HttpGet]
        public IHttpActionResult GetAllTransportInfo()
        {
            var data = HttpContext.Current.Request;
            var tblname = "tbl" + data["urlName"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = new DataTable();
            if(tblname == "tblstartpoint")
            {
                details = BaseDbServices.Instance.GetData("select * from tblstartpoint where CompanyId='"+CompanyId+"'", null);
            }
            else if(tblname == "tbltransportationdetails")
            {
                details = BaseDbServices.Instance.GetData("select * from tbltransportation where CompanyId='" + CompanyId + "'", null);
            }
            return Ok(details);
        }

        [Route("PostFacultyDetails"), HttpPost]
        public IHttpActionResult PostFacultyDetails([FromBody] Faculty obj)
        {
            GeneralSettingDbServices.Instance.SaveFacultyDetails(obj);
            return Ok("");
        }

        [Route("PostStartDate"),HttpPost]
        public IHttpActionResult PostStartDate([FromBody] StartDate obj)
        {
            List<MySqlParameter> batch = new List<MySqlParameter>()
            {
                new MySqlParameter("@batch",obj.Batch),
                new MySqlParameter("@companyid",obj.CompanyId)
            };
           if(obj.StartDateId !=0)
            {
            GeneralSettingDbServices.Instance.SaveStartDate(obj);

            }
            else
            {
                var details = BaseDbServices.Instance.GetData("select Batch from tblstartdate where Batch=@batch and CompanyId=@companyid",batch);
                if(details.Rows.Count > 0)
                {
                    BaseDbServices.Instance.RunQuery("Delete from tblstartdate where Batch=@batch and CompanyId=@companyid", batch);
                    GeneralSettingDbServices.Instance.SaveStartDate(obj);
                }
                else
                {
                    GeneralSettingDbServices.Instance.SaveStartDate(obj);
                }
            }
            return Ok("");
        }

        [Route("EditBatchDetails"), HttpPost]
        public IHttpActionResult EditBatchDetails([FromBody] Batch obj)
        {
            GeneralSettingDbServices.Instance.EditBatchDetails(obj);
            return Ok("");
        }

        [Route("PostReceiptDetails"), HttpPost]
        public IHttpActionResult PostReceiptDetails([FromBody] Receipt obj)
        {
            List<MySqlParameter> Info = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@id",obj.StudentId),
                        new MySqlParameter("@month",obj.Month),
                        new MySqlParameter("@billingId",obj.BillingId),
                        new MySqlParameter("@CompanyId",obj.CompanyId)
                    };
            BaseDbServices.Instance.RunQuery("Update tblbilling set IsCreated ='1' where BillingId=@billingId and CompanyId=@CompanyId", Info);
           
          
            int id = ReceiptDbServices.Instance.SaveReceiptDetails(obj);
            BaseDbServices.Instance.RunQuery("Update tbladvancedpaid set Status='Used' where StudentId=@id and CompanyId=@CompanyId", Info);
            return Ok(id);
        }

        [Route("PostDiscountDetails"), HttpPost]
        public IHttpActionResult PostDiscountDetails([FromBody] ScholarshipDetails obj)
        {
            if (obj.Id != 0)
            {

                List<MySqlParameter> Name = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@name",obj.ScholarshipName),
                        new MySqlParameter("@batch",obj.Batch),
                        new MySqlParameter("@class",obj.Class),
                        new MySqlParameter("@faculty",obj.Faculty),
                        new MySqlParameter("@CompanyId",obj.CompanyId)
                    };
                BaseDbServices.Instance.RunQuery("Delete from tblscholarshipdetails where ScholarshipName=@name and Batch=@batch and Class=@class and Faculty=@faculty and CompanyId=@CompanyId", Name);
                obj.Id = 0;
                ScholarshipDbServices.Instance.SaveDiscountDetails(obj);
                return Ok(true);
            }
            else
            {
                var ScholarshipName = BaseDbServices.Instance.GetData("Select ScholarshipName,Batch,Class,Faculty,CompanyId from tblscholarshipdetails", null);

                for (int i = 0; i < ScholarshipName.Rows.Count; i++)
                {
                    if (ScholarshipName.Rows[i]["ScholarshipName"].ToString() == obj.ScholarshipName &&
                        ScholarshipName.Rows[i]["Batch"].ToString() == obj.Batch &&
                        ScholarshipName.Rows[i]["Class"].ToString() == obj.Class &&
                        ScholarshipName.Rows[i]["Faculty"].ToString() == obj.Faculty &&
                       Convert.ToInt32(ScholarshipName.Rows[i]["CompanyId"]) == obj.CompanyId)
                    {
                        return Ok(false);

                    }
                }
                ScholarshipDbServices.Instance.SaveDiscountDetails(obj);
                return Ok(true);
            }
        }
        [Route("PostClassFeeDetails"), HttpPost]
        public IHttpActionResult PostClassFeeDetails([FromBody] BatchClass obj)
        {
            if (obj.BatchClassId != 0)
            {
                List<MySqlParameter> Id = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@id",obj.BatchClassId),
                        new MySqlParameter("@companyid",obj.CompanyId)
                    };
                BaseDbServices.Instance.RunQuery("Delete from tblbatchclassfee where BatchClassId = @id and CompanyId=@companyid", Id);
                GeneralSettingDbServices.Instance.SaveClassFeeDetails(obj);
               foreach(var r in obj.ClassFeeManagement)
                {
                    List<MySqlParameter> fee = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@amount",r.Amount),
                        new MySqlParameter("@id",obj.BatchClassId),
                        new MySqlParameter("@feeid",r.FeeId)
                    };

                    BaseDbServices.Instance.RunQuery("Update tblspecialscholarship set Amount=@amount where BatchClassId=@id and FeeId=@feeid", fee);
                }
                return Ok(true);
            }
            else
            {
                var classDetails = BaseDbServices.Instance.GetData("Select Batch,Class,Faculty from tblbatchclass where CompanyId='"+obj.CompanyId+"'", null);
                for (int i = 0; i < classDetails.Rows.Count; i++)
                {
                    if (classDetails.Rows[i]["Batch"].ToString() == obj.Batch &&
                        classDetails.Rows[i]["Class"].ToString() == obj.Class &&
                        classDetails.Rows[i]["Faculty"].ToString() == obj.Faculty)
                    {
                        return Ok(false);
                    }
                }
                GeneralSettingDbServices.Instance.SaveClassFeeDetails(obj);
                return Ok(true);

            }

        }

        [Route("PostStudentTransportDetails"),HttpPost]
        public IHttpActionResult PostStudentTransportDetails([FromBody] StudentTransport obj)
        {
            StudentTransportDbServices.Instance.SaveStudentTransportDetails(obj);
            return Ok("");
        }


        [Route("PostStudentExtraFee"), HttpPost]
        public IHttpActionResult PostStudentExtraFee([FromBody] StudentExtraFee obj)
        {
            var studentlist = new DataTable();
            
            if (obj.Batch != null && obj.Faculty == null)
            {
                studentlist = BaseDbServices.Instance.GetData("select StudentId from tblstudentinfo where IsDeleted=0 and Status='Active' and CompanyId='"+obj.CompanyId+"'", null);
            }
            else if(obj.Batch !=null && obj.Faculty !=null && obj.Class == null)
            {
                List<MySqlParameter> Info = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@batch",obj.Batch),
                        new MySqlParameter("@faculty",obj.Faculty),
                        new MySqlParameter("@CompanyId",obj.CompanyId)
                    };
                studentlist = BaseDbServices.Instance.GetData("select tblstudentinfo.StudentId from tblstudentinfo" +
                    " inner join tblcurrenteducation on tblstudentinfo.StudentId=tblcurrenteducation.StudentId" +
                    " where tblstudentinfo.IsDeleted=0 and tblstudentinfo.Status='Active'" +
                    " and tblcurrenteducation.Faculty=@faculty and tblstudentinfo.CompanyId=@CompanyId", Info);
            }
            else if(obj.Batch != null && obj.Faculty != null && obj.Class !=null && obj.Section == null)
            {
                List<MySqlParameter> Info = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@batch",obj.Batch),
                        new MySqlParameter("@faculty",obj.Faculty),
                        new MySqlParameter("@class",obj.Class),
                         new MySqlParameter("@CompanyId",obj.CompanyId)
                    };
                studentlist = BaseDbServices.Instance.GetData("select tblstudentinfo.StudentId from tblstudentinfo" +
                    " inner join tblcurrenteducation on tblstudentinfo.StudentId=tblcurrenteducation.StudentId" +
                    " where tblstudentinfo.IsDeleted=0 and tblstudentinfo.Status='Active'" +
                    " and tblcurrenteducation.Class=@class and tblstudentinfo.CompanyId=@CompanyId", Info);
            }
            else if (obj.Batch != null && obj.Faculty != null && obj.Class != null && obj.Section != null)
            {
                List<MySqlParameter> Info = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@batch",obj.Batch),
                        new MySqlParameter("@faculty",obj.Faculty),
                        new MySqlParameter("@class",obj.Class),
                        new MySqlParameter("@section",obj.Section),
                        new MySqlParameter("@CompanyId",obj.CompanyId)
                    };
                studentlist = BaseDbServices.Instance.GetData("select tblstudentinfo.StudentId from tblstudentinfo" +
                    " inner join tblcurrenteducation on tblstudentinfo.StudentId=tblcurrenteducation.StudentId" +
                    " where tblstudentinfo.IsDeleted=0 and tblstudentinfo.Status='Active'" +
                    " and tblcurrenteducation.Class=@class and tblcurrenteducation.Section=@section and tblstudentinfo.CompanyId=@CompanyId ", Info);
            }
            for (int i=0;i < studentlist.Rows.Count;i++)
            {
                StudentExtraFeeDbServices.Instance.SaveStudentExtraFee(obj, studentlist.Rows[i]["StudentId"].ToString());
            }
            return Ok("");
        }

        [Route("PostStudentExtraFeeByName"), HttpPost]
        public IHttpActionResult PostStudentExtraFeeByName([FromBody] StudentExtraFee obj)
        {
            StudentExtraFeeDbServices.Instance.SaveStudentExtraFeeDetails(obj);
            return Ok("");
        }

        [Route("PostSpecialScholarship"), HttpPost]
        public IHttpActionResult PostSpecialScholarship([FromBody] SpecialScholarship obj)
        {
                List<MySqlParameter> Id = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@id",obj.StudentId),
                        new MySqlParameter("@batch",obj.Batch),
                        new MySqlParameter("@CompanyId",obj.CompanyId)
                    };
            if (obj.SpecialScholarshipId != 0)
            {
                BaseDbServices.Instance.RunQuery("Delete from tblspecialscholarship where StudentId =@id and Batch=@batch and CompanyId=@CompanyId", Id);
                ScholarshipDbServices.Instance.SaveSpecialScholarship(obj);
                return Ok(true);
            }
            else
            {
                var StudentId = BaseDbServices.Instance.GetData("Select StudentId from tblspecialscholarship where StudentId =@id and Batch=@batch",Id);
                if(StudentId.Rows.Count != 0)
                {
                    return Ok(false);
                }
                ScholarshipDbServices.Instance.SaveSpecialScholarship(obj);
                return Ok(true);
            }

        }



        [Route("PostSectionDetails"), HttpPost]
        public IHttpActionResult PostSectionDetails([FromBody] Section obj)
        {
            GeneralSettingDbServices.Instance.SaveSectionDetails(obj);
            return Ok("");
        }

        [Route("PostCountryDetails"), HttpPost]
        public IHttpActionResult PostCountryDetails([FromBody] Country obj)
        {
            GeneralSettingDbServices.Instance.SaveCountryDetails(obj);
            return Ok("");
        }

        [Route("PostResponseText"),HttpPost]
        public IHttpActionResult PostResponseText([FromBody] ResponseText obj)
        {
            GeneralSettingDbServices.Instance.SaveResponseText(obj);
            return Ok("");
        }

        [Route("PostStateDetails"), HttpPost]
        public IHttpActionResult PostStateDetails([FromBody] State obj)
        {
            GeneralSettingDbServices.Instance.SaveStateDetails(obj);
            return Ok("");
        }

        [Route("PostStudentType"), HttpPost]
        public IHttpActionResult PostStudentType([FromBody] StudentType obj)
        {
            GeneralSettingDbServices.Instance.SaveStudentType(obj);
            return Ok("");
        }

        [Route("PostFeeStructure"), HttpPost]
        public IHttpActionResult PostFeeStructure([FromBody] FeeStructure obj)
        {
            GeneralSettingDbServices.Instance.SaveFeeStructure(obj);
            return Ok("");
        }

        [Route("PostScholarshipDetails"), HttpPost]
        public IHttpActionResult PostScholarshipDetails([FromBody] Scholarship obj)
        {
            ScholarshipDbServices.Instance.SaveScholarshipName(obj);
            return Ok("");
        }

        [Route("GetTotalHolidays"),HttpGet]
        public IHttpActionResult GetTotalHolidays()
        {
            var activeBatch = BaseDbServices.Instance.GetData("select FromYear,ToYear from tblbatchdetails where Status='Active'", null);
            if (activeBatch.Rows.Count > 0)
            {
                var batch = activeBatch.Rows[0]["FromYear"] + "-" + activeBatch.Rows[0]["ToYear"];
                var className = BaseDbServices.Instance.GetData("select Distinct Class from tbldayoffdetails" +
                    " inner join tbldayoff on tbldayoff.DayOffId=tbldayoffdetails.DayOffId" +
                    " where Class != 'Null' and Batch ='" + batch + "'", null);
                DataTable lst = new DataTable();
                lst.Columns.Add("Class", typeof(string));
                lst.Columns.Add("Total", typeof(int));
                for (int i = 0; i < className.Rows.Count; i++)
                {
                    var classholiday = BaseDbServices.Instance.GetData("select Class,DateFrom,DateTo from tbldayoffdetails" +
                        " inner join tbldayoff on tbldayoff.DayOffId=tbldayoffdetails.DayOffId " +
                        " where Class= '" + className.Rows[i]["Class"].ToString() + "'");
                    int counter = 0;
                    for (int j = 0; j < classholiday.Rows.Count; j++)
                    {
                        DateTime d1 = Convert.ToDateTime(classholiday.Rows[j]["DateFrom"]);
                        DateTime d2 = Convert.ToDateTime(classholiday.Rows[j]["DateTo"]);
                        TimeSpan span = d2 - d1;
                        counter += span.Days + 1;
                    }
                    lst.Rows.Add(new Object[]{
                className.Rows[i]["Class"].ToString(),
                counter,

           });
                }
               
                List<DataTable> result = new List<DataTable>
            {
                lst
            };
                return Ok(result);
            }
            else
            {
                return Ok("");
            }
        }

        [Route("GetStates"), HttpGet]
        public IHttpActionResult GetStates()
        {
            var data = HttpContext.Current.Request;
            var name = data["CountryName"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Country = new List<MySqlParameter>()
            {
                new MySqlParameter("@Country", name)
            };
            var details = BaseDbServices.Instance.GetData("Select StateName from tblstatedetails where CountryName=@Country and CompanyId='" + CompanyId + "'", Country);
            return Ok(details);
        }

        [Route("EditFeeStructure"), HttpPost]
        public IHttpActionResult EditFeeStructure()
        {
            var data = HttpContext.Current.Request;
            var OldName = data["OldName"];
            var NewName = data["NewName"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Name = new List<MySqlParameter>()
            {
                new MySqlParameter("@oldname", OldName),
                new MySqlParameter("@newname", NewName),
                new MySqlParameter("@companyid", CompanyId)
            };
            BaseDbServices.Instance.RunQuery("Update tblfeestructuredetails set FeeStructureName=@newname where FeeStructureName=@oldname and CompanyId=@companyid;" +
            "Update tblfeedetails set FeeStructureName=@newname where FeeStructureName=@oldname and CompanyId=@companyid;" +
            "Update tblbatchclassfee set FeeStructureName=@newname where FeeStructureName=@oldname and CompanyId=@companyid;" +
            "Update tblscholarshipdetails set FeeStructureName=@newname where FeeStructureName=@oldname and CompanyId=@companyid;" +
            "Update tblspecialscholarship set FeeStructureName=@newname where FeeStructureName=@oldname and CompanyId=@companyid;", Name);
            return Ok("");
        }

        [Route("EditCountryDetails"), HttpPost]
        public IHttpActionResult EditCountryDetails()
        {
            var data = HttpContext.Current.Request;
            var OldName = data["OldName"];
            var NewName = data["NewName"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Name = new List<MySqlParameter>()
            {
                new MySqlParameter("@oldname", OldName),
                new MySqlParameter("@newname", NewName),
                new MySqlParameter("@companyid",CompanyId)
            };
            BaseDbServices.Instance.RunQuery("Update tblstatedetails set CountryName=@newname where CountryName=@oldname and CompanyId=@CompanyId;" +
            "Update tblcountrydetails set CountryName=@newname where CountryName=@oldname and CompanyId=@CompanyId", Name);
            return Ok("");
        }

       

        

        [Route("EditResponsetext"), HttpPost]
        public IHttpActionResult EditResponsetext()
        {
            var data = HttpContext.Current.Request;
            var OldName = data["OldName"];
            var NewName = data["NewName"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Name = new List<MySqlParameter>()
            {
                new MySqlParameter("@oldname", OldName),
                new MySqlParameter("@newname", NewName),
                new MySqlParameter("@companyid", CompanyId)
            };
            BaseDbServices.Instance.RunQuery("Update tblresponsetext set Responsetext=@newname where Responsetext=@oldname and CompanyId=@companyid",Name);
            return Ok("");
        }

        [Route("EditStudentType"), HttpPost]
        public IHttpActionResult EditStudentType()
        {
            var data = HttpContext.Current.Request;
            var OldName = data["OldName"];
            var NewName = data["NewName"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Name = new List<MySqlParameter>()
            {
                new MySqlParameter("@oldname", OldName),
                new MySqlParameter("@newname", NewName),
                new MySqlParameter("@id", CompanyId)
            };
            BaseDbServices.Instance.RunQuery("Update tblstudenttype set StudentTypeName=@newname where StudentTypeName=@oldname and CompanyId=@id", Name);
            return Ok("");
        }

        [Route("PostFeeDetails"), HttpPost]
        public IHttpActionResult PostFeeDetails([FromBody] FeeManagement obj)
        {
            GeneralSettingDbServices.Instance.SaveFeeDetails(obj);
            return Ok("");
        }

        [Route("DeleteScholarshipDetails"), HttpPost]
        public IHttpActionResult DeleteScholarshipDetails()
        {
            var data = HttpContext.Current.Request;
            var name = data["Name"];
            var batch = data["Batch"];
            var _class = data["Class"];
            var faculty = data["Faculty"];
            var CompanyId = data["CompanyId"];

            List<MySqlParameter> Param = new List<MySqlParameter>()
            {
                new MySqlParameter("@name", name),
                new MySqlParameter("batch",batch),
                new MySqlParameter("@class",_class),
                new MySqlParameter("@faculty",faculty),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            BaseDbServices.Instance.RunQuery("Delete from tblscholarshipdetails where ScholarshipName=@name and Batch=@batch and Class=@class and Faculty=@faculty and CompanyId=@CompanyId", Param);
            return Ok("");
        }
        [Route("DeleteFeatureAction"),HttpPost]
        public IHttpActionResult DeleteFeatureAction()
        {
            var data = HttpContext.Current.Request;
            var id = data["ID"];
            BaseDbServices.Instance.RunQuery("Delete from tblfeatureaction where ID='" + id + "' ", null);
            return Ok("");
        }
        [Route("DeleteGroup"),HttpPost]
        public IHttpActionResult DeleteGroup()
        {
            var data = HttpContext.Current.Request;
            var id = data["ID"];
            BaseDbServices.Instance.RunQuery("Delete from tblgroup where ID='" + id + "' ", null);
            return Ok("");
        }
        [Route("DeleteFeature"), HttpPost]
        public IHttpActionResult DeleteFeature()
        {
            var data = HttpContext.Current.Request;
            var id = data["ID"];
            BaseDbServices.Instance.RunQuery("Delete from tblfeature where ID='" + id + "' ", null);
            return Ok("");
        }
        [Route("DeleteGeneralSetting"), HttpPost]
        public IHttpActionResult DeleteGeneralSetting()
        {
            var data = HttpContext.Current.Request;
            var tblName = "tbl" + data["urlName"];
            var id = data["id"];
            var name = data["name"];
            var strcuture = data["structure"];
            var Companyid = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@id", id)
            };
            List<MySqlParameter> Name = new List<MySqlParameter>()
            {
                new MySqlParameter("@name", name)
            };
            List<MySqlParameter> FeeStructure = new List<MySqlParameter>()
            {
                new MySqlParameter("@name", name),
                new MySqlParameter("@structure", strcuture)
            };

            List<MySqlParameter> Batch = new List<MySqlParameter>()
            {
                new MySqlParameter("@id", id),
                new MySqlParameter("@batch",name)
            };


            if (tblName == "tblbatchdetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where BatchId = @id and CompanyId='"+ Companyid + "'", Id);
            }
            else if(tblName == "tblresponsetext")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where ResponseTextId = @id", Id);
            }
            else if (tblName == "tblstartdate")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where StartDateId = @id and CompanyId='" + Companyid + "'", Id);
            }
            else if(tblName == "tbldayoffdetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where DayOffId = @id", Id);
                BaseDbServices.Instance.RunQuery("Delete from tbldayoff where DayOffId = @id", Id);
            }
            else if (tblName == "tblclassdetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where ClassId = @id and CompanyId='" + Companyid + "'", Id);
            }
            else if (tblName == "tblfacultydetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from tblclassdetails where FacultyId=@id and CompanyId='"+ Companyid + "'", Id);
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where FacultyId = @id and CompanyId='" + Companyid + "'", Id);
            }
            else if (tblName == "tblsectiondetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where SectionId = @id and CompanyId='" + Companyid + "'", Id);
            }
            else if (tblName == "tblfeestructuredetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where FeeStructureId = @id and CompanyId='" + Companyid + "'", Id);
                BaseDbServices.Instance.RunQuery("Delete from tblfeedetails where FeeStructureName = @name and CompanyId='" + Companyid + "'; " +
                "Delete from tblbatchclassfee  where FeeStructureName = @name and CompanyId='" + Companyid + "'", Name);
            }
            else if (tblName == "tblfeedetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where FeeId = @id and CompanyId='" + Companyid + "'", Id);
                BaseDbServices.Instance.RunQuery("Delete from tblbatchclassfee where FeeStructureName = @structure and FeeName = @name and CompanyId='" + Companyid + "'", FeeStructure);
            }
            else if (tblName == "tblcountrydetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from tblstatedetails where CountryName = @name and CompanyId='" + Companyid + "'", Name);
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where CountryId = @id and CompanyId='" + Companyid + "'", Id);
            }
            else if (tblName == "tblscholarshipname")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where ScholarshipId = @id and CompanyId='"+ Companyid + "'", Id);
                BaseDbServices.Instance.RunQuery("Delete from tblscholarshipdetails where ScholarshipName=@name and CompanyId='"+ Companyid + "'", Name);

            }

            else if (tblName == "tblstatedetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where StateId = @id and CompanyId='" + Companyid + "'", Id);
            }
            else if (tblName == "tblclassfeedetails")
            {
                BaseDbServices.Instance.RunQuery("Delete from tblbatchclassfee where BatchClassId=@id;" +
                "Delete from tblbatchclass where BatchClassId=@id", Id);

            }
            else if (tblName == "tblspecialscholarship")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where StudentId=@id and Batch=@batch and CompanyId='" +Companyid+"'", Batch);
            }
            else if (tblName == "tblstudenttype")
            {
                BaseDbServices.Instance.RunQuery("Delete from " + tblName + " where StudentTypeId=@id and CompanyId='" + Companyid + "'", Id);
            }
            return Ok("");
        }


        [Route("PostBillingDetails"), HttpPost]
        public IHttpActionResult PostBillingDetails([FromBody] Billing obj)
        {

            var studentinfo = BillingDbServices.Instance.GetStudentClass(obj.StudentId.ToString(),Convert.ToInt32(obj.CompanyId));
            if (studentinfo.Rows.Count == 0)
            {
                return Ok(false);
            }
            else if(studentinfo.Rows[0]["Status"].ToString() !="Active")
            {
                return Ok(false);
            }
            else
            {
                var studentType = studentinfo.Rows[0]["StudentType"];
                foreach(var w in obj.Month)
                { 
                List<MySqlParameter> Info = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@batch",studentinfo.Rows[0]["Batch"] ),
                        new MySqlParameter("@class",studentinfo.Rows[0]["Class"]),
                        new MySqlParameter("@faculty",studentinfo.Rows[0]["Faculty"]),
                        new MySqlParameter("@id",obj.StudentId),
                        new MySqlParameter("@month",w),
                        new MySqlParameter("@Cbatch",obj.Batch),
                        new MySqlParameter("@CompanyId",obj.CompanyId)
                    };

                var FeeDetails = new DataTable();
                int amount;
                int discount;
                string StudentId;
                int total = 0;
                string FeeStructureName;
                string FeeName;
                var batchclassid = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@Cbatch and Faculty=@faculty and Class=@class and CompanyId=@CompanyId", Info);
                var ids = batchclassid.Rows[0]["BatchClassId"];
                var CheckSpecialScholarship = BaseDbServices.Instance.GetData("Select SpecialScholarshipId from tblspecialscholarship where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                List<Tuple<int, string, string>> lstBilling = new List<Tuple<int, string, string>>();
                var YearlyFeeName = BillingDbServices.Instance.GetYearlyFeeById(obj.Batch, obj.StudentId,obj.CompanyId);
                var MonthFeeName = BillingDbServices.Instance.GetMonthlyFeeById(obj.Batch, w, obj.StudentId,obj.CompanyId);
                List<string> billingType = new List<string>();
                var bb = YearlyFeeName.AsEnumerable().Union(MonthFeeName.AsEnumerable());
                foreach (var item in bb)
                {
                    billingType.Add(item[0].ToString());
                }
                if (CheckSpecialScholarship.Rows.Count != 0)
                {
                    var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId=@CompanyId", Info);
                    for (int i = 0; i < FeeStructure.Rows.Count; i++)
                    {
                        for (int q = 0; q < obj.BillingFees.Count; q++)
                        {
                            if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                            {
                                if (studentType.ToString() == "General")
                                {
                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId=@CompanyId", Info);
                                }
                                else
                                {
                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId=@CompanyId", Info);
                                }
                                var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Studentid=@Id and Batch=@Cbatch and CompanyId=@CompanyId", Info);

                                for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                {
                                    var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                    for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                    {

                                        if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                        {
                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                            var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                            totalAmount = (int)Math.Round((double)amount - discountAmount);
                                            break;

                                        }
                                        else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                        {
                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                            totalAmount = (int)Math.Round((double)amount - discount);
                                            break;

                                        }

                                    }
                                    total = totalAmount;

                                    FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                    FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                    lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                }

                            }

                        }
                    }
                    var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                    if (transport.Rows.Count > 0)
                    {
                        var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch,w, obj.StudentId,obj.CompanyId);
                        if (checktransport.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                            lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                        }
                    }

                    var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                    if (studentextrafeeid.Rows.Count > 0)
                    {
                        var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId=@CompanyId", Info);
                        var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, w, obj.StudentId,obj.CompanyId);
                        if (checkextrafee.Rows.Count > 0)
                        {
                            List<string> checkType = new List<string>();
                            var ck = checkextrafee.AsEnumerable();
                            foreach (var item in ck)
                            {
                                checkType.Add(item[0].ToString());
                            }
                            for (int i = 0; i < extrafee.Rows.Count; i++)
                            {
                                if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                {

                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < extrafee.Rows.Count; i++)
                            {
                                lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                            }
                        }
                    }
                    StudentId = obj.StudentId.ToString();
                    string Id = DateTime.Now.ToString("yyyy");
                    
                    BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, w, obj.Batch, lstBilling,obj.CompanyId);

                }
                else
                {
                    var scholarshipname = studentinfo.Rows[0]["ScholarshipName"].ToString();
                    if (scholarshipname != "")
                    {
                        var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId=@CompanyId", Info);

                        for (int i = 0; i < FeeStructure.Rows.Count; i++)
                        {
                            for (int q = 0; q < obj.BillingFees.Count; q++)
                            {
                                if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                {
                                    if (studentType.ToString() == "General")
                                    {
                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId=@CompanyId", Info);
                                    }
                                    else
                                    {
                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId=@CompanyId", Info);
                                    }

                                    var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and ScholarshipName='" + scholarshipname + "' and Batch=@Cbatch and Class=@class and Faculty=@faculty and CompanyId=@CompanyId", Info);
                                    for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                    {
                                        var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                        for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                        {

                                            if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                            {
                                                amount = (int)SelectedFee.Rows[j]["Amount"];
                                                discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                                totalAmount = (int)Math.Round((double)amount - discountAmount);

                                                break;

                                            }
                                            else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                            {
                                                amount = (int)SelectedFee.Rows[j]["Amount"];
                                                discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                totalAmount = (int)Math.Round((double)amount - discount);
                                                break;

                                            }

                                        }
                                        total = totalAmount;

                                        FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                        FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                        lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                    }

                                }
                            }

                        }
                        var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                        if (transport.Rows.Count > 0)
                        {
                            var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, w, obj.StudentId,obj.CompanyId);
                            if (checktransport.Rows.Count > 0)
                            {

                            }
                            else
                            {
                                var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                            }
                        }

                        var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                        if (studentextrafeeid.Rows.Count > 0)
                        {
                            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId=@CompanyId", Info);
                            var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, w, obj.StudentId,obj.CompanyId);
                            if (checkextrafee.Rows.Count > 0)
                            {
                                List<string> checkType = new List<string>();
                                var ck = checkextrafee.AsEnumerable();
                                foreach (var item in ck)
                                {
                                    checkType.Add(item[0].ToString());
                                }
                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                {
                                    if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                    {

                                        lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                {
                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                }
                            }
                        }
                        StudentId = obj.StudentId.ToString();
                        string Id = DateTime.Now.ToString("yyyy");
                        BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, w, obj.Batch, lstBilling,obj.CompanyId);
                    }
                    else
                    {
                        var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails and CompanyId=@CompanyId", Info);

                        for (int i = 0; i < FeeStructure.Rows.Count; i++)
                        {
                            for (int q = 0; q < obj.BillingFees.Count; q++)
                            {
                                if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                {
                                    if (studentType.ToString() == "General")
                                    {
                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId=@CompanyId", Info);
                                    }
                                    else
                                    {
                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId=@CompanyId", Info);
                                    }
                                    for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                    {
                                        var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                        total = totalAmount;

                                        FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                        FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                        lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));

                                    }

                                }
                            }

                        }
                        var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                        if (transport.Rows.Count > 0)
                        {
                            var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch,w, obj.StudentId,obj.CompanyId);
                            if (checktransport.Rows.Count > 0)
                            {

                            }
                            else
                            {
                                var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                            }
                        }

                        var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                        if (studentextrafeeid.Rows.Count > 0)
                        {
                            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId=@CompanyId", Info);
                            var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, w, obj.StudentId,obj.CompanyId);
                            if (checkextrafee.Rows.Count > 0)
                            {
                                List<string> checkType = new List<string>();
                                var ck = checkextrafee.AsEnumerable();
                                foreach (var item in ck)
                                {
                                    checkType.Add(item[0].ToString());
                                }
                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                {
                                    if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                    {

                                        lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                {
                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                }
                            }
                        }
                        StudentId = obj.StudentId.ToString();
                        string Id = DateTime.Now.ToString("yyyy");
                        BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId,w, obj.Batch, lstBilling,obj.CompanyId);
                    }
                }
            }
            }
            return Ok(true);
        }
        [Route("PostClassBillingDetails"), HttpPost]
        public IHttpActionResult PostClassBillingDetails([FromBody] Billing obj)
        {
            var studentList = new DataTable();
            if(obj.Class == null && obj.Section == null)
            {
                var classlist = BaseDbServices.Instance.GetData("select ClassName from tblclassdetails where CompanyId='"+obj.CompanyId+"'", null);
                for(int p =0;p <classlist.Rows.Count;p++)
                {
                   
                    studentList = BillingDbServices.Instance.GetAllStudentId(obj.Batch,classlist.Rows[p]["ClassName"].ToString(),obj.CompanyId);

                    for (int m = 0; m < studentList.Rows.Count; m++)
                    {
                        var studentinfo = BillingDbServices.Instance.GetStudentClassInfo(studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                        foreach (var s in obj.Month)
                        {
                            var studentType = studentinfo.Rows[0]["StudentType"];
                            List<MySqlParameter> Info = new List<MySqlParameter>()
                                {
                                    new MySqlParameter("@batch",studentinfo.Rows[0]["Batch"] ),
                                    new MySqlParameter("@class",studentinfo.Rows[0]["Class"]),
                                    new MySqlParameter("@faculty",studentinfo.Rows[0]["Faculty"]),
                                    new MySqlParameter("@id",studentList.Rows[m]["StudentId"].ToString()),
                                    new MySqlParameter("@Cbatch",obj.Batch),
                                    new MySqlParameter("@month",s),
                                    new MySqlParameter("@CompanyId", obj.CompanyId)
                                };
                            var FeeDetails = new DataTable();
                            int amount;
                            int discount;
                            string StudentId;
                            int total = 0;
                            string FeeStructureName;
                            string FeeName;
                            var batchclassid = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@Cbatch and Faculty=@faculty and Class=@class and CompanyId=@CompanyId", Info);
                            var ids = batchclassid.Rows[0]["BatchClassId"];
                            var CheckSpecialScholarship = BaseDbServices.Instance.GetData("Select SpecialScholarshipId from tblspecialscholarship where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                            List<Tuple<int, string, string>> lstBilling = new List<Tuple<int, string, string>>();
                            var YearlyFeeName = BillingDbServices.Instance.GetYearlyFeeById(obj.Batch, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                            var MonthFeeName = BillingDbServices.Instance.GetMonthlyFeeById(obj.Batch,s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                            List<string> billingType = new List<string>();
                            var bb = YearlyFeeName.AsEnumerable().Union(MonthFeeName.AsEnumerable());
                            foreach (var item in bb)
                            {
                                billingType.Add(item[0].ToString());
                            }

                            if (CheckSpecialScholarship.Rows.Count != 0)
                            {
                                var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);
                                for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                {
                                    for (int q = 0; q < obj.BillingFees.Count; q++)
                                    {
                                        if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                        {
                                            if (studentType.ToString() == "General")
                                            {
                                                FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                            }
                                            else
                                            {
                                                FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='"+obj.CompanyId+"'", null);
                                            }
                                            var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Studentid=@Id and Batch=@Cbatch and CompanyId=@CompanyId", Info);

                                            for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                            {
                                                var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                                {

                                                    if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                                    {
                                                        amount = (int)SelectedFee.Rows[j]["Amount"];
                                                        discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                        var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                                        totalAmount = (int)Math.Round((double)amount - discountAmount);
                                                        break;

                                                    }
                                                    else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                                    {
                                                        amount = (int)SelectedFee.Rows[j]["Amount"];
                                                        discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                        totalAmount = (int)Math.Round((double)amount - discount);
                                                        break;

                                                    }

                                                }
                                                total = totalAmount;

                                                FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                            }

                                        }

                                    }
                                }
                                var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                if (transport.Rows.Count > 0)
                                {
                                    var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch,s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                    if (checktransport.Rows.Count > 0)
                                    {

                                    }
                                    else
                                    {
                                        var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                        lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                    }
                                }

                                var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                if (studentextrafeeid.Rows.Count > 0)
                                {
                                    var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                    var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                    if (checkextrafee.Rows.Count > 0)
                                    {
                                        List<string> checkType = new List<string>();
                                        var ck = checkextrafee.AsEnumerable();
                                        foreach (var item in ck)
                                        {
                                            checkType.Add(item[0].ToString());
                                        }
                                        for (int i = 0; i < extrafee.Rows.Count; i++)
                                        {
                                            if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                            {

                                                lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < extrafee.Rows.Count; i++)
                                        {
                                            lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                        }
                                    }
                                }
                                StudentId = studentList.Rows[m]["StudentId"].ToString();
                                string Id = DateTime.Now.ToString("yyyy");
                                BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);


                            }
                            else
                            {
                                var scholarshipname = studentinfo.Rows[0]["ScholarshipName"].ToString();
                                if (scholarshipname != "")
                                {
                                    var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);

                                    for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                    {
                                        for (int q = 0; q < obj.BillingFees.Count; q++)
                                        {
                                            if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                            {
                                                if (studentType.ToString() == "General")
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                }
                                                else
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='"+obj.CompanyId+"'", null);
                                                }
                                                var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and ScholarshipName='" + scholarshipname + "' and Batch=@Cbatch and Class=@class and Faculty=@faculty and CompanyId=@CompanyId", Info);
                                                for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                {
                                                    var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                    for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                                    {

                                                        if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                                        {
                                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                            var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                                            totalAmount = (int)Math.Round((double)amount - discountAmount);

                                                            break;

                                                        }
                                                        else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                                        {
                                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                            totalAmount = (int)Math.Round((double)amount - discount);
                                                            break;

                                                        }

                                                    }
                                                    total = totalAmount;

                                                    FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                    FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                    lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                                }

                                            }
                                        }

                                    }
                                    var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                    if (transport.Rows.Count > 0)
                                    {
                                        var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checktransport.Rows.Count > 0)
                                        {

                                        }
                                        else
                                        {
                                            var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                            lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                        }
                                    }

                                    var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                    if (studentextrafeeid.Rows.Count > 0)
                                    {
                                        var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                        var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checkextrafee.Rows.Count > 0)
                                        {
                                            List<string> checkType = new List<string>();
                                            var ck = checkextrafee.AsEnumerable();
                                            foreach (var item in ck)
                                            {
                                                checkType.Add(item[0].ToString());
                                            }
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                {

                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                            }
                                        }
                                    }
                                    StudentId = studentList.Rows[m]["StudentId"].ToString();
                                    string Id = DateTime.Now.ToString("yyyy");
                                    BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);
                                }
                                else
                                {
                                    var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails and CompanyId='"+obj.CompanyId+"'", null);

                                    for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                    {
                                        for (int q = 0; q < obj.BillingFees.Count; q++)
                                        {
                                            if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                            {
                                                if (studentType.ToString() == "General")
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                }
                                                else
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='" + obj.CompanyId + "'", null);
                                                }

                                                for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                {
                                                    var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                    total = totalAmount;

                                                    FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                    FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                    lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));

                                                }

                                            }
                                        }

                                    }
                                    var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                    if (transport.Rows.Count > 0)
                                    {
                                        var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checktransport.Rows.Count > 0)
                                        {

                                        }
                                        else
                                        {
                                            var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                            lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                        }
                                    }

                                    var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                    if (studentextrafeeid.Rows.Count > 0)
                                    {
                                        var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                        var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checkextrafee.Rows.Count > 0)
                                        {
                                            List<string> checkType = new List<string>();
                                            var ck = checkextrafee.AsEnumerable();
                                            foreach (var item in ck)
                                            {
                                                checkType.Add(item[0].ToString());
                                            }
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                {

                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                            }
                                        }
                                    }
                                    StudentId = studentList.Rows[m]["StudentId"].ToString();
                                    BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);
                                }
                            }
                        }
                    }
                }
            }
            else if(obj.Class == null && obj.Section != null)
            {
                var classlist = BaseDbServices.Instance.GetData("select ClassName from tblclassdetails and CompanyId='"+obj.CompanyId+"'", null);
                for (int p = 0; p < classlist.Rows.Count; p++)
                {
                    foreach (var e in obj.Section)
                    {
                        studentList = BillingDbServices.Instance.GetStudentList(obj.Batch, classlist.Rows[p]["ClassName"].ToString(),e,obj.CompanyId);

                        for (int m = 0; m < studentList.Rows.Count; m++)
                        {
                            var studentinfo = BillingDbServices.Instance.GetStudentClassInfo(studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                            foreach (var s in obj.Month)
                            {
                                var studentType = studentinfo.Rows[0]["StudentType"];
                                List<MySqlParameter> Info = new List<MySqlParameter>()
                                {
                                    new MySqlParameter("@batch",studentinfo.Rows[0]["Batch"] ),
                                    new MySqlParameter("@class",studentinfo.Rows[0]["Class"]),
                                    new MySqlParameter("@faculty",studentinfo.Rows[0]["Faculty"]),
                                    new MySqlParameter("@id",studentList.Rows[m]["StudentId"].ToString()),
                                    new MySqlParameter("@Cbatch",obj.Batch),
                                    new MySqlParameter("@month",s),
                                    new MySqlParameter("@CompanyId",obj.CompanyId)
                                };
                                var FeeDetails = new DataTable();
                                int amount;
                                int discount;
                                string StudentId;
                                int total = 0;
                                string FeeStructureName;
                                string FeeName;
                                var batchclassid = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@Cbatch and Faculty=@faculty and Class=@class and CompanyId=@CompanyId", Info);
                                var ids = batchclassid.Rows[0]["BatchClassId"];
                                var CheckSpecialScholarship = BaseDbServices.Instance.GetData("Select SpecialScholarshipId from tblspecialscholarship where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                List<Tuple<int, string, string>> lstBilling = new List<Tuple<int, string, string>>();
                                var YearlyFeeName = BillingDbServices.Instance.GetYearlyFeeById(obj.Batch, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                var MonthFeeName = BillingDbServices.Instance.GetMonthlyFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                List<string> billingType = new List<string>();
                                var bb = YearlyFeeName.AsEnumerable().Union(MonthFeeName.AsEnumerable());
                                foreach (var item in bb)
                                {
                                    billingType.Add(item[0].ToString());
                                }

                                if (CheckSpecialScholarship.Rows.Count != 0)
                                {
                                    var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId=@CompanyId", Info);
                                    for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                    {
                                        for (int q = 0; q < obj.BillingFees.Count; q++)
                                        {
                                            if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                            {
                                                if (studentType.ToString() == "General")
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                }
                                                else
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='" + obj.CompanyId + "'", null);
                                                }
                                                var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Studentid=@Id and Batch=@Cbatch and CompanyId=@CompanyId", Info);

                                                for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                {
                                                    var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                    for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                                    {

                                                        if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                                        {
                                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                            var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                                            totalAmount = (int)Math.Round((double)amount - discountAmount);
                                                            break;

                                                        }
                                                        else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                                        {
                                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                            totalAmount = (int)Math.Round((double)amount - discount);
                                                            break;

                                                        }

                                                    }
                                                    total = totalAmount;

                                                    FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                    FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                    lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                                }

                                            }

                                        }
                                    }
                                    var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                    if (transport.Rows.Count > 0)
                                    {
                                        var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checktransport.Rows.Count > 0)
                                        {

                                        }
                                        else
                                        {
                                            var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                            lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                        }
                                    }

                                    var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                    if (studentextrafeeid.Rows.Count > 0)
                                    {
                                        var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                        var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checkextrafee.Rows.Count > 0)
                                        {
                                            List<string> checkType = new List<string>();
                                            var ck = checkextrafee.AsEnumerable();
                                            foreach (var item in ck)
                                            {
                                                checkType.Add(item[0].ToString());
                                            }
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                {

                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                            }
                                        }
                                    }
                                    StudentId = studentList.Rows[m]["StudentId"].ToString();
                                    string Id = DateTime.Now.ToString("yyyy");
                                    BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);


                                }
                                else
                                {
                                    var scholarshipname = studentinfo.Rows[0]["ScholarshipName"].ToString();
                                    if (scholarshipname != "")
                                    {
                                        var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);

                                        for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                        {
                                            for (int q = 0; q < obj.BillingFees.Count; q++)
                                            {
                                                if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                                {
                                                    if (studentType.ToString() == "General")
                                                    {
                                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                    }
                                                    else
                                                    {
                                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='" + obj.CompanyId + "'", null);
                                                    }
                                                    var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and ScholarshipName='" + scholarshipname + "' and Batch=@Cbatch and Class=@class and Faculty=@faculty and CompanyId=@CompanyId", Info);
                                                    for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                    {
                                                        var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                        for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                                        {

                                                            if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                                            {
                                                                amount = (int)SelectedFee.Rows[j]["Amount"];
                                                                discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                                var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                                                totalAmount = (int)Math.Round((double)amount - discountAmount);

                                                                break;

                                                            }
                                                            else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                                            {
                                                                amount = (int)SelectedFee.Rows[j]["Amount"];
                                                                discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                                totalAmount = (int)Math.Round((double)amount - discount);
                                                                break;

                                                            }

                                                        }
                                                        total = totalAmount;

                                                        FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                        FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                        lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                                    }

                                                }
                                            }

                                        }
                                        var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                        if (transport.Rows.Count > 0)
                                        {
                                            var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                            if (checktransport.Rows.Count > 0)
                                            {

                                            }
                                            else
                                            {
                                                var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                                lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                            }
                                        }

                                        var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                        if (studentextrafeeid.Rows.Count > 0)
                                        {
                                            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                            var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                            if (checkextrafee.Rows.Count > 0)
                                            {
                                                List<string> checkType = new List<string>();
                                                var ck = checkextrafee.AsEnumerable();
                                                foreach (var item in ck)
                                                {
                                                    checkType.Add(item[0].ToString());
                                                }
                                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                                {
                                                    if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                    {

                                                        lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                                {
                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        StudentId = studentList.Rows[m]["StudentId"].ToString();
                                        string Id = DateTime.Now.ToString("yyyy");
                                        BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);
                                    }
                                    else
                                    {
                                        var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);

                                        for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                        {
                                            for (int q = 0; q < obj.BillingFees.Count; q++)
                                            {
                                                if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                                {
                                                    if (studentType.ToString() == "General")
                                                    {
                                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                    }
                                                    else
                                                    {
                                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='"+obj.CompanyId+"'", null);
                                                    }

                                                    for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                    {
                                                        var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                        total = totalAmount;

                                                        FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                        FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                        lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));

                                                    }

                                                }
                                            }

                                        }
                                        var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                        if (transport.Rows.Count > 0)
                                        {
                                            var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                            if (checktransport.Rows.Count > 0)
                                            {

                                            }
                                            else
                                            {
                                                var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                                lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                            }
                                        }

                                        var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                        if (studentextrafeeid.Rows.Count > 0)
                                        {
                                            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                            var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                            if (checkextrafee.Rows.Count > 0)
                                            {
                                                List<string> checkType = new List<string>();
                                                var ck = checkextrafee.AsEnumerable();
                                                foreach (var item in ck)
                                                {
                                                    checkType.Add(item[0].ToString());
                                                }
                                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                                {
                                                    if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                    {

                                                        lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                                {
                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        StudentId = studentList.Rows[m]["StudentId"].ToString();
                                        BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);
                                    }
                                }
                            }
                        }
                    }
                    }
                }
            else if(obj.Class !=null && obj.Section == null)
            {
                foreach (var e in obj.Class)
                {
                    studentList = BillingDbServices.Instance.GetAllStudentId(obj.Batch,e,obj.CompanyId);

                    for (int m = 0; m < studentList.Rows.Count; m++)
                    {
                        var studentinfo = BillingDbServices.Instance.GetStudentClassInfo(studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                        foreach (var s in obj.Month)
                        {
                            var studentType = studentinfo.Rows[0]["StudentType"];
                            List<MySqlParameter> Info = new List<MySqlParameter>()
                                {
                                    new MySqlParameter("@batch",studentinfo.Rows[0]["Batch"] ),
                                    new MySqlParameter("@class",studentinfo.Rows[0]["Class"]),
                                    new MySqlParameter("@faculty",studentinfo.Rows[0]["Faculty"]),
                                    new MySqlParameter("@id",studentList.Rows[m]["StudentId"].ToString()),
                                    new MySqlParameter("@Cbatch",obj.Batch),
                                    new MySqlParameter("@month",s),
                                    new MySqlParameter("@CompanyId",obj.CompanyId)
                                };
                            var FeeDetails = new DataTable();
                            int amount;
                            int discount;
                            string StudentId;
                            int total = 0;
                            string FeeStructureName;
                            string FeeName;
                            var batchclassid = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@Cbatch and Faculty=@faculty and Class=@class and CompanyId=@CompanyId", Info);
                            var ids = batchclassid.Rows[0]["BatchClassId"];
                            var CheckSpecialScholarship = BaseDbServices.Instance.GetData("Select SpecialScholarshipId from tblspecialscholarship where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                            List<Tuple<int, string, string>> lstBilling = new List<Tuple<int, string, string>>();
                            var YearlyFeeName = BillingDbServices.Instance.GetYearlyFeeById(obj.Batch, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                            var MonthFeeName = BillingDbServices.Instance.GetMonthlyFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                            List<string> billingType = new List<string>();
                            var bb = YearlyFeeName.AsEnumerable().Union(MonthFeeName.AsEnumerable());
                            foreach (var item in bb)
                            {
                                billingType.Add(item[0].ToString());
                            }

                            if (CheckSpecialScholarship.Rows.Count != 0)
                            {
                                var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);
                                for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                {
                                    for (int q = 0; q < obj.BillingFees.Count; q++)
                                    {
                                        if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                        {
                                            if (studentType.ToString() == "General")
                                            {
                                                FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                            }
                                            else
                                            {
                                                FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='" + obj.CompanyId + "'", null);
                                            }
                                            var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Studentid=@Id and Batch=@Cbatch and CompanyId=@CompanyId", Info);

                                            for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                            {
                                                var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                                {

                                                    if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                                    {
                                                        amount = (int)SelectedFee.Rows[j]["Amount"];
                                                        discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                        var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                                        totalAmount = (int)Math.Round((double)amount - discountAmount);
                                                        break;

                                                    }
                                                    else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                                    {
                                                        amount = (int)SelectedFee.Rows[j]["Amount"];
                                                        discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                        totalAmount = (int)Math.Round((double)amount - discount);
                                                        break;

                                                    }

                                                }
                                                total = totalAmount;

                                                FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                            }

                                        }

                                    }
                                }
                                var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                if (transport.Rows.Count > 0)
                                {
                                    var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                    if (checktransport.Rows.Count > 0)
                                    {

                                    }
                                    else
                                    {
                                        var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                        lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                    }
                                }

                                var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                if (studentextrafeeid.Rows.Count > 0)
                                {
                                    var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                    var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                    if (checkextrafee.Rows.Count > 0)
                                    {
                                        List<string> checkType = new List<string>();
                                        var ck = checkextrafee.AsEnumerable();
                                        foreach (var item in ck)
                                        {
                                            checkType.Add(item[0].ToString());
                                        }
                                        for (int i = 0; i < extrafee.Rows.Count; i++)
                                        {
                                            if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                            {

                                                lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < extrafee.Rows.Count; i++)
                                        {
                                            lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                        }
                                    }
                                }
                                StudentId = studentList.Rows[m]["StudentId"].ToString();
                                string Id = DateTime.Now.ToString("yyyy");
                                BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);


                            }
                            else
                            {
                                var scholarshipname = studentinfo.Rows[0]["ScholarshipName"].ToString();
                                if (scholarshipname != "")
                                {
                                    var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);

                                    for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                    {
                                        for (int q = 0; q < obj.BillingFees.Count; q++)
                                        {
                                            if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                            {
                                                if (studentType.ToString() == "General")
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                }
                                                else
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='" + obj.CompanyId + "'", null);
                                                }
                                                var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and ScholarshipName='" + scholarshipname + "' and Batch=@Cbatch and Class=@class and Faculty=@faculty and CompanyId=@CompanyId", Info);
                                                for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                {
                                                    var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                    for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                                    {

                                                        if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                                        {
                                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                            var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                                            totalAmount = (int)Math.Round((double)amount - discountAmount);

                                                            break;

                                                        }
                                                        else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                                        {
                                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                            totalAmount = (int)Math.Round((double)amount - discount);
                                                            break;

                                                        }

                                                    }
                                                    total = totalAmount;

                                                    FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                    FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                    lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                                }

                                            }
                                        }

                                    }
                                    var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                    if (transport.Rows.Count > 0)
                                    {
                                        var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checktransport.Rows.Count > 0)
                                        {

                                        }
                                        else
                                        {
                                            var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                            lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                        }
                                    }

                                    var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                    if (studentextrafeeid.Rows.Count > 0)
                                    {
                                        var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                        var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checkextrafee.Rows.Count > 0)
                                        {
                                            List<string> checkType = new List<string>();
                                            var ck = checkextrafee.AsEnumerable();
                                            foreach (var item in ck)
                                            {
                                                checkType.Add(item[0].ToString());
                                            }
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                {

                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                            }
                                        }
                                    }
                                    StudentId = studentList.Rows[m]["StudentId"].ToString();
                                    string Id = DateTime.Now.ToString("yyyy");
                                    BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);
                                }
                                else
                                {
                                    var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);

                                    for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                    {
                                        for (int q = 0; q < obj.BillingFees.Count; q++)
                                        {
                                            if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                            {
                                                if (studentType.ToString() == "General")
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                }
                                                else
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='" + obj.CompanyId + "'", null);
                                                }

                                                for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                {
                                                    var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                    total = totalAmount;

                                                    FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                    FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                    lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));

                                                }

                                            }
                                        }

                                    }
                                    var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                    if (transport.Rows.Count > 0)
                                    {
                                        var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checktransport.Rows.Count > 0)
                                        {

                                        }
                                        else
                                        {
                                            var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                            lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                        }
                                    }

                                    var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                    if (studentextrafeeid.Rows.Count > 0)
                                    {
                                        var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                        var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checkextrafee.Rows.Count > 0)
                                        {
                                            List<string> checkType = new List<string>();
                                            var ck = checkextrafee.AsEnumerable();
                                            foreach (var item in ck)
                                            {
                                                checkType.Add(item[0].ToString());
                                            }
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                {

                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                            }
                                        }
                                    }
                                    StudentId = studentList.Rows[m]["StudentId"].ToString();
                                    BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);
                                }
                            }
                        }
                    }
                }
            }
            else if(obj.Class != null && obj.Section != null)
            {
               
                foreach (var w in obj.Class)
                {
                    foreach (var e in obj.Section)
                    {
                        studentList = BillingDbServices.Instance.GetStudentList(obj.Batch,w, e,obj.CompanyId);

                        for (int m = 0; m < studentList.Rows.Count; m++)
                        {
                            var studentinfo = BillingDbServices.Instance.GetStudentClassInfo(studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                            foreach (var s in obj.Month)
                            {
                                var studentType = studentinfo.Rows[0]["StudentType"];
                                List<MySqlParameter> Info = new List<MySqlParameter>()
                                {
                                    new MySqlParameter("@batch",studentinfo.Rows[0]["Batch"] ),
                                    new MySqlParameter("@class",studentinfo.Rows[0]["Class"]),
                                    new MySqlParameter("@faculty",studentinfo.Rows[0]["Faculty"]),
                                    new MySqlParameter("@id",studentList.Rows[m]["StudentId"].ToString()),
                                    new MySqlParameter("@Cbatch",obj.Batch),
                                    new MySqlParameter("@month",s),
                                    new MySqlParameter("@CompanyId",obj.CompanyId)
                                };
                                var FeeDetails = new DataTable();
                                int amount;
                                int discount;
                                string StudentId;
                                int total = 0;
                                string FeeStructureName;
                                string FeeName;
                                var batchclassid = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@Cbatch and Faculty=@faculty and Class=@class and CompanyId=@CompanyId", Info);
                                var ids = batchclassid.Rows[0]["BatchClassId"];
                                var CheckSpecialScholarship = BaseDbServices.Instance.GetData("Select SpecialScholarshipId from tblspecialscholarship where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                List<Tuple<int, string, string>> lstBilling = new List<Tuple<int, string, string>>();
                                var YearlyFeeName = BillingDbServices.Instance.GetYearlyFeeById(obj.Batch, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                var MonthFeeName = BillingDbServices.Instance.GetMonthlyFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                List<string> billingType = new List<string>();
                                var bb = YearlyFeeName.AsEnumerable().Union(MonthFeeName.AsEnumerable());
                                foreach (var item in bb)
                                {
                                    billingType.Add(item[0].ToString());
                                }

                                if (CheckSpecialScholarship.Rows.Count != 0)
                                {
                                    var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);
                                    for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                    {
                                        for (int q = 0; q < obj.BillingFees.Count; q++)
                                        {
                                            if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                            {
                                                if (studentType.ToString() == "General")
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                }
                                                else
                                                {
                                                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='" + obj.CompanyId + "'", null);
                                                }
                                                var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Studentid=@Id and Batch=@Cbatch and CompanyId=@CompanyId", Info);

                                                for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                {
                                                    var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                    for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                                    {

                                                        if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                                        {
                                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                            var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                                            totalAmount = (int)Math.Round((double)amount - discountAmount);
                                                            break;

                                                        }
                                                        else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                                        {
                                                            amount = (int)SelectedFee.Rows[j]["Amount"];
                                                            discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                            totalAmount = (int)Math.Round((double)amount - discount);
                                                            break;

                                                        }

                                                    }
                                                    total = totalAmount;

                                                    FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                    FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                    lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                                }

                                            }

                                        }
                                    }
                                    var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                    if (transport.Rows.Count > 0)
                                    {
                                        var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checktransport.Rows.Count > 0)
                                        {

                                        }
                                        else
                                        {
                                            var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                            lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                        }
                                    }

                                    var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                    if (studentextrafeeid.Rows.Count > 0)
                                    {
                                        var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                        var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                        if (checkextrafee.Rows.Count > 0)
                                        {
                                            List<string> checkType = new List<string>();
                                            var ck = checkextrafee.AsEnumerable();
                                            foreach (var item in ck)
                                            {
                                                checkType.Add(item[0].ToString());
                                            }
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                {

                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < extrafee.Rows.Count; i++)
                                            {
                                                lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                            }
                                        }
                                    }
                                    StudentId = studentList.Rows[m]["StudentId"].ToString();
                                    string Id = DateTime.Now.ToString("yyyy");
                                    BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);


                                }
                                else
                                {
                                    var scholarshipname = studentinfo.Rows[0]["ScholarshipName"].ToString();
                                    if (scholarshipname != "")
                                    {
                                        var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);

                                        for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                        {
                                            for (int q = 0; q < obj.BillingFees.Count; q++)
                                            {
                                                if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                                {
                                                    if (studentType.ToString() == "General")
                                                    {
                                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                    }
                                                    else
                                                    {
                                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='" + obj.CompanyId + "'", null);
                                                    }
                                                    var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and ScholarshipName='" + scholarshipname + "' and Batch=@Cbatch and Class=@class and Faculty=@faculty and CompanyId=@CompanyId", Info);
                                                    for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                    {
                                                        var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                        for (int j = 0; j < SelectedFee.Rows.Count; j++)
                                                        {

                                                            if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                                            {
                                                                amount = (int)SelectedFee.Rows[j]["Amount"];
                                                                discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                                var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                                                totalAmount = (int)Math.Round((double)amount - discountAmount);

                                                                break;

                                                            }
                                                            else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                                            {
                                                                amount = (int)SelectedFee.Rows[j]["Amount"];
                                                                discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                                                totalAmount = (int)Math.Round((double)amount - discount);
                                                                break;

                                                            }

                                                        }
                                                        total = totalAmount;

                                                        FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                        FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                        lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));


                                                    }

                                                }
                                            }

                                        }
                                        var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                        if (transport.Rows.Count > 0)
                                        {
                                            var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                            if (checktransport.Rows.Count > 0)
                                            {

                                            }
                                            else
                                            {
                                                var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                                lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                            }
                                        }

                                        var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                        if (studentextrafeeid.Rows.Count > 0)
                                        {
                                            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                            var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                            if (checkextrafee.Rows.Count > 0)
                                            {
                                                List<string> checkType = new List<string>();
                                                var ck = checkextrafee.AsEnumerable();
                                                foreach (var item in ck)
                                                {
                                                    checkType.Add(item[0].ToString());
                                                }
                                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                                {
                                                    if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                    {

                                                        lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                                {
                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        StudentId = studentList.Rows[m]["StudentId"].ToString();
                                        string Id = DateTime.Now.ToString("yyyy");
                                        BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);
                                    }
                                    else
                                    {
                                        var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId='"+obj.CompanyId+"'", null);

                                        for (int i = 0; i < FeeStructure.Rows.Count; i++)
                                        {
                                            for (int q = 0; q < obj.BillingFees.Count; q++)
                                            {
                                                if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == obj.BillingFees[q].FeeStructureName && obj.BillingFees[q].IsChecked == true && !billingType.Contains(obj.BillingFees[q].FeeStructureName))
                                                {
                                                    if (studentType.ToString() == "General")
                                                    {
                                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and StudentType='" + studentType + "' and CompanyId='"+obj.CompanyId+"'", null);
                                                    }
                                                    else
                                                    {
                                                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + batchclassid.Rows[0]["BatchClassId"] + "' and (StudentType='General' or StudentType='" + studentType + "') and CompanyId='" + obj.CompanyId + "'", null);
                                                    }

                                                    for (int k = 0; k < FeeDetails.Rows.Count; k++)
                                                    {
                                                        var totalAmount = Convert.ToInt32(FeeDetails.Rows[k]["Amount"]);
                                                        total = totalAmount;

                                                        FeeStructureName = FeeDetails.Rows[k]["FeeStructureName"].ToString();
                                                        FeeName = FeeDetails.Rows[k]["FeeName"].ToString();

                                                        lstBilling.Add(new Tuple<int, string, string>(total, FeeStructureName, FeeName));

                                                    }

                                                }
                                            }

                                        }
                                        var transport = BaseDbServices.Instance.GetData("select Amount from tblstudenttransport where StudentId=@id and Batch=@Cbatch and CompanyId=@CompanyId", Info);
                                        if (transport.Rows.Count > 0)
                                        {
                                            var checktransport = BillingDbServices.Instance.GetAllTransportDetails(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                            if (checktransport.Rows.Count > 0)
                                            {

                                            }
                                            else
                                            {
                                                var Amount = Convert.ToInt32(transport.Rows[0]["Amount"]);
                                                lstBilling.Add(new Tuple<int, string, string>(Amount, "Transportation", "Transport Fee"));
                                            }
                                        }

                                        var studentextrafeeid = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where Batch=@Cbatch and Month=@month and StudentId=@id and CompanyId=@CompanyId", Info);
                                        if (studentextrafeeid.Rows.Count > 0)
                                        {
                                            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId='" + studentextrafeeid.Rows[0]["StudentExtraFeeId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                                            var checkextrafee = BillingDbServices.Instance.GetExtraFeeById(obj.Batch, s, studentList.Rows[m]["StudentId"].ToString(),obj.CompanyId);
                                            if (checkextrafee.Rows.Count > 0)
                                            {
                                                List<string> checkType = new List<string>();
                                                var ck = checkextrafee.AsEnumerable();
                                                foreach (var item in ck)
                                                {
                                                    checkType.Add(item[0].ToString());
                                                }
                                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                                {
                                                    if (!checkType.Contains(extrafee.Rows[i]["FeeName"]))
                                                    {

                                                        lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < extrafee.Rows.Count; i++)
                                                {
                                                    lstBilling.Add(new Tuple<int, string, string>(Convert.ToInt32(extrafee.Rows[i]["Amount"]), "Extra", extrafee.Rows[i]["FeeName"].ToString()));
                                                }
                                            }
                                        }
                                        StudentId = studentList.Rows[m]["StudentId"].ToString();
                                        BillingDbServices.Instance.SaveBillingDetails(obj.CreatedBy,StudentId, s, obj.Batch, lstBilling,obj.CompanyId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        
            return Ok("");
        }

        [Route("PostStudentAttendance"),HttpPost]
        public IHttpActionResult PostStudentAttendance([FromBody] StudentAttendance obj)
        {
            List<MySqlParameter> details = new List<MySqlParameter>()
            {
                new MySqlParameter("@batch",obj.Batch),
                new MySqlParameter("@date",obj.Date),
                new MySqlParameter("@CompanyId",obj.CompanyId)
            };
            var list = StudentDBServices.Instance.CheckStudentAttendance(obj.Batch, obj.Date,obj.CompanyId);
            List<string> Info = new List<string>();
            var bb = list.AsEnumerable();
            foreach (var item in bb)
            {
                Info.Add(item[2].ToString());
            }
            foreach (var m in obj.Attendance)
            {
                if(Info.Contains(m.StudentId))
                {
                    BaseDbServices.Instance.RunQuery("Delete from tblstudentattendance where Batch=@batch and Date=@date and CompanyId=@CompanyId and StudentId= '" + m.StudentId + "'", details);
                }
            }
            StudentDBServices.Instance.SaveStudentAttendance(obj);
            return Ok("");
        }

        [Route("PostStaffAttendance"), HttpPost]
        public IHttpActionResult PostStaffAttendance([FromBody] StaffAttendance obj)
        {
            List<MySqlParameter> details = new List<MySqlParameter>()
            {
                new MySqlParameter("@batch",obj.Batch),
                new MySqlParameter("@date",obj.Date)
            };
            var list = TeacherDbServices.Instance.CheckStaffAttendance(obj.Batch, obj.Date,obj.CompanyId);
            List<string> Info = new List<string>();
            var bb = list.AsEnumerable();
            foreach (var item in bb)
            {
                Info.Add(item[2].ToString());
            }
            foreach (var m in obj.Attendance)
            {
                if (Info.Contains(m.TeacherId))
                {
                    BaseDbServices.Instance.RunQuery("Delete from tblstaffattendance where Batch=@batch' and Date=@date and TeacherId= '" + m.TeacherId + "'", details);
                }
            }
           TeacherDbServices.Instance.SaveStaffAttendance(obj);
            return Ok("");
        }

        [Route("PostTeacherDetails"),HttpPost]
        public IHttpActionResult PostTeacherDetails([FromBody] TeacherPersonalInfo obj)
        {
            if(obj.TeacherId !=0)
            {
                List<MySqlParameter> Id = new List<MySqlParameter>()
                {
                    new MySqlParameter("@id",obj.TeacherId),
                    new MySqlParameter("@CompanyId",obj.CompanyId)
                };
                BaseDbServices.Instance.RunQuery("Delete from tbleducation where TeacherId=@id and CompanyId=@CompanyId", Id);
                BaseDbServices.Instance.RunQuery("Delete from tblexperience where TeacherId=@id and CompanyId=@CompanyId", Id);
            }
             int id = TeacherDbServices.Instance.SaveTeacherDetails(obj);
            return Ok(id);
        }

        [Route("PostStudentDetails"), HttpPost]
        public IHttpActionResult PostStudentDetails([FromBody] StudentPersonalInfo obj)
        {
           
            if (obj.StudentId !=null)
            {
                List<MySqlParameter> Id = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@id",obj.StudentId)
                    };
                BaseDbServices.Instance.RunQuery("Delete from tblpasteducation where StudentId = @id and CompanyId='"+obj.CompanyId+"'", Id);
            }
            
            string id = StudentDBServices.Instance.SaveStudentdetails(obj);
            return Ok(id);
        }


        [Route("PostCompanyDetails"), HttpPost]
        public IHttpActionResult PostCompanyDetails([FromBody] CompanyDetails obj)
        {
            if (obj.CompanyId != 0)
            {
                CompanyDbServices.Instance.SaveCompanyDetails(obj);
            }
            else
            {
                //byte[] byts = System.Text.Encoding.UTF8.GetBytes(obj.Users.Password);
                //obj.Users.Password = Convert.ToBase64String(byts);
                CompanyDbServices.Instance.SaveCompanyDetails(obj);

            }
           return Ok("");
        }
        [Route("UploadDocuments"), HttpPost]
        public bool UploadDocuments()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var prefix = HttpContext.Current.Request.Form["Prefix"];
                var ID = HttpContext.Current.Request.Form["StudentId"];
                var CompanyId = HttpContext.Current.Request.Form["CompanyId"];

                try
                {
                    DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/UploadedFiles/student/documents/"));
                    if (!di.Exists) di.Create();
                    
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        //var ext = Path.GetExtension(file.FileName);
                        //var filename = $"{prefix}_{ID}_{i}{ext}";
                        var filename = $"{prefix}_{ID}_{CompanyId}_{file.FileName}";
                        var filePath = Path.Combine(di.FullName, filename);
                       
                        file.SaveAs(filePath);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return true;
        }

        [Route("UploadTeacherDocuments"), HttpPost]
        public bool UploadTeacherDocuments()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var prefix = HttpContext.Current.Request.Form["Prefix"];
                var ID = HttpContext.Current.Request.Form["TeacherId"];
                var CompanyId = HttpContext.Current.Request.Form["CompanyId"];
                try
                {
                    DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/UploadedFiles/teacher/documents/"));
                    if (!di.Exists) di.Create();

                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        //var ext = Path.GetExtension(file.FileName);
                        //var filename = $"{prefix}_{ID}_{i}{ext}";
                        var filename = $"{prefix}_{ID}_{CompanyId}_{file.FileName}";
                        var filePath = Path.Combine(di.FullName, filename);

                        file.SaveAs(filePath);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return true;
        }

        [Route("UploadReceiptDocuments"), HttpPost]
        public bool UploadReceiptDocuments()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var prefix = HttpContext.Current.Request.Form["Prefix"];
                var StudentId = HttpContext.Current.Request.Form["StudentId"];
                var ReceiptId = HttpContext.Current.Request.Form["ReceiptId"];
                var CompanyId = HttpContext.Current.Request.Form["CompanyId"];
                try
                {
                    DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/UploadedFiles/student/documents"));
                    if (!di.Exists) di.Create();

                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        //var ext = Path.GetExtension(file.FileName);
                        //var filename = $"{prefix}_{ID}_{i}{ext}";
                        var filename = $"{prefix}_{StudentId}_{CompanyId}_{ReceiptId}_{file.FileName}";
                        var filePath = Path.Combine(di.FullName, filename);

                        file.SaveAs(filePath);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return true;
        }

        [Route("GetAllExtraFee"),HttpGet]
        public IHttpActionResult GetAllExtraFee()
        {
            var result = BaseDbServices.Instance.GetData("select FeeName,Amount from tblfeedetails where FeeStructureName='Extra'", null);
            return Ok(result);
        }
        [Route("GetTeacherDetails")]
        public IHttpActionResult GetTeacherDetails()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var result = TeacherDbServices.Instance.GetAllTeachers(CompanyId);
            return Ok(result);
        }

        [Route("GetListOfCompany"),HttpGet]
        public IHttpActionResult GetListOfCompany()
        {
            var result = CompanyDbServices.Instance.GetListOfCompany();
            return Ok(result);
        }

        [Route("GetStudentDetails")]
        public IHttpActionResult GetStudentDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var result = StudentDBServices.Instance.GetAllStudents(Batch,Class,CompanyId);
            //var data =BaseDbServices.Instance.GetData("SELECT * FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY StudentId) as row FROM tblstudentinfo) a WHERE IsDeleted = 0 and", pageNo, pageSize);
            removeUnwantedColumns(ref result, "StudentDetails");
            return Ok(result);
        }

        [Route("GetAllSpecialScholarshipDetails")]
        public IHttpActionResult GetAllSpecialScholarshipDetails()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var result = ScholarshipDbServices.Instance.GetAllSpecialScholarship(CompanyId);
            return Ok(result);
        }

        [Route("GetStudentListBySection"), HttpGet]
        public IHttpActionResult GetStudentListBySection()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var _class = data["Class"];
            var section = data["Section"];
            var date = data["Date"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var list = StudentDBServices.Instance.GetStudentListBySection( _class, section,date,CompanyId);
            list.Columns.Add("Attendance");
            for (int k = 0; k < list.Rows.Count; k++)
            {
                list.Rows[k]["Attendance"] = "p";
            }

            var checkattendance = StudentDBServices.Instance.CheckStudentAttendance(batch, date,CompanyId);
            if (checkattendance.Rows.Count != 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    for (int j = 0; j < checkattendance.Rows.Count; j++)
                    {
                        if (list.Rows[i]["StudentId"].ToString() == checkattendance.Rows[j]["StudentId"].ToString())
                        {
                            list.Rows[i]["Attendance"] = checkattendance.Rows[j]["Attendance"];
                        }
                    }
                }
            }
            return Ok(list);

        }

        [Route("GetStaffListByDesignation"), HttpGet]
        public IHttpActionResult GetStaffListByDesignation()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var department = data["Department"];
            var designation = data["Designation"];
            var date = data["Date"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var list = TeacherDbServices.Instance.GetStaffListByDesignation(batch, department, designation,CompanyId);
            list.Columns.Add("Attendance");
            for (int k = 0; k < list.Rows.Count; k++)
            {
                list.Rows[k]["Attendance"] = "p";
            }

            var checkattendance = TeacherDbServices.Instance.CheckStaffAttendance(batch, date,CompanyId);
            if (checkattendance.Rows.Count != 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    for (int j = 0; j < checkattendance.Rows.Count; j++)
                    {
                        if (list.Rows[i]["TeacherId"].ToString() == checkattendance.Rows[j]["TeacherId"].ToString())
                        {
                            list.Rows[i]["Attendance"] = checkattendance.Rows[j]["Attendance"];
                        }
                    }
                }
            }
            return Ok(list);

        }

        [Route("GetStudentListByClass"), HttpGet]
        public IHttpActionResult GetStudentListByClass()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var _class = data["Class"];
            var date = data["Date"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var list = StudentDBServices.Instance.GetStudentListByClass( _class,date,CompanyId);
            list.Columns.Add("Attendance");
            for (int k = 0; k < list.Rows.Count; k++)
            {
                list.Rows[k]["Attendance"] = "p";
            }

            var checkattendance = StudentDBServices.Instance.CheckStudentAttendance(batch, date,CompanyId);
            if (checkattendance.Rows.Count != 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    for (int j = 0; j < checkattendance.Rows.Count; j++)
                    {
                        if (list.Rows[i]["StudentId"].ToString() == checkattendance.Rows[j]["StudentId"].ToString())
                        {
                            list.Rows[i]["Attendance"] = checkattendance.Rows[j]["Attendance"];
                        }
                    }
                }
            }
            return Ok(list);

        }
        [Route("GetStaffListByDepartment"), HttpGet]
        public IHttpActionResult GetStaffListByDepartment()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var department = data["Department"];
            var date = data["Date"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var list = TeacherDbServices.Instance.GetStaffListByDepartment(batch, department,CompanyId);
            list.Columns.Add("Attendance");
            for (int k = 0; k < list.Rows.Count; k++)
            {
                list.Rows[k]["Attendance"] = "p";
            }

            var checkattendance = TeacherDbServices.Instance.CheckStaffAttendance(batch, date,CompanyId);
            if (checkattendance.Rows.Count != 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    for (int j = 0; j < checkattendance.Rows.Count; j++)
                    {
                        if (list.Rows[i]["TeacherId"].ToString() == checkattendance.Rows[j]["TeacherId"].ToString())
                        {
                            list.Rows[i]["Attendance"] = checkattendance.Rows[j]["Attendance"];
                        }
                    }
                }
            }
            return Ok(list);

        }

        [Route("GetStaffListByBatch")]
        public IHttpActionResult GetStaffListByBatch()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var date = data["Date"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var list = TeacherDbServices.Instance.GetStaffListByBatch(batch,CompanyId);
            list.Columns.Add("Attendance");
            for (int k = 0; k < list.Rows.Count; k++)
            {
                list.Rows[k]["Attendance"] = "p";
            }
            var checkattendance = TeacherDbServices.Instance.CheckStaffAttendance(batch, date,CompanyId);
            if (checkattendance.Rows.Count != 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    for (int j = 0; j < checkattendance.Rows.Count; j++)
                    {
                        if (list.Rows[i]["TeacherId"].ToString() == checkattendance.Rows[j]["TeacherId"].ToString())
                        {
                            list.Rows[i]["Attendance"] = checkattendance.Rows[j]["Attendance"];
                        }
                    }
                }
            }
            return Ok(list);
        }
        
        [Route("GetStudentListByBatch")]
        public IHttpActionResult GetStudentListByBatch()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var date = data["Date"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var list = StudentDBServices.Instance.GetStudentListByBatch(date,CompanyId);
            list.Columns.Add("Attendance");
            for(int k =0;k <list.Rows.Count; k++)
            {
                list.Rows[k]["Attendance"] = "p";
            }
            
            var checkattendance = StudentDBServices.Instance.CheckStudentAttendance(batch,date,CompanyId);
            if(checkattendance.Rows.Count != 0)
            { 
                for(int i =0; i<list.Rows.Count;i++)
                {
                    for(int j = 0; j<checkattendance.Rows.Count; j++)
                    {
                        if(list.Rows[i]["StudentId"].ToString() == checkattendance.Rows[j]["StudentId"].ToString())
                        {
                            list.Rows[i]["Attendance"] = checkattendance.Rows[j]["Attendance"];
                        }
                    }
                }
            }
            return Ok(list);
        }

        [Route("GetDueAmount/{id}")]
        public IHttpActionResult GetDueAmount(int id)
        {
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", id)
            };
            var dueAmount = BaseDbServices.Instance.GetData("Select DueAmount,TotalAmount from tblreceipt where StudentId=@Id and Status='Partial Paid'", Id);
            return Ok(dueAmount);
        }

        [Route("GetStudentClass"),HttpGet]
        public IHttpActionResult GetStudentClass()
        {
            var data = HttpContext.Current.Request;
            var StudentId = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var info = ScholarshipDbServices.Instance.GetStudentClass(StudentId,CompanyId);

            return Ok(info);
        }

        [Route("GetAllScholarshipId"),HttpGet]
        public IHttpActionResult GetAllScholarshipId()
        {
            var data = HttpContext.Current.Request;
            var name = data["Name"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Name = new List<MySqlParameter>()
            {
                new MySqlParameter("@Name", name),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var id = BaseDbServices.Instance.GetData("Select Id from tblscholarshipdetails where ScholarshipName=@Name and CompanyId=@CompanyId", Name);
            return Ok(id);
        }


        [Route("GetExtraFeeById"),HttpGet]
        public IHttpActionResult GetExtraFeeById()
        {
            var data = HttpContext.Current.Request;
            var Id = data["Id"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> info = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",Id),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var details = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId=@id and CompanyId=@CompanyId", info);
            return Ok(details);
        }

        [Route("GetBillingDetailsById"),HttpGet]
        public IHttpActionResult GetBillingDetailsById()
        {
            var data = HttpContext.Current.Request;
            var BillingId = data["BillingId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> info = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",BillingId),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var details = BaseDbServices.Instance.GetData("select tblbillingdetails.*,tblstudentinfo.FirstName,tblstudentinfo.LastName,tblbilling.StudentId,tblbilling.Batch,tblbilling.Month " +
                " from tblbillingdetails " +
                " inner join tblbilling on tblbillingdetails.BillingId = tblbilling.BillingId " +
                " inner join tblstudentinfo on tblstudentinfo.StudentId=tblbilling.StudentId" +
                " where tblbillingdetails.BillingId =@id and tblbillingdetails.CompanyId=@CompanyId",info);
            return Ok(details);
        }

        [Route("MultipleBillingDetails"),HttpPost]
        public IHttpActionResult MultipleBillingDetails([FromBody] MultipleBillingDelete obj)
        {
            foreach(var r in obj.BillingId)
            {
                List<MySqlParameter> Id = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@Id", r)
                    };
              
                BaseDbServices.Instance.RunQuery("Delete from tblbillingdetails where BillingId=@Id;" +
                "Delete from tblbilling where BillingId=@Id;" +
                "Delete from tblreceipt where BillingId=@Id", Id);
            }

            return Ok("");
        }
        [Route("DeleteBillingDetails"), HttpPost]
        public IHttpActionResult DeleteBillingDetails()
        {
            var data = HttpContext.Current.Request;
            var BillingId = data["BillingId"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", BillingId),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            //BaseDbServices.Instance.RunQuery("Insert into tbldeletedbilling select * from tblbilling where BillingId=@Id", Id);
            //BaseDbServices.Instance.RunQuery("Insert into tbldeletedbillingdetails select * from tblbillingdetails where BillingId=@Id", Id);
            //BaseDbServices.Instance.RunQuery("Insert into tbldeletedreceipt select * from tblreceipt where BillingId=@Id", Id);
            BaseDbServices.Instance.RunQuery("Delete from tblbillingdetails where BillingId=@Id and CompanyId=@CompanyId;" +
                "Delete from tblbilling where BillingId=@Id and CompanyId=@CompanyId;" +
                "Delete from tblreceipt where BillingId=@Id and CompanyId=@CompanyId", Id);

            return Ok("");
        }

        [Route("DeleteReceiptDetails"), HttpPost]
        public IHttpActionResult DeleteReceiptDetails()
        {
            var data = HttpContext.Current.Request;
            var ReceiptId = data["ReceiptId"];
            var Batch = data["Batch"];
            var StudentId = data["StudentId"];
            var Month = data["Month"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", ReceiptId),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            //BaseDbServices.Instance.RunQuery("Insert into tbldeletedreceipt select * from tblreceipt where ReceiptId=@Id", Id);
            BaseDbServices.Instance.RunQuery("Delete from tblreceipt where ReceiptId=@Id and CompanyId=@CompanyId",Id);

            var info = BaseDbServices.Instance.GetData("select * from tblreceipt where Batch='" + Batch + "' and StudentId='" + StudentId + "' and Month='" + Month + "' and CompanyId='"+CompanyId+"'", null);
            if(info.Rows.Count > 0)
            {
                BaseDbServices.Instance.RunQuery("Update tblbilling set Status='Partial Paid',IsCreated=1 where Batch='" + Batch + "' and StudentId='" + StudentId + "' and Month='" + Month + "' and CompanyId='" + CompanyId + "'", null);
            }
            else
            {
                BaseDbServices.Instance.RunQuery("Update tblbilling set Status='UnPaid',IsCreated=0 where Batch='" + Batch + "' and StudentId='" + StudentId + "' and Month='" + Month + "' and CompanyId='" + CompanyId + "'", null);
            }

            return Ok("");
        }

        [Route("DeleteStudentExtraFeeDetails"),HttpPost]
        public IHttpActionResult DeleteStudentExtraFeeDetails()
        {
            var data = HttpContext.Current.Request;
            var Id = data["Id"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Info = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", Id),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            BaseDbServices.Instance.RunQuery("delete from tblstudentextrafeedetails where StudentExtraFeeId=@Id and CompanyId=@CompanyId", Info);
            BaseDbServices.Instance.RunQuery("delete from tblstudentextrafee where StudentExtraFeeId=@Id and CompanyId=@CompanyId", Info);
            return Ok("");
        }

        [Route("DeleteStudentTransportDetails"),HttpPost]
        public IHttpActionResult DeleteStudentTransportDetails()
        {
            var data = HttpContext.Current.Request;
            var Id = data["Id"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Info = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", Id),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            BaseDbServices.Instance.RunQuery("Delete from tblstudenttransport where StudentTransportId=@Id and CompanyId=@CompanyId", Info);
            return Ok("");
        }

        [Route("GetDueStatus"),HttpGet]
        public IHttpActionResult GetDueStatus()
        {
            var data= HttpContext.Current.Request;
            var StudentId = data["StudentId"];
            var Month = data["Month"];
            var BillingId = data["BillingId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            List<MySqlParameter> Status = new List<MySqlParameter>()
            {
                new MySqlParameter("@StudentId", StudentId),
                new MySqlParameter("@Month",Month),
                 new MySqlParameter("@BillingId",BillingId),
                 new MySqlParameter("@CompanyId",CompanyId)
            };
                var previousMonthDue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as dis,sum(Fine) as fine from tblreceipt" +
                    " where tblreceipt.StudentId=@StudentId and tblreceipt.Month=@Month and tblreceipt.BillingId =@BillingId and tblreceipt.CompanyId=@CompanyId", Status);

                return Ok(previousMonthDue);
            
        }

        [Route("GetCurrentStatus"), HttpGet]
        public IHttpActionResult GetCurrentStatus()
        {
            var data = HttpContext.Current.Request;
            var StudentId = data["StudentId"];
            var Month = data["Month"];
            var BillingId = data["BillingId"];

            List<MySqlParameter> Status = new List<MySqlParameter>()
            {
                new MySqlParameter("@StudentId", StudentId),
                new MySqlParameter("@Month",Month),
                 new MySqlParameter("@BillingId",BillingId)
            };
            var MonthDue = BaseDbServices.Instance.GetData("Select DueAmount from tblreceipt" +
                " where tblreceipt.StudentId=@StudentId and tblreceipt.Month =@Month order by ReceiptId DESC", Status);

            return Ok(MonthDue);
        }

        [Route("GetPaidStatus"), HttpGet]
        public IHttpActionResult GetPaidStatus()
        {
            var data = HttpContext.Current.Request;
            var StudentId = data["StudentId"];
            var Month = data["Month"];
            List<MySqlParameter> Status = new List<MySqlParameter>()
            {
                new MySqlParameter("@StudentId", StudentId),
                new MySqlParameter("@Month",Month)
            };
            var StatusDetails = BaseDbServices.Instance.GetData("Select * from tblreceipt where StudentId=@StudentId and Month=@Month", Status);
            if (StatusDetails.Rows.Count > 0)
            {
                for (int i = 0; i < StatusDetails.Rows.Count; i++)
                {
                    if (StatusDetails.Rows[i]["Status"].ToString() == "Partial Paid")
                    {
                        var Details = BaseDbServices.Instance.GetData("select sum(PaidAmount) as PaidAmount from tblreceipt where StudentId=@StudentId and Month=@Month", Status);
                        return Ok(Details);

                    }
                }
                
            }
            else
            {
                return Ok(false);
            }
            return Ok(false);
        }

        [Route("GetDueStudentList"),HttpGet]
        public IHttpActionResult GetDueStudentList()
        {
            var data = HttpContext.Current.Request;
            var Batch =data["Batch"];
            var Class= data["Class"];
            var StudentId = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            if(StudentId !="")
            {
                var studentlist = DueDbService.Instance.GetAllStudentListDueByStudentId(StudentId,CompanyId);
                return Ok(studentlist);
            }
            if (Batch == "" && Class == "")
            {
                var studentlist = DueDbService.Instance.GetAllStudentListDue(CompanyId);
                return Ok(studentlist);
            }
            else if(Batch !="" && Class =="")
            {
                var studentlist = DueDbService.Instance.GetAllStudentListDueByBatch(Batch,CompanyId);
                return Ok(studentlist);
            }
            else if (Batch == "" && Class != "")
            {
                var studentlist = DueDbService.Instance.GetAllStudentListDueByClass(Class,CompanyId);
                return Ok(studentlist);
            }
            else if (Batch != "" && Class != "")
            {
                var studentlist = DueDbService.Instance.GetAllStudentListDueByBatchClass(Batch,Class,CompanyId);
                return Ok(studentlist);
            }
            return Ok("");
        }

        [Route("GetPrintBillingDetails"),HttpGet]
        public IHttpActionResult GetPrintBillingDetails()
        {
            var data = HttpContext.Current.Request;
            var BillingId = Convert.ToInt32(data["BillingId"]);
            var Month = data["Month"];
            var Batch = data["Batch"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            int from = Convert.ToInt32(Batch.Substring(0, 4));
            int to = Convert.ToInt32(Batch.Substring(5, 4));
            var checkpreviousyear = new DataTable();

            from = from - 1;
            to = to - 1;
            var batch = (from + "-" + to).ToString();
            var billingInfo = BillingDbServices.Instance.GetBillingInfo(BillingId,CompanyId);
            var studentid = BaseDbServices.Instance.GetData("select StudentId from tblbilling where BillingId='" + BillingId + "' and CompanyId='"+CompanyId+"'", null);
            var PreviousYearDue= BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as dis,sum(Fine) as fine from tblreceipt" +
                " where tblreceipt.StudentId='" + studentid.Rows[0]["StudentId"] + "' and tblreceipt.Batch ='" + batch + "' and tblreceipt.CompanyId='"+ CompanyId + "'", null);
            var PreviousDue = new DataTable();
            var totalfee = new DataTable();
            if (PreviousYearDue.Rows[0]["paid"] is DBNull)
            {
                PreviousDue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as dis,sum(Fine) as fine from tblreceipt" +
                " where tblreceipt.StudentId='" + studentid.Rows[0]["StudentId"] + "' and tblreceipt.Month < '" + Month + "' and tblreceipt.Batch ='" + Batch + "' and tblreceipt.CompanyId='"+CompanyId+"'", null);
                if(PreviousDue.Rows[0]["paid"] is DBNull)
                {
                    PreviousDue.Rows[0]["paid"] = 0;
                    PreviousDue.Rows[0]["dis"] = 0;
                    PreviousDue.Rows[0]["fine"] = 0;
                }
                else
                {

                }
            }
            else
            {

                PreviousDue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as dis,sum(Fine) as fine from tblreceipt" +
                " where tblreceipt.StudentId='"+ studentid.Rows[0]["StudentId"] + "' and tblreceipt.Month < '"+ Month + "' and tblreceipt.Batch ='" +Batch+ "' and tblreceipt.CompanyId='" + CompanyId + "'", null);
                if (PreviousDue.Rows[0]["paid"] is DBNull)
                {
                    PreviousDue.Rows[0]["paid"] = 0 + Convert.ToInt32(PreviousYearDue.Rows[0]["paid"]);
                    PreviousDue.Rows[0]["dis"] = 0 + Convert.ToInt32(PreviousYearDue.Rows[0]["dis"]);
                    PreviousDue.Rows[0]["fine"] = 0 + Convert.ToInt32(PreviousYearDue.Rows[0]["fine"]);

                }
                else
                {

                    PreviousDue.Rows[0]["paid"] = Convert.ToInt32(PreviousDue.Rows[0]["paid"]) + Convert.ToInt32(PreviousYearDue.Rows[0]["paid"]);
                    PreviousDue.Rows[0]["dis"] = Convert.ToInt32(PreviousDue.Rows[0]["dis"]) + Convert.ToInt32(PreviousYearDue.Rows[0]["dis"]);
                    PreviousDue.Rows[0]["fine"] = Convert.ToInt32(PreviousDue.Rows[0]["fine"]) + Convert.ToInt32(PreviousYearDue.Rows[0]["fine"]);
                }
            }
            var PreviousYeartotalfee = BaseDbServices.Instance.GetData("select  sum(tblbillingdetails.Amount) as total from tblbillingdetails" +
                " INNER join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId" +
                " where tblbilling.StudentId='" + studentid.Rows[0]["StudentId"] + "'and tblbilling.Batch = '" + batch + "' and tblbilling.CompanyId='" + CompanyId + "'", null);
            if(PreviousYeartotalfee.Rows[0]["total"] is DBNull)
            {
                totalfee = BaseDbServices.Instance.GetData("select  sum(tblbillingdetails.Amount) as total from tblbillingdetails" +
                " INNER join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId" +
                " where tblbilling.StudentId='" + studentid.Rows[0]["StudentId"] + "' and tblbilling.Month < '" + Month + "' and tblbilling.Batch = '" + Batch + "' and tblbilling.CompanyId='" + CompanyId + "'", null);
                if(totalfee.Rows[0]["total"] is DBNull)
                {
                    totalfee.Rows[0]["total"] = 0;
                }
            }
            else
            {

             totalfee = BaseDbServices.Instance.GetData("select  sum(tblbillingdetails.Amount) as total from tblbillingdetails" +
                " INNER join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId" +
                " where tblbilling.StudentId='" + studentid.Rows[0]["StudentId"] + "' and tblbilling.Month < '" + Month + "' and tblbilling.Batch = '" + Batch + "' and tblbilling.CompanyId='" + CompanyId + "'", null);
                if (totalfee.Rows[0]["total"] is DBNull)
                {
                    totalfee.Rows[0]["total"] = 0;
                }
                totalfee.Rows[0]["total"] = Convert.ToInt32(PreviousYeartotalfee.Rows[0]["total"]) + Convert.ToInt32(totalfee.Rows[0]["total"]);
            }

            var billingdetails = BillingDbServices.Instance.GetPrintBillingInfo(BillingId,CompanyId);
            var details = new DataTable();
            details.Columns.Add("FeeName");
            details.Columns.Add("Amount");
            var YearlyAmount = 0;
            var MonthlyAmount = 0;
            for (int i =0; i <billingdetails.Rows.Count;i++)
            {
                if(billingdetails.Rows[i]["FeeStructureName"].ToString() == "Yearly")
                {
                    YearlyAmount += Convert.ToInt32(billingdetails.Rows[i]["Amount"]);
                }
                else if(billingdetails.Rows[i]["FeeStructureName"].ToString() == "Monthly" && billingdetails.Rows[i]["FeeName"].ToString() != "Lodging and Fooding")
                {
                    MonthlyAmount += Convert.ToInt32(billingdetails.Rows[i]["Amount"]);
                }
                else
                {
                    DataRow row = details.NewRow();
                    row["FeeName"] = billingdetails.Rows[i]["FeeName"];
                    row["Amount"] =billingdetails.Rows[i]["Amount"];
                    details.Rows.Add(row);
                }
            }
            DataRow row1 = details.NewRow();
            row1["FeeName"] = "Yearly";
            row1["Amount"] = YearlyAmount;
            details.Rows.Add(row1);
            DataRow row2 = details.NewRow();
            row2["FeeName"] = "Monthly";
            row2["Amount"] = MonthlyAmount;
            details.Rows.Add(row2);

            var currentDue= BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as dis,sum(Fine) as fine from tblreceipt" +
                " where tblreceipt.BillingId='"+ BillingId + "' and tblreceipt.CompanyId='"+CompanyId+"'", null);
            if(currentDue.Rows[0]["paid"] is DBNull)
            {
                currentDue.Rows[0]["paid"] = 0;
                currentDue.Rows[0]["dis"] = 0;
                currentDue.Rows[0]["fine"] = 0;
            }
            List<DataTable> lst = new List<DataTable>()
            {
                billingInfo,
                details,
                PreviousDue,
                totalfee,
                currentDue
            };
            return Ok(lst);
        }

        [Route("GetPrintReceiptDetails"), HttpGet]
        public IHttpActionResult GetPrintReceiptDetails()
        {
            var data = HttpContext.Current.Request;
            var ReceiptId = Convert.ToInt32(data["ReceiptId"]);
            var BillingId = Convert.ToInt32(data["BillingId"]);
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var receiptInfo = BillingDbServices.Instance.GetBillingInfo(BillingId,CompanyId);
            var receiptdetails = ReceiptDbServices.Instance.GetPrintReceiptInfo(ReceiptId,CompanyId);
            var advancedpaid = BaseDbServices.Instance.GetData("select Amount from tbladvancedpaid where ReceiptId='" + ReceiptId + "'", null);
            List<DataTable> lst = new List<DataTable>()
            {
                receiptInfo,
                receiptdetails,
                advancedpaid
            };
            return Ok(lst);
        }

        [Route("GetReceiptPrintInfo"),HttpGet]
        public IHttpActionResult GetReceiptPrintInfo()
        {
            var data = HttpContext.Current.Request;
            var ReceiptId = data["ReceiptId"];
            var BillingId = data["BillingId"];
            List<MySqlParameter> Detail = new List<MySqlParameter>()
            {
                new MySqlParameter("@receiptid",ReceiptId),
                new MySqlParameter("@billingid", BillingId)
            };
            var receiptdetails = BaseDbServices.Instance.GetData("Select tblreceipt.*,tblstudentinfo.FirstName,tblstudentinfo.LastName,tblcurrenteducation.Batch,tblcurrenteducation.Class,tblcurrenteducation.Faculty" +
                " from tblreceipt" +
                " inner join tblstudentinfo on tblreceipt.StudentId=tblstudentinfo.StudentId" +
                " inner join tblcurrenteducation on tblreceipt.StudentId = tblcurrenteducation.StudentId" +
                " where ReceiptId=@receiptid", Detail);
            
            return Ok(receiptdetails);

        }

        [Route("GetPreviousMonthBilling"),HttpGet]
        public IHttpActionResult GetPreviousMonthBilling()
        {
            var data = HttpContext.Current.Request;
            var Id = data["Id"];
            var StudentId = data["StudentId"];
            var Month = data["Month"];
            var Batch = data["Batch"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            List<MySqlParameter> Detail = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",Id),
                new MySqlParameter("@StudentId", StudentId),
                new MySqlParameter("@Month", Month),
                new MySqlParameter("@Batch",Batch),
                new MySqlParameter("@CompanyId",CompanyId)
            };

            int from = Convert.ToInt32(Batch.Substring(0, 4));
            int to = Convert.ToInt32(Batch.Substring(5, 4));
            var checkpreviousyear = new DataTable();
           
                from = from - 1;
                to = to - 1;
                var batch = (from + "-" + to).ToString();
                checkpreviousyear = BaseDbServices.Instance.GetData("select * from tblbilling where StudentId =@StudentId and (Status ='Unpaid' or Status='Partial Paid') and Batch ='" + batch + "' and CompanyId=@CompanyId", Detail);
            if (checkpreviousyear.Rows.Count > 0)
            {
                return Ok(checkpreviousyear);
            }
            else
            {
                checkpreviousyear = BaseDbServices.Instance.GetData("select * from tblbilling where StudentId=@StudentId and (Status ='Unpaid' or Status='Partial Paid') and Batch =@Batch and Month < @Month and CompanyId=@CompanyId order by Month", Detail);

                return Ok(checkpreviousyear);
            }

        }

        [Route("GetReceiptDetail"), HttpGet]
        public IHttpActionResult GetReceiptDetail()
        {
            var data = HttpContext.Current.Request;
            var Id = data["Id"];
            var StudentId = data["StudentId"];
            var Month = data["Month"];
            var Batch = data["Batch"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            List<MySqlParameter> Detail = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",Id),
                new MySqlParameter("@StudentId", StudentId),
                new MySqlParameter("@Month", Month),
                new MySqlParameter("@Batch",Batch),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            int from = Convert.ToInt32(Batch.Substring(0, 4));
            int to = Convert.ToInt32(Batch.Substring(5,4));

            var checkststus = BaseDbServices.Instance.GetData("select Status from tblbilling where BillingId=@id and CompanyId=@CompanyId", Detail);
            if (checkststus.Rows[0]["Status"].ToString() == "Paid")
            {
                return Ok(true);
            }
            else 
            {
                from = from - 1;
                to = to - 1;
                var batch = (from + "-" + to).ToString();
                var checkpreviousyear = BaseDbServices.Instance.GetData("select * from tblbilling where StudentId =@StudentId and (Status ='Unpaid' or Status='Partial Paid') and Batch ='" + batch + "' and CompanyId=@CompanyId", Detail);
                if(checkpreviousyear.Rows.Count > 0)
                {
                    return Ok(false);
                }
                else
                {
                    var checkpreviousmonth = BaseDbServices.Instance.GetData("select * from tblbilling where StudentId=@StudentId and (Status ='Unpaid' or Status='Partial Paid') and Batch =@Batch and Month < @Month and CompanyId=@CompanyId order by Month", Detail);
                    if(checkpreviousmonth.Rows.Count > 0)
                    {
                        return Ok(false);
                    }
                    else
                    {
                        var detail = BillingDbServices.Instance.GetReceiptDetail(Id,CompanyId);

                        return Ok(detail);
                    }
                }
                

                
            }
        }

        [Route("GetClassByFaculty"),HttpGet]
        public IHttpActionResult GetClassByFaculty()
        {
            var data = HttpContext.Current.Request;
            var faculty = data["Faculty"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> info = new List<MySqlParameter>()
            {
                new MySqlParameter("@faculty",faculty),
                new MySqlParameter("@CompanyId",CompanyId),
              
            };
            var id= BaseDbServices.Instance.GetData("select FacultyId from tblfacultydetails where FacultyName=@faculty and CompanyId=@CompanyId",info);
            var details = BaseDbServices.Instance.GetData("select ClassName from tblclassdetails where FacultyId='" + id.Rows[0]["FacultyId"] + "' and CompanyId=@CompanyId", info);
            return Ok(details);
        }

        [Route("GetAllClass"),HttpPost]
        public IHttpActionResult GetAllClass([FromBody] DayOff obj)
        {
            
            List<DataTable> lst = new List<DataTable>();
            if (obj.Faculty != null)
            {
                foreach (var r in obj.Faculty)
                {
                    var id = BaseDbServices.Instance.GetData("select FacultyId from tblfacultydetails where FacultyName='" + r + "' and CompanyId='"+obj.CompanyId+"'", null);
                    var details = BaseDbServices.Instance.GetData("select ClassName from tblclassdetails where FacultyId='" + id.Rows[0]["FacultyId"] + "' and CompanyId='"+obj.CompanyId+"'", null);
                    lst.Add(details);
                }
            }
            return Ok(lst);
        }

        [Route("GetFine"),HttpGet]
        public IHttpActionResult GetFine()
        {
            var data = HttpContext.Current.Request;
            var date = data["Date"];
            var total = data["Total"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var discountAmount = 0;
            var today = DateTime.Now.ToString("MM/dd/yyyy");
            DateTime billingdate = Convert.ToDateTime(date);
            DateTime receiptdate = Convert.ToDateTime(today);
            TimeSpan span = receiptdate - billingdate;
            int counter = span.Days + 1;
            var fine = BaseDbServices.Instance.GetData("select FineType,FineAmount from tblfine where DayFrom <= '" + counter + "' and DayTo >= '" + counter + "' and CompanyId='"+CompanyId+"'", null);
            if(fine.Rows.Count > 0)
            {
                int fineamount = 0;
                
                var type = fine.Rows[0]["FineType"].ToString();
                if(type == "percentage")
                {
                    fineamount= Convert.ToInt32(fine.Rows[0]["FineAmount"]);
                    discountAmount = (int)Math.Round((double)(Convert.ToInt32(total) * fineamount) / 100);
                }
                else if(type == "flat")
                {
                    discountAmount = Convert.ToInt32(fine.Rows[0]["FineAmount"]);
                }
            }
           
            return Ok(discountAmount);
        }

        [Route("GetTotalFee/{id}")]
        public IHttpActionResult GetTotalFee(string id)
        {
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", id)
            };
            var detail = BaseDbServices.Instance.GetData("Select sum(Amount) as Total from tblbillingdetails where BillingId=@Id", Id);

            return Ok(detail);
        }


        [Route("GetSelectedMonth/{id}")]
        public IHttpActionResult GetSelectedMonth(int id)
        {
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", id)
            };
            var month = BaseDbServices.Instance.GetData("Select Month from tblbilling where Billingid=@Id", Id);
            return Ok(month);
        }

        [Route("GetSpecialScholarshipId"),HttpGet]
        public IHttpActionResult GetSpecialScholarshipId()
        {
            var data = HttpContext.Current.Request;
            var StudentId = data["StudentId"];
            var Batch = data["Batch"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", StudentId),
                new MySqlParameter("@Batch", Batch),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var scholarshipId = BaseDbServices.Instance.GetData("Select SpecialScholarshipId from tblspecialscholarship where StudentId=@Id and Batch=@Batch and CompanyId=@CompanyId", Id);
            return Ok(scholarshipId);
        }

        [Route("GetSpecificScholarship/{id}")]
        public IHttpActionResult GetSpecificScholarship(int id)
        {
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", id)
            };
            var classdetails = BaseDbServices.Instance.GetData("Select Batch,Class,Faculty from tblspecialscholarship where StudentId=@id", Id);
            if (classdetails.Rows.Count == 0)
            {
                return Ok("");
            }
            else
            {
                var batch = classdetails.Rows[0]["Batch"];
                var _class = classdetails.Rows[0]["Class"];
                var faculty = classdetails.Rows[0]["Faculty"];

                List<MySqlParameter> Fee = new List<MySqlParameter>()
            {
                new MySqlParameter("@batch", batch),
                new MySqlParameter("@class",_class),
                new MySqlParameter("@faculty",faculty)
            };
                var batchclassid = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@batch and Faculty=@faculty and Class=@class", Fee);
                var ids = batchclassid.Rows[0]["BatchClassId"];
                var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails", null);
                List<DataTable> lst = new List<DataTable>();
                var FeeDetails = new DataTable();
                int total;
                int amount;
                int discount;
                var FeeInfo = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where Batch=@batch and Class=@class and Faculty=@faculty", Fee);
                if (FeeInfo.Rows.Count > 0)
                {

                    for (int i = 0; i < FeeStructure.Rows.Count; i++)
                    {
                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=" + batchclassid.Rows[0]["BatchClassId"], null);
                        FeeDetails.Columns.Add("IsChecked");
                        FeeDetails.Columns.Add("DiscountType");
                        FeeDetails.Columns.Add("Discount");
                        FeeDetails.Columns.Add("TotalAmount");
                        lst.Add(FeeDetails);
                        var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Studentid=@Id", Id);
                        for (int k = 0; k < FeeDetails.Rows.Count; k++)
                        {
                            for (int j = 0; j < SelectedFee.Rows.Count; j++)
                            {

                                if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                                {
                                    amount = (int)SelectedFee.Rows[j]["Amount"];
                                    discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                    var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                    total = (int)Math.Round((double)amount - discountAmount);
                                    FeeDetails.Rows[k]["TotalAmount"] = total;
                                    FeeDetails.Rows[k]["IsChecked"] = true;
                                    FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
                                    FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
                                    FeeDetails.Rows[k]["IsChecked"] = true;


                                }
                                else if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
                                {
                                    amount = (int)SelectedFee.Rows[j]["Amount"];
                                    discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
                                    total = (int)Math.Round((double)amount - discount);
                                    FeeDetails.Rows[k]["TotalAmount"] = total;
                                    FeeDetails.Rows[k]["IsChecked"] = true;
                                    FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
                                    FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
                                    FeeDetails.Rows[k]["IsChecked"] = true;
                                }


                            }
                        }
                    }

                }
                return Ok(lst);
            }
        }

        [Route("GetBillingInfo"), HttpGet]
        public IHttpActionResult GetBillingInfo()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var _class = data["Class"];
            var faculty = data["Faculty"];
            var BillingId = data["BillingId"];
            var StudentId = data["StudentId"];
            var Section = data["Section"];
            List<MySqlParameter> Fee = new List<MySqlParameter>()
            {
                new MySqlParameter("@batch", batch),
                new MySqlParameter("@class",_class),
                new MySqlParameter("@faculty",faculty),
                new MySqlParameter("@section",Section)
            };


            List<MySqlParameter> Studentid = new List<MySqlParameter>()
            {
                new MySqlParameter("@studentId",StudentId)
            };
            var billingId = new DataTable();
            if (StudentId == "null")
            {
                if (Section == "null")
                {
                    billingId = BaseDbServices.Instance.GetData("Select BillingId from tblbilling where Batch=@batch and Class=@class and Faculty=@faculty", Fee);

                }
                else
                {
                    billingId = BaseDbServices.Instance.GetData("Select BillingId from tblbilling where Batch=@batch and Class=@class and Faculty=@faculty and Section=@section", Fee);

                }
            }
            else
            {
                billingId = BaseDbServices.Instance.GetData("Select BillingId from tblbilling where StudentId=@studentId", Studentid);

            }

            var id = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@batch and Faculty=@faculty and Class=@class", Fee);
            var ids = id.Rows[0]["BatchClassId"];
            var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails", null);
            List<DataTable> lst = new List<DataTable>();
            var FeeDetails = new DataTable();
            int total;
            int amount;
            int discount;
            int discountAmount;
            //var SpecialFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where StudentId=@studentId", Studentid);
            //var NormalScholarship = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where Batch=@batch and Faculty=@faculty and Class=@class", Fee);
            //if (SpecialFee.Rows.Count > 0)
            //{
            for (int i = 0; i < FeeStructure.Rows.Count; i++)
            {
                FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=" + id.Rows[0]["BatchClassId"], null);
                FeeDetails.Columns.Add("IsChecked");
                FeeDetails.Columns.Add("DiscountType");
                FeeDetails.Columns.Add("Discount");
                FeeDetails.Columns.Add("TotalAmount");
                lst.Add(FeeDetails);
                var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblbillingdetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BillingId=" + billingId.Rows[0]["BillingId"], null);
                for (int k = 0; k < FeeDetails.Rows.Count; k++)
                {
                    for (int j = 0; j < SelectedFee.Rows.Count; j++)
                    {

                        if (FeeDetails.Rows[k]["FeeName"].ToString() == SelectedFee.Rows[j]["FeeName"].ToString() && FeeDetails.Rows[k]["FeeStructureName"].ToString() == SelectedFee.Rows[j]["FeeStructureName"].ToString())
                        {
                            if (SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
                            {
                                amount = (int)SelectedFee.Rows[j]["Amount"];
                                discount = (int)SelectedFee.Rows[j]["Discount"];
                                discountAmount = (int)Math.Round((double)(amount * discount) / 100);
                                total = (int)Math.Round((double)amount - discountAmount);
                                FeeDetails.Rows[k]["TotalAmount"] = total;
                                FeeDetails.Rows[k]["IsChecked"] = true;
                                FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
                                FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
                                FeeDetails.Rows[k]["IsChecked"] = true;
                            }
                            else
                            {

                                amount = (int)SelectedFee.Rows[j]["Amount"];
                                discount = (int)SelectedFee.Rows[j]["Discount"];
                                total = (int)Math.Round((double)amount - discount);
                                FeeDetails.Rows[k]["TotalAmount"] = total;
                                FeeDetails.Rows[k]["IsChecked"] = true;
                                FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
                                FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
                                FeeDetails.Rows[k]["IsChecked"] = true;
                            }

                        }
                        else
                        {

                        }


                    }
                }
            }
            return Ok(lst);
            //}
            //else if(NormalScholarship.Rows.Count > 0)
            //{

            //}


        }

        //[Route("GetSelectedScholarshipFee"), HttpGet]
        //public IHttpActionResult GetSelectedScholarshipFee()
        //{
        //    var data = HttpContext.Current.Request;
        //    var batch = data["Batch"];
        //    var _class = data["Class"];
        //    var faculty = data["Faculty"];

        //    List<MySqlParameter> Fee = new List<MySqlParameter>()
        //    {
        //        new MySqlParameter("@batch", batch),
        //        new MySqlParameter("@class",_class),
        //        new MySqlParameter("@faculty",faculty)
        //    };
        //    var id = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@batch and Faculty=@faculty and Class=@class", Fee);
        //    var ids = id.Rows[0]["BatchClassId"];
        //    var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails", null);
        //    List<DataTable> lst = new List<DataTable>();
        //    var FeeDetails = new DataTable();
        //    int total;
        //    int amount;
        //    int discount;
        //    var FeeInfo = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where Batch=@batch and Class=@class and Faculty=@faculty", Fee);
        //    if (FeeInfo.Rows.Count > 0)
        //    {

        //        for (int i = 0; i < FeeStructure.Rows.Count; i++)
        //        {
        //            FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=" + id.Rows[0]["BatchClassId"], null);
        //            FeeDetails.Columns.Add("IsChecked");
        //            FeeDetails.Columns.Add("DiscountType");
        //            FeeDetails.Columns.Add("Discount");
        //            FeeDetails.Columns.Add("TotalAmount");
        //            lst.Add(FeeDetails);
        //          var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Batch=@batch and Class=@class and Faculty=@faculty", Fee);
        //            for (int k = 0; k < FeeDetails.Rows.Count; k++)
        //            {
        //                for (int j = 0; j < SelectedFee.Rows.Count; j++)
        //                {

        //                    if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "percentage")
        //                    {
        //                        amount = (int)SelectedFee.Rows[j]["Amount"];
        //                        discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
        //                        var discountAmount = (int)Math.Round((double)(amount * discount) / 100);
        //                        total = (int)Math.Round((double)amount - discountAmount);
        //                        FeeDetails.Rows[k]["TotalAmount"] = total;
        //                        FeeDetails.Rows[k]["IsChecked"] = true;
        //                        FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
        //                        FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
        //                        FeeDetails.Rows[k]["IsChecked"] = true;


        //                    }
        //                    else if(FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString() && SelectedFee.Rows[j]["DiscountType"].ToString() == "flat")
        //                    {
        //                        amount = (int)SelectedFee.Rows[j]["Amount"];
        //                        discount = Convert.ToInt32(SelectedFee.Rows[j]["Discount"]);
        //                        total = (int)Math.Round((double)amount - discount);
        //                        FeeDetails.Rows[k]["TotalAmount"] = total;
        //                        FeeDetails.Rows[k]["IsChecked"] = true;
        //                        FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
        //                        FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
        //                        FeeDetails.Rows[k]["IsChecked"] = true;
        //                    }


        //                }
        //            }
        //        }
        //        return Ok(lst);
        //    }
        //    else
        //    {
        //        for (int i = 0; i < FeeStructure.Rows.Count; i++)
        //        {


        //            FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=" + id.Rows[0]["BatchClassId"], null);
        //            FeeDetails.Columns.Add("DiscountType");
        //            FeeDetails.Columns.Add("Discount");
        //            FeeDetails.Columns.Add("TotalAmount");
        //            FeeDetails.Columns.Add("IsChecked");
        //            for (int j = 0; j < FeeDetails.Rows.Count; j++)
        //            {
        //                FeeDetails.Rows[j]["IsChecked"] = true;
        //                FeeDetails.Rows[j]["TotalAmount"] = FeeDetails.Rows[j]["Amount"];

        //            }
        //            lst.Add(FeeDetails);
        //        }
        //        return Ok(lst);
        //    }

        //}

        
        [Route("DeleteUserDetails/{id}"),HttpPost]
        public IHttpActionResult DeleteUserDetails(int id)
        {
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",id)
            };
            BaseDbServices.Instance.RunQuery("Delete tbluserdetail where UserID=@id",Id);
            return Ok("");
        }

        [Route("GetAllStudentListByFaculty"),HttpGet]
        public IHttpActionResult GetAllStudentListByFaculty()
        {
            var data = HttpContext.Current.Request;
            var faculty = data["Faculty"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var studentdetails = StudentTransportDbServices.Instance.GetAllStudentListByFaculty(faculty,CompanyId);
            var pickUpPoint = BaseDbServices.Instance.GetData("select Place from tblstartpoint where CompanyId='"+ CompanyId + "'", null);
            var transportdetails = BaseDbServices.Instance.GetData("select * from tbltransportation where CompanyId='" + CompanyId + "'", null);
            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblfeedetails where FeeStructureName='Extra' and CompanyId='"+ CompanyId + "'", null);
            List<DataTable> lst = new List<DataTable>()
            {
                studentdetails,
                transportdetails,
                extrafee,
                pickUpPoint
            };
            return Ok(lst);
        }

        [Route("GetAllStudentListByClass"), HttpGet]
        public IHttpActionResult GetAllStudentListByClass()
        {
            var data = HttpContext.Current.Request;
            var faculty = data["Faculty"];
            var _class = data["Class"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var studentdetails = StudentTransportDbServices.Instance.GetAllStudentListByClass(faculty,_class,CompanyId);
            var pickUpPoint = BaseDbServices.Instance.GetData("select Place from tblstartpoint where CompanyId='"+CompanyId+"'", null);
            var transportdetails = BaseDbServices.Instance.GetData("select * from tbltransportation where CompanyId='" + CompanyId + "'", null);
            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblfeedetails where FeeStructureName='Extra' and CompanyId='"+ CompanyId + "'", null);
            List<DataTable> lst = new List<DataTable>()
            {
                studentdetails,
                transportdetails,
                extrafee,
                pickUpPoint
            };
            return Ok(lst);
        }

        [Route("GetAllExtraFeeDetails"),HttpGet]
        public IHttpActionResult GetAllExtraFeeDetails()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblfeedetails where FeeStructureName='Extra' and CompanyId='"+CompanyId+"'", null);
            return Ok(extrafee);
        }

        [Route("GetAllStudentListBySection"), HttpGet]
        public IHttpActionResult GetAllStudentListBySection()
        {
            var data = HttpContext.Current.Request;
            var faculty = data["Faculty"];
            var _class = data["Class"];
            var section = data["Section"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var studentdetails = StudentTransportDbServices.Instance.GetAllStudentListBySection(faculty, _class,section,CompanyId);
            var transportdetails = BaseDbServices.Instance.GetData("select * from tbltransportation where CompanyId='"+CompanyId+"'", null);
            var pickUpPoint = BaseDbServices.Instance.GetData("select Place from tblstartpoint where CompanyId='" + CompanyId + "'", null);
            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblfeedetails where FeeStructureName='Extra' and CompanyId='"+CompanyId+"'", null);
            List<DataTable> lst = new List<DataTable>()
            {
                studentdetails,
                transportdetails,
                extrafee,
                pickUpPoint
            };
            return Ok(lst);
        }

        [Route("GetAllStudentList"),HttpGet]
        public IHttpActionResult GetAllStudentList()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var studentdetails = StudentTransportDbServices.Instance.GetAllStudentList(CompanyId);
            var transportdetails = BaseDbServices.Instance.GetData("select * from tbltransportation where CompanyId='"+CompanyId+"'", null);
            var pickUpPoint = BaseDbServices.Instance.GetData("select Place from tblstartpoint where CompanyId='" + CompanyId + "'", null);
            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblfeedetails where FeeStructureName='Extra' and CompanyId='"+ CompanyId + "'", null);
            List<DataTable> lst = new List<DataTable>()
            {
                studentdetails,
                transportdetails,
                extrafee,
                pickUpPoint
            };
            return Ok(lst);
        }

        [Route("GetTransportAmount"),HttpGet]
        public IHttpActionResult GetTransportAmount()
        {
            var data = HttpContext.Current.Request;
            var to = data["To"];
            var way = data["Way"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> details = new List<MySqlParameter>()
            {
                new MySqlParameter("@to",to),
                new MySqlParameter("@way",way),
                new MySqlParameter("@CompanyId",CompanyId),
            };
            DataTable amount = new DataTable();
            if(way == "OneWay")
            {
                amount = BaseDbServices.Instance.GetData("select OneWayAmount from tbltransportation where PlaceTo=@to and CompanyId=@CompanyId", details);
            }
            else if(way == "TwoWay")
            {
                amount = BaseDbServices.Instance.GetData("select TwoWayAmount from tbltransportation where PlaceTo=@to and CompanyId=@CompanyId", details);
            }
            amount.Columns[0].ColumnName = "Amount";
            
            return Ok(amount);
        }

        [Route("GetAllScholarshipDetails"), HttpGet]
        public IHttpActionResult GetAllScholarshipDetails()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var _class = data["Class"];
            var faculty = data["Faculty"];
            var scholarshipName = data["Name"];
            var CompanyId = data["CompanyId"];

            List<MySqlParameter> Fee = new List<MySqlParameter>()
            {
                new MySqlParameter("@batch", batch),
                new MySqlParameter("@class",_class),
                new MySqlParameter("@faculty",faculty),
                new MySqlParameter("@name",scholarshipName),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var id = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@batch and Faculty=@faculty and Class=@class and CompanyId=@CompanyId", Fee);
            var ids = id.Rows[0]["BatchClassId"];
            var FeeDetails = new DataTable();
            List<DataTable> lst = new List<DataTable>();
            var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId=@CompanyId", Fee);
            var first = FeeStructure.Rows.Count;
            for (int i = 0; i < FeeStructure.Rows.Count; i++)
            {
                if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == "Extra")
                { }
                else
                {
                    FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + id.Rows[0]["BatchClassId"] + "' and CompanyId=@CompanyId", Fee);
                    FeeDetails.Columns.Add("IsChecked");
                    FeeDetails.Columns.Add("DiscountType");
                    FeeDetails.Columns.Add("Discount");
                    lst.Add(FeeDetails);
                    var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblscholarshipdetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Batch=@batch and Class=@class and Faculty=@faculty and ScholarshipName=@name and CompanyId=@CompanyId", Fee);
                    for (int k = 0; k < FeeDetails.Rows.Count; k++)
                    {
                        for (int j = 0; j < SelectedFee.Rows.Count; j++)
                        {

                            if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString())
                            {
                                FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
                                FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
                                FeeDetails.Rows[k]["IsChecked"] = true;


                            }
                            else
                            {

                            }


                        }
                    }
                }
            }
            return Ok(lst);
        }

        [Route("GetStudentExtraFee"),HttpGet]
        public IHttpActionResult GetStudentExtraFee()
        {
            var data = HttpContext.Current.Request;
            var Id = data["Id"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Info = new List<MySqlParameter>()
            {
                new MySqlParameter("@Id", Id),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var feedetails = BaseDbServices.Instance.GetData("select FeeName,Amount from tblfeedetails where FeeStructureName='Extra' and IsDeleted='0' and CompanyId=@CompanyId", Info);
            feedetails.Columns.Add("IsChecked");
            var selectedextrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblstudentextrafeedetails where StudentExtraFeeId=@Id and CompanyId=@CompanyId", Info);
            for(int k = 0; k < feedetails.Rows.Count;k++)
            {
                for(int j = 0; j < selectedextrafee.Rows.Count;j++)
                {
                    if(feedetails.Rows[k]["FeeName"].ToString() == selectedextrafee.Rows[j]["FeeName"].ToString())
                    {
                        feedetails.Rows[k]["Amount"] = selectedextrafee.Rows[j]["Amount"];
                        feedetails.Rows[k]["IsChecked"] = true;
                    }
                    else
                    {

                    }
                }
            }
            return Ok(feedetails);
        }

        [Route("GetSpecialScholarshipDetails"), HttpGet]
        public IHttpActionResult GetSpecialScholarshipDetails()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var _class = data["Class"];
            var faculty = data["Faculty"];
            var StudentId = data["Id"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Detail = new List<MySqlParameter>()
            {
                new MySqlParameter("@batch", batch),
                new MySqlParameter("@class",_class),
                new MySqlParameter("@faculty",faculty),
                new MySqlParameter("@id",StudentId),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            var StudentType = BaseDbServices.Instance.GetData("Select StudentType from tblstudentInfo where StudentId=@id and CompanyId=@CompanyId", Detail);
            var type = StudentType.Rows[0]["StudentType"];
            var id = BaseDbServices.Instance.GetData("Select BatchClassId from tblbatchclass where Batch=@batch and Faculty=@faculty and Class=@class and CompanyId=@CompanyId", Detail);
            var ids = id.Rows[0]["BatchClassId"];
            var FeeDetails = new DataTable();
            List<DataTable> lst = new List<DataTable>();
            var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where CompanyId=@CompanyId", Detail);
            var first = FeeStructure.Rows.Count;
            for (int i = 0; i < FeeStructure.Rows.Count; i++)
            {
                if (FeeStructure.Rows[i]["FeeStructureName"].ToString() == "Extra")
                {

                }
                else
                {
                    if (type.ToString() == "General")
                    {
                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=" + id.Rows[0]["BatchClassId"] + " and StudentType='" + type + "' and CompanyId=@CompanyId ", Detail);
                        FeeDetails.Columns.Add("IsChecked");
                        FeeDetails.Columns.Add("DiscountType");
                        FeeDetails.Columns.Add("Discount");
                        lst.Add(FeeDetails);
                        var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Batch=@batch and Class=@class and Faculty=@faculty and StudentId=@id and CompanyId=@CompanyId", Detail);
                        for (int k = 0; k < FeeDetails.Rows.Count; k++)
                        {
                            for (int j = 0; j < SelectedFee.Rows.Count; j++)
                            {

                                if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString())
                                {
                                    FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
                                    FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
                                    FeeDetails.Rows[k]["IsChecked"] = true;
                                    
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                    else if (type.ToString() == "Day-Boarder")
                    {
                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=" + id.Rows[0]["BatchClassId"] + " and (StudentType='" + type + "' or StudentType='General') and CompanyId=@CompanyId", Detail);
                        FeeDetails.Columns.Add("IsChecked");
                        FeeDetails.Columns.Add("DiscountType");
                        FeeDetails.Columns.Add("Discount");
                        lst.Add(FeeDetails);
                        var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Batch=@batch and Class=@class and Faculty=@faculty and StudentId=@id and CompanyId=@CompanyId", Detail);
                        for (int k = 0; k < FeeDetails.Rows.Count; k++)
                        {
                            for (int j = 0; j < SelectedFee.Rows.Count; j++)
                            {

                                if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString())
                                {
                                    FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
                                    FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
                                    FeeDetails.Rows[k]["IsChecked"] = true;
                                    
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                    else if (type.ToString() == "Boarder")
                    {
                        FeeDetails = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId='" + id.Rows[0]["BatchClassId"] + "' and (StudentType='" + type + "' or StudentType='General') and CompanyId=@CompanyId", Detail);
                        FeeDetails.Columns.Add("IsChecked");
                        FeeDetails.Columns.Add("DiscountType");
                        FeeDetails.Columns.Add("Discount");
                        lst.Add(FeeDetails);
                        var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "'  and Batch=@batch and Class=@class and Faculty=@faculty and StudentId=@id and CompanyId=@CompanyId", Detail);
                        for (int k = 0; k < FeeDetails.Rows.Count; k++)
                        {
                            for (int j = 0; j < SelectedFee.Rows.Count; j++)
                            {

                                if (FeeDetails.Rows[k]["FeeId"].ToString() == SelectedFee.Rows[j]["FeeId"].ToString())
                                {
                                    FeeDetails.Rows[k]["DiscountType"] = SelectedFee.Rows[j]["DiscountType"];
                                    FeeDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
                                    FeeDetails.Rows[k]["IsChecked"] = true;


                                }
                                else
                                {

                                }


                            }
                        }
                    }


                }
            }
            //for (int i = 0; i < FeeStructure.Rows.Count; i++)
            //{
            //    ScholarshipDetails = BaseDbServices.Instance.GetData("Select * from tblfeedetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and IsDeleted =0", null);
            //    ScholarshipDetails.Columns.Add("IsChecked");
            //    ScholarshipDetails.Columns.Add("Discount");
            //    lst.Add(ScholarshipDetails);
            //    var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblspecialscholarship where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and StudentId=@id",Id);
            //    for (int k = 0; k < ScholarshipDetails.Rows.Count; k++)
            //    {
            //        for (int j = 0; j < SelectedFee.Rows.Count; j++)
            //        {

            //            if (ScholarshipDetails.Rows[k]["FeeName"].ToString() == SelectedFee.Rows[j]["FeeName"].ToString() && ScholarshipDetails.Rows[k]["FeeStructureName"].ToString() == SelectedFee.Rows[j]["FeeStructureName"].ToString())
            //            {
            //                ScholarshipDetails.Rows[k]["Discount"] = SelectedFee.Rows[j]["Discount"];
            //                ScholarshipDetails.Rows[k]["IsChecked"] = true;


            //            }
            //            else
            //            {

            //            }


            //        }
            //    }
            //}
            return Ok(lst);
        }
        [Route("GetClassAllFeeDetails")]
        public IHttpActionResult GetClassAllFee()
        {
            var data = HttpContext.Current.Request;
            var BatchClassId = Convert.ToInt32(data["BatchClassId"]);
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",BatchClassId),
                new MySqlParameter("@companyid",CompanyId)
            };
            var FeeDetails = new DataTable();
            List<DataTable> lst = new List<DataTable>();

            var FeeStructure = BaseDbServices.Instance.GetData("Select FeeStructureName from tblfeestructuredetails where FeeStructureName !='Extra' and CompanyId=@companyid", Id);
            var first = FeeStructure.Rows.Count;
            for (int i = 0; i < FeeStructure.Rows.Count; i++)
            {
                FeeDetails = BaseDbServices.Instance.GetData("Select * from tblfeedetails where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and IsDeleted =0 and CompanyId=@companyid",Id);
                FeeDetails.Columns.Add("IsChecked");
                lst.Add(FeeDetails);
                var SelectedFee = BaseDbServices.Instance.GetData("Select * from tblbatchclassfee where FeeStructureName='" + FeeStructure.Rows[i]["FeeStructureName"] + "' and BatchClassId=@id",Id);
                for (int k = 0; k < FeeDetails.Rows.Count; k++)
                {
                    for (int j = 0; j < SelectedFee.Rows.Count; j++)
                    {

                        if (FeeDetails.Rows[k]["FeeStructureName"].ToString() == SelectedFee.Rows[j]["FeeStructureName"].ToString() &&
                            FeeDetails.Rows[k]["FeeName"].ToString() == SelectedFee.Rows[j]["FeeName"].ToString() &&
                            FeeDetails.Rows[k]["StudentType"].ToString() == SelectedFee.Rows[j]["StudentType"].ToString())
                        {
                            FeeDetails.Rows[k]["Amount"] = SelectedFee.Rows[j]["Amount"];
                            FeeDetails.Rows[k]["FeeId"] = SelectedFee.Rows[j]["FeeId"];
                            FeeDetails.Rows[k]["IsChecked"] = true;
                        }
                        else
                        {

                        }
                    }
                }
            }
            return Ok(lst);
        }

        [Route("GetUserList"), HttpGet]
        public IHttpActionResult GetUserList()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = BaseDbServices.Instance.GetData("Select * from tbluserdetail where CompanyId='"+ CompanyId + "'",null);
            return Ok(details);
        }

        [Route("GetUserName")]
        public IHttpActionResult GetUserName()
        {
            var data = HttpContext.Current.Request;
            var Email = data["Email"];
            List<MySqlParameter> User = new List<MySqlParameter>()
            {
                new MySqlParameter("@user",Email)
            };
            var userid = BaseDbServices.Instance.GetData("select UserID from tbluserdetail where Email=@user",User);
            return Ok(userid);
        }


        [Route("GetStudentInfo"),HttpGet]
        public IHttpActionResult GetStudentInfo()
        {
            var data = HttpContext.Current.Request;
            var id = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",id),
                new MySqlParameter("@companyid",CompanyId)
            };
            var studentInfo = BaseDbServices.Instance.GetData("Select * from tblstudentinfo where StudentId=@id and CompanyId=@companyid and IsDeleted=0 ",Id);
            var pastEducation = BaseDbServices.Instance.GetData("Select * from tblpasteducation where StudentId=@id and CompanyId=@companyid and IsDeleted=0 ", Id);
            var currentEducation = BaseDbServices.Instance.GetData("Select * from tblcurrenteducation where StudentId=@id and CompanyId=@companyid and IsDeleted=0 ", Id);
            var currentAdrress = BaseDbServices.Instance.GetData("Select * from tblcurrentaddress where StudentId=@id and CompanyId=@companyid and IsDeleted=0 ", Id);
            var permanentAddress = BaseDbServices.Instance.GetData("Select * from tblpermanentaddress where StudentId=@id and CompanyId=@companyid and IsDeleted=0 ", Id);
            var emergencyContact = BaseDbServices.Instance.GetData("Select * from tblemergencycontact where StudentId=@id and CompanyId=@companyid and IsDeleted=0 ", Id);
            var scholarship = BaseDbServices.Instance.GetData("Select * from tblscholarship where StudentId=@id and CompanyId=@companyid and IsDeleted=0 ", Id);

            var filename = "doc_";
            filename += id + "_" + CompanyId + "_*";
            List<string> docs = new List<string>();
            var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/student/documents");
            DirectoryInfo folder = new DirectoryInfo(filePath);
            if (folder.Exists)
            {
                FileInfo[] files = folder.GetFiles(filename);
                foreach (var file in files)
                {
                    docs.Add(file.Name);
                }
            }

            List<DataTable> Info = new List<DataTable>
            {
                studentInfo,
                pastEducation,
                currentEducation,
                currentAdrress,
                permanentAddress,
                emergencyContact,
                scholarship
            };
            return Ok(Info);
        }


        [Route("GetTeacherInfo"),HttpGet]
        public IHttpActionResult GetTeacherInfo()
        {
            var data = HttpContext.Current.Request;
            var StaffId = data["StaffId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",StaffId),
                new MySqlParameter("@CompanyId",CompanyId)

            };
            var teacherInfo = BaseDbServices.Instance.GetData("Select * from tblteacherinfo where TeacherId=@id and IsDeleted=0 and CompanyId=@CompanyId",Id);
            var education = BaseDbServices.Instance.GetData("Select * from tbleducation where TeacherId=@id and IsDeleted=0 and CompanyId=@CompanyId ", Id);
            var experience = BaseDbServices.Instance.GetData("Select * from tblexperience where TeacherId=@id and IsDeleted=0 and CompanyId=@CompanyId ", Id);
            var currentAdrress = BaseDbServices.Instance.GetData("Select * from tblteachercurrentaddress where TeacherId=@id and IsDeleted=0 and CompanyId=@CompanyId ", Id);
            var permanentAddress = BaseDbServices.Instance.GetData("Select * from tblteacherpermanentaddress where TeacherId=@id and IsDeleted=0 and CompanyId=@CompanyId ", Id);
           
            var filename = "doc_";
            filename += StaffId + "_*";
            List<string> docs = new List<string>();
            var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/teacher/documents");
            DirectoryInfo folder = new DirectoryInfo(filePath);
            if (folder.Exists)
            {
                FileInfo[] files = folder.GetFiles(filename);
                foreach (var file in files)
                {
                    docs.Add(file.Name);
                }
            }

            List<DataTable> Info = new List<DataTable>
            {
                teacherInfo,
                education,
                experience,
                currentAdrress,
                permanentAddress
            };
            return Ok(Info);
        }

        [Route("GetAllStudentTransport"),HttpGet]
        public IHttpActionResult GetAllStudentTransport()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = StudentTransportDbServices.Instance.GetAllStudentTransport(CompanyId);
            return Ok(details);
        }

        [Route("GetAllStudentExtraFee"), HttpGet]
        public IHttpActionResult GetAllStudentExtraFee()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = StudentExtraFeeDbServices.Instance.GetAllStudentExtraFee(CompanyId);
            return Ok(details);
        }
        [Route("GetCompanyDetailsById/{id}"),HttpGet]
        public IHttpActionResult GetCompanyDetailsById(int id)
        {
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",id)
            };
            var details = CompanyDbServices.Instance.GetCompanyDetailsById(id);
            return Ok(details);
        }

        [Route("GetuserDetails/{id}"),HttpGet]
        public IHttpActionResult GetuserDetails(int id )
        {
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",id)
            };
            var details = BaseDbServices.Instance.GetData("select * from tbluserdetail where UserID=@id",Id);
            return Ok(details);
        }

        [Route("GetStudentDocument/{id}")]
        public IHttpActionResult GetStudentDocument(string id)
        {
            var filename = "doc_";
            filename += id + "_*";
            List<string> docs = new List<string>();
            var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/student/documents");
            DirectoryInfo folder = new DirectoryInfo(filePath);
            if (folder.Exists)
            {
                FileInfo[] files = folder.GetFiles(filename);
                foreach (var file in files)
                {
                    docs.Add(file.Name);
                }
            }
            return Ok(docs);
        }

        [Route("GetTeacherDocument/{id}")]
        public IHttpActionResult GetTeacherDocument(int id)
        {
            var filename = "doc_";
            filename += id + "_*";
            List<string> docs = new List<string>();
            var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/teacher/documents");
            DirectoryInfo folder = new DirectoryInfo(filePath);
            if (folder.Exists)
            {
                FileInfo[] files = folder.GetFiles(filename);
                foreach (var file in files)
                {
                    docs.Add(file.Name);
                }
            }
            return Ok(docs);
        }

        [Route("DeleteTeacher"),HttpPost]
        public IHttpActionResult DeleteTeacher()
        {
            var data = HttpContext.Current.Request;
            var StaffId = data["StaffId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",StaffId),
                new MySqlParameter("@CompanyId",CompanyId)
            };
            BaseDbServices.Instance.RunQuery("Update tbleducation set IsDeleted=1 where TeacherId=@id and CompanyId=@CompanyId",Id);
            BaseDbServices.Instance.RunQuery("Update tblexperience set IsDeleted=1 where TeacherId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Update tblteachercurrentaddress set IsDeleted=1 where TeacherId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Update tblteacherpermanentaddress set IsDeleted=1 where TeacherId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Delete from tblstaffattendance where TeacherId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Update tblteacherinfo set IsDeleted=1 where TeacherId=@id and CompanyId=@CompanyId", Id);
            return Ok("");
        }
        

        [Route("DeleteStudent"), HttpPost]
        public IHttpActionResult DeleteStudent()
        {
            var data = HttpContext.Current.Request;
            var id = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<MySqlParameter> Id = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",id),
                new MySqlParameter("@CompanyId",CompanyId)
            };

            BaseDbServices.Instance.RunQuery("Update tblpasteducation set IsDeleted=1 where StudentId=@id and CompanyId=@CompanyId",Id);
            BaseDbServices.Instance.RunQuery("Update tblcurrenteducation set IsDeleted=1 where StudentId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Update tblcurrentaddress set IsDeleted=1 where StudentId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Update tblpermanentaddress set IsDeleted=1 where StudentId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Update tblemergencycontact set IsDeleted=1 where StudentId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Update tblscholarship set IsDeleted=1 where StudentId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Update tblstudentinfo set IsDeleted=1 where StudentId=@id and CompanyId=@CompanyId", Id);
            BaseDbServices.Instance.RunQuery("Delete from tblspecialscholarship where StudentId=@id and CompanyId=@CompanyId", Id);
            return Ok("");
        }

        [Route("DeleteDocument"), HttpPost]
        public IHttpActionResult DeleteDocument()
        {
            var data = HttpContext.Current.Request;
            var filename = data["filename"];
            var filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/student/documents"), filename);
            FileInfo f = new FileInfo(filePath);
            f.Delete();
            return Ok(true);
        }
        [Route("DeleteTeacherDocument"), HttpPost]
        public IHttpActionResult DeleteTeacherDocument()
        {
            var data = HttpContext.Current.Request;
            var filename = data["filename"];
            var filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/teacher/documents"), filename);
            FileInfo f = new FileInfo(filePath);
            f.Delete();
            return Ok(true);
        }

        //[Route("ExportReport"), HttpGet]
        //public IHttpActionResult ExportReport()
        //{
        //    DataTable data;
        //    data = BaseDbServices.Instance.GetData("select tblstudentinfo.*,tblcurrenteducation.Class,tblcurrenteducation.Section,tblcurrenteducation.Faculty from tblstudentinfo " +
        //        "inner join tblcurrenteducation on tblstudentinfo.StudentId=tblcurrenteducation.StudentId " +
        //        "where tblstudentinfo.IsDeleted=0 and tblcurrenteducation.IsDeleted=0", null);
        //    removeUnwantedColumns(ref data, "pdfFile");
        //    var title = "Student Details";
          //  return Ok(Utilities.ExportToPdf(data, title));

        //}

        [Route("GetAllStudentListByName"),HttpGet]
        public IHttpActionResult GetAllStudentListByName()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Faculty = data["Faculty"];
            var Section = data["Section"];
            var Name = data["Name"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var studentdetails = StudentTransportDbServices.Instance.GetAllStudentListByName(Batch,Faculty,Class,Section,Name,CompanyId);
            var pickUpPoint = BaseDbServices.Instance.GetData("select Place from tblstartpoint where CompanyId='"+CompanyId+"'", null);
            var transportdetails = BaseDbServices.Instance.GetData("select * from tbltransportation where CompanyId='"+ CompanyId + "'", null);
            var extrafee = BaseDbServices.Instance.GetData("select FeeName,Amount from tblfeedetails where FeeStructureName='Extra' and CompanyId='"+ CompanyId + "'", null);
            List<DataTable> lst = new List<DataTable>()
            {
                studentdetails,
                transportdetails,
                extrafee,
                pickUpPoint
            };
            return Ok(lst);
        }

        [Route("GetSearchedDayOff"),HttpGet]
        public IHttpActionResult GetSearchedDayOff()
        {
            var data = HttpContext.Current.Request;
            var batch = data["Batch"];
            var department = data["Department"];
            var _class = data["Class"];
            var faculty = data["Faculty"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            var details = GeneralSettingDbServices.Instance.GetSearchedDayOff(batch, department, faculty, _class,CompanyId);
            details.Columns.Add("TotalDays", typeof(Int32));
            for (int i = 0; i < details.Rows.Count; i++)
            {
                DateTime d1 = Convert.ToDateTime(details.Rows[i]["DateFrom"]);
                DateTime d2 = Convert.ToDateTime(details.Rows[i]["DateTo"]);
                TimeSpan span = d2 - d1;
                int counter = span.Days + 1;
                details.Rows[i]["TotalDays"] = counter;
            }
        
            return Ok(details);
    }

        [Route("SearchFeeDetails"),HttpGet]
        public IHttpActionResult SearchFeeDetails()
        {
            var data = HttpContext.Current.Request;
            var FeeName = data["FeeName"];
            var Type = data["Type"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var result = GeneralSettingDbServices.Instance.GetSearchedFee(FeeName, Type,CompanyId);
            return Ok(result);
        }

        [Route("SearchCompanyDetails"),HttpGet]
        public IHttpActionResult SearchCompanyDetails()
        {
            var data = HttpContext.Current.Request;
            var Name = data["Name"];
            var Email = data["Email"];
            var Status = data["Status"];
            var result = CompanyDbServices.Instance.GetSearchedCompany(Name, Email, Status);
            return Ok(result);
        }

        [Route("SearchStudent"), HttpGet]
        public IHttpActionResult SearchStudent()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Section = data["Section"];
            var Name = data["Name"];
            var Type = data["Type"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var result = StudentDBServices.Instance.GetSearchedStudent(Batch, Class, Section, Name,Type,CompanyId);
            //
           
            //var filename = Utilities.ExportToPdf(result, title);
            var res = new
            {
                searchResult = result,
               // reportPath = filename
            };
            return Ok(res);
        }

        [Route("GetFacultyByClass"),HttpGet]
        public IHttpActionResult GetFacultyByClass()
        {
            var data = HttpContext.Current.Request;
            var Class = data["Class"];
            var CompanyId = data["CompanyId"];
            List<MySqlParameter> Info = new List<MySqlParameter>()
            {
                new MySqlParameter("@class", Class),
                new MySqlParameter("@companyid", CompanyId)
            };
            var facultyId = BaseDbServices.Instance.GetData("select FacultyId from tblclassdetails where ClassName=@class and CompanyId=@companyid", Info);
            var faculty = BaseDbServices.Instance.GetData("select FacultyName from tblfacultydetails where FacultyId='" + facultyId.Rows[0]["FacultyId"] + "' and CompanyId=@companyid", Info);
            return Ok(faculty);
        }

        [Route("SearchClassFee"), HttpGet]
        public IHttpActionResult SearchClassFee()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Faculty = data["Faculty"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            var result = GeneralSettingDbServices.Instance.GetSearchedClassFee(Batch, Class, Faculty,CompanyId);

            return Ok(result);
        }

        [Route("GetAttendanceDetails"),HttpGet]
        public IHttpActionResult GetAttendanceDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var StudentId = data["StudentId"];
            var DateFrom = data["DateFrom"];
            var DateTo = data["DateTo"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            var details = StudentDBServices.Instance.GetAttendanceDetails(Batch, StudentId, DateFrom, DateTo,CompanyId);
            return Ok(details);
        }

        [Route("GetStaffAttendanceDetails"), HttpGet]
        public IHttpActionResult GetStaffAttendanceDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var TeacherId = data["TeacherId"];
            var DateFrom = data["DateFrom"];
            var DateTo = data["DateTo"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = TeacherDbServices.Instance.GetAttendanceDetails(Batch, TeacherId, DateFrom, DateTo,CompanyId);
            return Ok(details);
        }

        [Route("GetAllStaffAttendance"),HttpGet]
        public IHttpActionResult GetAllStaffAttendance()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var batchdetails = BaseDbServices.Instance.GetData("select FromYear,ToYear from tblbatchdetails where Status='Active' and CompanyId='"+CompanyId+"'", null);
            if (batchdetails.Rows.Count > 0)
            {
                var batch = batchdetails.Rows[0]["FromYear"] + "-" + batchdetails.Rows[0]["ToYear"];
                int Year = Convert.ToInt32(DateTime.Now.Year);
                int Month = Convert.ToInt32(DateTime.Now.Month);
                int Day = Convert.ToInt32(DateTime.Now.Day);
                var convertedRawNepaliDate = new DateConverter().EngToNep(Year, Month, Day);
                var nepaliDate = convertedRawNepaliDate.ConvertedDate.Year + "-" + convertedRawNepaliDate.ConvertedDate.Month + "-" + convertedRawNepaliDate.ConvertedDate.Day;
                var startDate = BaseDbServices.Instance.GetData("select Date from tblstartdate where Batch='" + batch + "' and CompanyId='"+CompanyId+"'");
                if (startDate.Rows.Count > 0)
                {
                    var date = startDate.Rows[0]["Date"].ToString();
                    var presentAttendance = TeacherDbServices.Instance.GetAllPresentAttendance(batch, date, nepaliDate,CompanyId);
                    var absentAttendance = TeacherDbServices.Instance.GetAllAbsentAttendance(batch, date, nepaliDate, CompanyId);
                    presentAttendance.Columns.Add("Absent", typeof(int));
                    presentAttendance.Columns.Add("TotalWorking", typeof(int));
                    List<string> Info = new List<string>();
                    var bb = absentAttendance.AsEnumerable();
                    foreach (var item in bb)
                    {
                        Info.Add(item[0].ToString());
                    }
                    for (int i = 0; i < presentAttendance.Rows.Count; i++)
                    {
                        if (Info.Contains(presentAttendance.Rows[i]["TeacherId"].ToString()))
                        {
                            var absent = absentAttendance.AsEnumerable().FirstOrDefault(R => R["TeacherId"].ToString() == presentAttendance.Rows[i]["TeacherId"].ToString())["Absent"];
                            presentAttendance.Rows[i]["Absent"] = absent;
                        }
                        else
                        {
                            presentAttendance.Rows[i]["Absent"] = 0;
                        }
                    }
                    for (int j = 0; j < presentAttendance.Rows.Count; j++)
                    {
                        DataTable holiday = new DataTable();

                        if (presentAttendance.Rows[j]["Department"].ToString() == "Teacher")
                        {
                            holiday = TeacherDbServices.Instance.GetTotalTeacherHolidays(batch, "Teacher", presentAttendance.Rows[j]["Faculty"].ToString(), date, nepaliDate,CompanyId);
                        }
                        else
                        {
                            holiday = TeacherDbServices.Instance.GetTotalHolidays(batch, presentAttendance.Rows[j]["Department"].ToString(), date, nepaliDate,CompanyId);
                        }
                        // var holiday = StudentDBServices.Instance.GetTotalHolidays(Batch, presentAttendance.Rows[j]["Class"].ToString(), DateFrom, DateTo);
                        DateTime d1 = Convert.ToDateTime(date);
                        DateTime d2 = Convert.ToDateTime(nepaliDate);
                        TimeSpan span = d2 - d1;
                        int counter = span.Days + 1;
                        int totalworking = 0;
                        var totalHoliday = holiday.Rows[0]["Holiday"].ToString();
                        if (totalHoliday != "")
                        {
                            totalworking = counter - Convert.ToInt32(holiday.Rows[0]["Holiday"]);

                        }
                        else
                        {
                            totalworking = counter - 0;
                        }
                        presentAttendance.Rows[j]["TotalWorking"] = totalworking;
                    }
                    return Ok(new
                    {
                        presentAttendance,
                        date,
                        nepaliDate
                    });
                }
                else
                {
                    return Ok("");
                }
            }
            else
            {
                return Ok("");
            }

        }

        [Route("GetAllAttendance"),HttpGet]
        public IHttpActionResult GetAllAttendance()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var batchdetails = BaseDbServices.Instance.GetData("select FromYear,ToYear from tblbatchdetails where Status='Active' and CompanyId='"+CompanyId+"'", null);
            if (batchdetails.Rows.Count > 0)
            {
                var batch = batchdetails.Rows[0]["FromYear"] + "-" + batchdetails.Rows[0]["ToYear"];
                int Year = Convert.ToInt32(DateTime.Now.Year);
                int Month = Convert.ToInt32(DateTime.Now.Month);
                int Day = Convert.ToInt32(DateTime.Now.Day);
                var convertedRawNepaliDate = new DateConverter().EngToNep(Year, Month, Day);
                var nepaliDate = convertedRawNepaliDate.ConvertedDate.Year + "-" + convertedRawNepaliDate.ConvertedDate.Month + "-" + convertedRawNepaliDate.ConvertedDate.Day;
                var startDate = BaseDbServices.Instance.GetData("select Date from tblstartdate where Batch='" + batch + "' and CompanyId='"+CompanyId+"'",null);
                if (startDate.Rows.Count > 0)
                {
                    var date = startDate.Rows[0]["Date"].ToString();
                    var presentAttendance = StudentDBServices.Instance.GetAllPresentAttendance(batch, date, nepaliDate,CompanyId);
                    var absentAttendance = StudentDBServices.Instance.GetAllAbsentAttendance(batch, date, nepaliDate,CompanyId);
                    presentAttendance.Columns.Add("Absent", typeof(int));
                    presentAttendance.Columns.Add("TotalWorking", typeof(int));
                    List<string> Info = new List<string>();
                    var bb = absentAttendance.AsEnumerable();
                    foreach (var item in bb)
                    {
                        Info.Add(item[0].ToString());
                    }
                    for (int i = 0; i < presentAttendance.Rows.Count; i++)
                    {
                        if (Info.Contains(presentAttendance.Rows[i]["StudentId"]))
                        {
                            var absent = absentAttendance.AsEnumerable().FirstOrDefault(R => R["StudentId"].ToString() == presentAttendance.Rows[i]["StudentId"].ToString())["Absent"];
                            presentAttendance.Rows[i]["Absent"] = absent;
                        }
                        else
                        {
                            presentAttendance.Rows[i]["Absent"] = 0;
                        }
                    }
                    for (int j = 0; j < presentAttendance.Rows.Count; j++)
                    {
                        var holiday = StudentDBServices.Instance.GetTotalHolidays(batch, presentAttendance.Rows[j]["Class"].ToString(), date, nepaliDate,CompanyId);
                        DateTime d1 = Convert.ToDateTime(date);
                        DateTime d2 = Convert.ToDateTime(nepaliDate);
                        TimeSpan span = d2 - d1;
                        int counter = span.Days + 1;
                        int totalworking = 0;
                        var totalHoliday = holiday.Rows[0]["Holiday"].ToString();
                        if (totalHoliday != "")
                        {
                            totalworking = counter - Convert.ToInt32(holiday.Rows[0]["Holiday"]);

                        }
                        else
                        {
                            totalworking = counter - 0;
                        }
                        presentAttendance.Rows[j]["TotalWorking"] = totalworking;
                    }

                    return Ok(new
                    {
                        presentAttendance,
                        date,
                        nepaliDate
                    });
                }
                else
                {
                    return Ok("");
                }
            }
            else
            {
                return Ok("");
            }
        }

        [Route("SearchAttendanceDetails"),HttpGet]
        public IHttpActionResult SearchAttendanceDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Section = data["Section"];
            var DateFrom = data["DateFrom"];
            var DateTo = data["DateTo"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            var presentAttendance = StudentDBServices.Instance.GetPresentAttendance(Batch, Class, Section,DateFrom,DateTo,CompanyId);
            var absentAttendance = StudentDBServices.Instance.GetAbsentAttendance(Batch, Class, Section, DateFrom, DateTo,CompanyId);
            presentAttendance.Columns.Add("Absent", typeof(int));
            presentAttendance.Columns.Add("TotalWorking", typeof(int));
            List<string> Info = new List<string>();
            var bb = absentAttendance.AsEnumerable();
            foreach (var item in bb)
            {
                Info.Add(item[0].ToString());
            }
            for(int i =0; i < presentAttendance.Rows.Count;i++)
            {
                if(Info.Contains(presentAttendance.Rows[i]["StudentId"]))
                {
                    var absent = absentAttendance.AsEnumerable().FirstOrDefault(R => R["StudentId"].ToString() == presentAttendance.Rows[i]["StudentId"].ToString())["Absent"];
                    presentAttendance.Rows[i]["Absent"] = absent;
                }
                else
                {
                    presentAttendance.Rows[i]["Absent"] = 0;
                }
            }
            for(int j =0; j <presentAttendance.Rows.Count;j++)
            {
                var holiday = StudentDBServices.Instance.GetTotalHolidays(Batch, presentAttendance.Rows[j]["Class"].ToString(), DateFrom, DateTo,CompanyId);
                    DateTime d1 = Convert.ToDateTime(DateFrom);
                    DateTime d2 = Convert.ToDateTime(DateTo);
                    TimeSpan span = d2 - d1;
                    int counter = span.Days + 1;
                int totalworking = 0;
                var totalHoliday = holiday.Rows[0]["Holiday"].ToString();
                if (totalHoliday != "")
                {
                    totalworking = counter - Convert.ToInt32(holiday.Rows[0]["Holiday"]);
                    
                }
                else
                {
                     totalworking = counter - 0;
                }
                presentAttendance.Rows[j]["TotalWorking"] = totalworking;
            }
            return Ok(presentAttendance);
        }

        [Route("SearchStaffAttendanceDetails"), HttpGet]
        public IHttpActionResult SearchStaffAttendanceDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Department = data["Department"];
            var Designation = data["Designation"];
            var DateFrom = data["DateFrom"];
            var DateTo = data["DateTo"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            var presentAttendance = TeacherDbServices.Instance.GetPresentAttendance(Batch, Department, Designation, DateFrom, DateTo,CompanyId);
            var absentAttendance = TeacherDbServices.Instance.GetAbsentAttendance(Batch, Department, Designation, DateFrom, DateTo, CompanyId);
            presentAttendance.Columns.Add("Absent", typeof(int));
            presentAttendance.Columns.Add("TotalWorking", typeof(int));
            List<string> Info = new List<string>();
            var bb = absentAttendance.AsEnumerable();
            foreach (var item in bb)
            {
                Info.Add(item[0].ToString());
            }
            for (int i = 0; i < presentAttendance.Rows.Count; i++)
            {
                if (Info.Contains(presentAttendance.Rows[i]["TeacherId"].ToString()))
                {
                    var absent = absentAttendance.AsEnumerable().FirstOrDefault(R => R["TeacherId"].ToString() == presentAttendance.Rows[i]["TeacherId"].ToString())["Absent"];
                    presentAttendance.Rows[i]["Absent"] = absent;
                }
                else
                {
                    presentAttendance.Rows[i]["Absent"] = 0;
                }
            }
            for (int j = 0; j < presentAttendance.Rows.Count; j++)
            {
                DataTable holiday = new DataTable();
                
              if(presentAttendance.Rows[j]["Department"].ToString() == "Teacher")
                {
                    holiday = TeacherDbServices.Instance.GetTotalTeacherHolidays(Batch, "Teacher", presentAttendance.Rows[j]["Faculty"].ToString(), DateFrom, DateTo,CompanyId);
                }
                else
                {
                    holiday = TeacherDbServices.Instance.GetTotalHolidays(Batch,presentAttendance.Rows[j]["Department"].ToString(), DateFrom, DateTo,CompanyId);
                }
               // var holiday = StudentDBServices.Instance.GetTotalHolidays(Batch, presentAttendance.Rows[j]["Class"].ToString(), DateFrom, DateTo);
                DateTime d1 = Convert.ToDateTime(DateFrom);
                DateTime d2 = Convert.ToDateTime(DateTo);
                TimeSpan span = d2 - d1;
                int counter = span.Days + 1;
                int totalworking = 0;
                var totalHoliday = holiday.Rows[0]["Holiday"].ToString();
                if (totalHoliday != "")
                {
                    totalworking = counter - Convert.ToInt32(holiday.Rows[0]["Holiday"]);

                }
                else
                {
                    totalworking = counter - 0;
                }
                presentAttendance.Rows[j]["TotalWorking"] = totalworking;
            }
            return Ok(presentAttendance);
        }

        [Route("SearchStudentExtraFeeDetails"),HttpGet]
        public IHttpActionResult SearchStudentExtraFeeDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Faculty = data["Faculty"];
            var Section = data["Section"];
            var StudentId = data["StudentId"];
            var Month = data["Month"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            var result = StudentExtraFeeDbServices.Instance.GetSearchedExtraFee(Batch, Class, Faculty, Section, StudentId, Month,CompanyId);
            return Ok(result);
        }

        [Route("SearchStudentTransportDetails"),HttpGet]
        public IHttpActionResult SearchStudentTransportDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Faculty = data["Faculty"];
            var Section = data["Section"];
            var StudentId = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            var result = StudentTransportDbServices.Instance.GetSearchedTransport(Batch, Class, Faculty, Section, StudentId,CompanyId);
            return Ok(result);

        }

        [Route("SearchBillingDetails"), HttpGet]
        public IHttpActionResult SearchBillingDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Month = data["Month"];
            var Status = data["Status"];
            var StudentId = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var result = BillingDbServices.Instance.GetSearchedBilling(Batch, Class, Month, Status, StudentId,CompanyId);
            return Ok(result);
        }

        [Route("SearchDueReport"), HttpGet]
        public IHttpActionResult SearchDueReport()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Month = data["Month"];
            var StudentId = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            if(StudentId != "")
            {
                var result = DueDbService.Instance.GetSearchedDueReportByStudentId(StudentId,CompanyId);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    var info = DueDbService.Instance.GetPartialDueByStudentId(result.Rows[i]["StudentId"].ToString(), result.Rows[i]["Batch"].ToString(),CompanyId);
                    if (info.Rows.Count > 0)
                    {
                        var totalbillings = BaseDbServices.Instance.GetData("select sum(Amount) total from tblbillingdetails " +
                           " inner join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId " +
                           " where tblbilling.StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and tblbilling.Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblbilling.CompanyId='" + CompanyId+"' ", null);
                        var partialdue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as Dis,sum(Fine) as fine from tblreceipt where StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblreceipt.CompanyId='" + CompanyId + "'", null);
                        var total = Convert.ToInt32(totalbillings.Rows[0]["total"]) + Convert.ToInt32(partialdue.Rows[0]["fine"]) - Convert.ToInt32(partialdue.Rows[0]["dis"]);
                        var due = total - Convert.ToInt32(partialdue.Rows[0]["paid"]);

                        result.Rows[i]["due"] = due;
                    }
                    else
                    {

                    }

                }
                if (result.Rows.Count != 0)
                {
                    return Ok(result.AsEnumerable().Where(R => Convert.ToInt32(R["Due"]) != 0).CopyToDataTable());
                }
                else
                {
                    return Ok(result);
                }
            }

            else if (Class == "" && Month == "")
            {
                var result = DueDbService.Instance.GetSearchedDueReportByBatch(Batch,CompanyId);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    var info = DueDbService.Instance.GetPartialDueByStudentId(result.Rows[i]["StudentId"].ToString(), result.Rows[i]["Batch"].ToString(),CompanyId);
                    if (info.Rows.Count > 0)
                    {
                        var totalbillings = BaseDbServices.Instance.GetData("select sum(Amount) total from tblbillingdetails " +
                          " inner join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId " +
                          " where tblbilling.StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and tblbilling.Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblbilling.CompanyId='" + CompanyId + "' ", null);
                        var partialdue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as Dis,sum(Fine) as fine from tblreceipt where StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblreceipt.CompanyId='" + CompanyId + "'", null);
                        var total = Convert.ToInt32(totalbillings.Rows[0]["total"]) + Convert.ToInt32(partialdue.Rows[0]["fine"]) - Convert.ToInt32(partialdue.Rows[0]["dis"]);
                        var due = total - Convert.ToInt32(partialdue.Rows[0]["paid"]);

                        result.Rows[i]["due"] = due;
                    }
                    else
                    {

                    }

                }
                if (result.Rows.Count != 0)
                {
                    return Ok(result.AsEnumerable().Where(R => Convert.ToInt32(R["Due"]) != 0).CopyToDataTable());
                }
                else
                {
                    return Ok(result);
                }
            }
            else if(Batch =="" && Class =="")
            {
                var result = DueDbService.Instance.GetSearchedDueReportByMonth(Month,CompanyId);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (Convert.ToInt32(result.Rows[i]["Due"]) != 0)
                    {

                    }
                    else
                    {
                        var info = DueDbService.Instance.GetPartialDueByStudentId(result.Rows[i]["StudentId"].ToString(), result.Rows[i]["Batch"].ToString(),CompanyId);
                        if (info.Rows.Count > 0)
                        {
                            var totalbillings = BaseDbServices.Instance.GetData("select sum(Amount) as total from tblbillingdetails " +
                          " inner join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId " +
                          " where tblbilling.StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and tblbilling.Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblbilling.CompanyId='"+CompanyId+"' ", null);
                            var partialdue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as Dis,sum(Fine) as fine from tblreceipt where StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblreceipt.CompanyId='"+CompanyId+"'", null);
                            var total = Convert.ToInt32(totalbillings.Rows[0]["total"]) + Convert.ToInt32(partialdue.Rows[0]["fine"]) - Convert.ToInt32(partialdue.Rows[0]["dis"]);
                            var due = total - Convert.ToInt32(partialdue.Rows[0]["paid"]);

                            result.Rows[i]["due"] = due;
                        }
                        else
                        {

                        }
                    }

                }
                if (result.Rows.Count != 0)
                {
                    return Ok(result.AsEnumerable().Where(R => Convert.ToInt32(R["Due"]) != 0).CopyToDataTable());
                }
                else
                {
                    return Ok(result);
                }
            }
            else if (Batch == "" && Month == "")
            {
                var result = DueDbService.Instance.GetSearchedDueReportByClass(Class,CompanyId);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    var info = DueDbService.Instance.GetPartialDueByStudentId(result.Rows[i]["StudentId"].ToString(), result.Rows[i]["Batch"].ToString(),CompanyId);
                    if (info.Rows.Count > 0)
                    {
                        var totalbillings = BaseDbServices.Instance.GetData("select sum(Amount) total from tblbillingdetails " +
                          " inner join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId " +
                          " where tblbilling.StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and tblbilling.Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblbilling.CompanyId='"+CompanyId+"' ", null);
                        var partialdue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as Dis,sum(Fine) as fine from tblreceipt where StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblreceipt.CompanyId='"+CompanyId+"'", null);
                        var total = Convert.ToInt32(totalbillings.Rows[0]["total"]) + Convert.ToInt32(partialdue.Rows[0]["fine"]) - Convert.ToInt32(partialdue.Rows[0]["dis"]);
                        var due = total - Convert.ToInt32(partialdue.Rows[0]["paid"]);

                        result.Rows[i]["due"] = due;
                    }
                    else
                    {

                    }

                }
                if (result.Rows.Count != 0)
                {
                    return Ok(result.AsEnumerable().Where(R => Convert.ToInt32(R["Due"]) != 0).CopyToDataTable());
                }
                else
                {
                    return Ok(result);
                }
            }
            else if (Month == "")
            {
                var result = DueDbService.Instance.GetSearchedDueReportByBatchClass(Batch,Class,CompanyId);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    var info = DueDbService.Instance.GetPartialDueByStudentId(result.Rows[i]["StudentId"].ToString(), result.Rows[i]["Batch"].ToString(),CompanyId);
                    if (info.Rows.Count > 0)
                    {
                        var totalbillings = BaseDbServices.Instance.GetData("select sum(Amount) total from tblbillingdetails " +
                          " inner join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId " +
                          " where tblbilling.StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and tblbilling.Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblbilling.CompanyId='"+CompanyId+"' ", null);
                        var partialdue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as Dis,sum(Fine) as fine from tblreceipt where StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblreceipt.CompanyId='"+CompanyId+"'", null);
                        var total = Convert.ToInt32(totalbillings.Rows[0]["total"]) + Convert.ToInt32(partialdue.Rows[0]["fine"]) - Convert.ToInt32(partialdue.Rows[0]["dis"]);
                        var due = total - Convert.ToInt32(partialdue.Rows[0]["paid"]);

                        result.Rows[i]["due"] =  due;
                    }
                    else
                    {

                    }

                }
                if (result.Rows.Count != 0)
                {
                    return Ok(result.AsEnumerable().Where(R => Convert.ToInt32(R["Due"]) != 0).CopyToDataTable());
                }
                else
                {
                    return Ok(result);
                }
            }
            else if (Class == "")
            {
                var result = DueDbService.Instance.GetSearchedDueReportByBatchMonth(Batch, Month,CompanyId);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (Convert.ToInt32(result.Rows[i]["Due"]) != 0)
                    {

                    }
                    else
                    {
                        var info = DueDbService.Instance.GetPartialDueByStudentId(result.Rows[i]["StudentId"].ToString(), result.Rows[i]["Batch"].ToString(),CompanyId);
                        if (info.Rows.Count > 0)
                        {
                            var totalbillings = BaseDbServices.Instance.GetData("select sum(Amount) total from tblbillingdetails " +
                          " inner join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId " +
                          " where tblbilling.StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and tblbilling.Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblbilling.CompanyId='"+CompanyId+"' ", null);
                            var partialdue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as Dis,sum(Fine) as fine from tblreceipt where StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and Batch='" + result.Rows[i]["Batch"].ToString() + "' and CompanyId='"+CompanyId+"' ", null);
                            var total = Convert.ToInt32(totalbillings.Rows[0]["total"]) + Convert.ToInt32(partialdue.Rows[0]["fine"]) - Convert.ToInt32(partialdue.Rows[0]["dis"]);
                            var due = total - Convert.ToInt32(partialdue.Rows[0]["paid"]);

                            result.Rows[i]["due"] = due;
                        }
                        else
                        {

                        }
                    }

                }
                if (result.Rows.Count != 0)
                {
                    return Ok(result.AsEnumerable().Where(R => Convert.ToInt32(R["Due"]) != 0).CopyToDataTable());
                }
                else
                {
                    return Ok(result);
                }
            }

            else if (Batch == "")
            {
                var result = DueDbService.Instance.GetSearchedDueReportByClassMonth(Class, Month,CompanyId);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (Convert.ToInt32(result.Rows[i]["Due"]) != 0)
                    {

                    }
                    else
                    {
                        var info = DueDbService.Instance.GetPartialDueByStudentId(result.Rows[i]["StudentId"].ToString(), result.Rows[i]["Batch"].ToString(),CompanyId);
                        if (info.Rows.Count > 0)
                        {
                            var totalbillings = BaseDbServices.Instance.GetData("select sum(Amount) total from tblbillingdetails " +
                           " inner join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId " +
                           " where tblbilling.StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and tblbilling.Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblbilling.CompanyId='"+CompanyId+"' ", null);
                            var partialdue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as Dis,sum(Fine) as fine from tblreceipt where StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and Batch='" + result.Rows[i]["Batch"].ToString() + "' and CompanyId='"+CompanyId+"'", null);
                            var total = Convert.ToInt32(totalbillings.Rows[0]["total"]) + Convert.ToInt32(partialdue.Rows[0]["fine"]) - Convert.ToInt32(partialdue.Rows[0]["dis"]);
                            var due = total - Convert.ToInt32(partialdue.Rows[0]["paid"]);

                            result.Rows[i]["due"] =  due;
                        }
                        else
                        {

                        }
                    }

                }
                if (result.Rows.Count != 0)
                {
                    return Ok(result.AsEnumerable().Where(R => Convert.ToInt32(R["Due"]) != 0).CopyToDataTable());
                }
                else
                {
                    return Ok(result);
                }
            }
            else
            {
                var result = DueDbService.Instance.GetSearchedDueReportByBatchClassMonth(Batch,Class, Month,CompanyId);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (Convert.ToInt32(result.Rows[i]["Due"]) != 0)
                    {

                    }
                    else
                    {
                        var info = DueDbService.Instance.GetPartialDueByStudentId(result.Rows[i]["StudentId"].ToString(), result.Rows[i]["Batch"].ToString(),CompanyId);
                        if (info.Rows.Count > 0)
                        {

                            var totalbillings = BaseDbServices.Instance.GetData("select sum(Amount) total from tblbillingdetails " +
                          " inner join tblbilling on tblbilling.BillingId=tblbillingdetails.BillingId " +
                          " where tblbilling.StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and tblbilling.Batch='" + result.Rows[i]["Batch"].ToString() + "' and tblbilling.CompanyId='"+CompanyId+"' ", null);
                            var partialdue = BaseDbServices.Instance.GetData("select sum(PaidAmount) as paid,sum(Discount) as Dis,sum(Fine) as fine from tblreceipt where StudentId='" + result.Rows[i]["StudentId"].ToString() + "' and Batch='" + result.Rows[i]["Batch"].ToString() + "' and CompanyId='"+CompanyId+"'", null);
                            var total = Convert.ToInt32(totalbillings.Rows[0]["total"]) + Convert.ToInt32(partialdue.Rows[0]["fine"]) - Convert.ToInt32(partialdue.Rows[0]["dis"]);
                            var due = total - Convert.ToInt32(partialdue.Rows[0]["paid"]);

                            result.Rows[i]["due"] =  due;
                        }
                        else
                        {

                        }
                    }
                }
                if (result.Rows.Count != 0)
                {
                    return Ok(result.AsEnumerable().Where(R => Convert.ToInt32(R["Due"]) != 0).CopyToDataTable());
                }
                else
                {
                    return Ok(result);
                }
            }
            //else
            //{

            //    var result = DueDbService.Instance.GetSearchedDueReport(Batch, Class, Month, StudentId);
            //    if (result.Rows.Count != 0)
            //    { 
            //        return Ok(result.AsEnumerable().Where(R => Convert.ToInt32(R["due"]) != 0).CopyToDataTable());
            //    }
            //    else
            //    {
            //        return Ok(result);
            //    }
            //}
            
        }

        [Route("SearchReceiptDetails"), HttpGet]
        public IHttpActionResult SearchReceiptDetails()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var Month = data["Month"];
            var StudentId = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var details = ReceiptDbServices.Instance.GetSearchedReceipt(Batch, Class, Month, StudentId,CompanyId);
            
            return Ok(details);
            }
        

        [Route("SearchSpecialScholarship"), HttpGet]
        public IHttpActionResult SearchSpecialScholarship()
        {
            var data = HttpContext.Current.Request;
            var Batch = data["Batch"];
            var Class = data["Class"];
            var StudentId = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            //var Month = data["Month"];

            var result = ScholarshipDbServices.Instance.GetSearchedScholarship(Batch, Class, StudentId,CompanyId);
           // removeUnwantedColumns(ref result, "SearchedScholarship");
            return Ok(result);
        }

        [Route("GetBillingReceiptTotal"), HttpGet]
        public IHttpActionResult GetBillingReceiptTotal()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var activeBatch = BaseDbServices.Instance.GetData("select FromYear,ToYear from tblbatchdetails where Status='Active' and CompanyId='"+ CompanyId + "'", null);
            if (activeBatch.Rows.Count > 0)
            {
                var batch = activeBatch.Rows[0]["FromYear"] + "-" + activeBatch.Rows[0]["ToYear"];
                var totalBilling = BaseDbServices.Instance.GetTotalPaid(batch,CompanyId);
                totalBilling.Columns.RemoveAt(2);
                totalBilling.Columns.RemoveAt(3);
                totalBilling.Columns.RemoveAt(4);
                totalBilling.Columns.Add("Due");
                return Ok(totalBilling);
            }
            else
            {
                return Ok("");
            }
        }

        [Route("GetBatchBillingReceiptTotal"), HttpGet]
        public IHttpActionResult GetBatchBillingReceiptTotal()
        {
            //var data = HttpContext.Current.Request;
            //var CompanyId = data["CompanyId"];
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            var totalBatchBilling = BaseDbServices.Instance.GetBatchTotalBill(CompanyId);
            totalBatchBilling.Columns.RemoveAt(2);
            totalBatchBilling.Columns.RemoveAt(3);
            totalBatchBilling.Columns.RemoveAt(4);
            totalBatchBilling.Columns.Add("Due");
            return Ok(totalBatchBilling);
        }

        [Route("PostStartPoint"),HttpPost]
        public IHttpActionResult PostStartPoint([FromBody] StartPoint obj)
        {
            StudentTransportDbServices.Instance.SaveStartPoint(obj);
            return Ok("");
        }

        [Route("GetDashboardData"), HttpGet]
        public IHttpActionResult GetDashboardData()
        {
            var data = HttpContext.Current.Request;
            var CompanyId = Convert.ToInt32(data["CompanyId"]);
            List<DataTable> aggTotal = BaseDbServices.Instance.GetAggregateTotal(CompanyId,"Class", "Faculty", "Batch");
            List<DataTable> aggBillingSum = new List<DataTable>();
            List<DataTable> aggReceiptSum = new List<DataTable>();
            
            var total = BaseDbServices.Instance.GetData($"SELECT COUNT(*) AS total FROM tblstudentinfo where IsDeleted=0 and Status='Active' and CompanyId='"+CompanyId+"'");
            var male = BaseDbServices.Instance.GetData($"select count(*) as total from tblstudentinfo group by Gender,IsDeleted,Status,CompanyId having Gender='Male' and IsDeleted=0 and Status='Active' and CompanyId='" + CompanyId + "'");
            var female = BaseDbServices.Instance.GetData($"select count(*) as total from tblstudentinfo group by Gender,IsDeleted,Status,CompanyId having Gender='Female' and IsDeleted=0 and Status='Active' and CompanyId='" + CompanyId + "'");
            //var billingSum = BaseDbServices.Instance.GetBillingSum();
            //var receiptSum = BaseDbServices.Instance.GetReceiptSum();

            //aggBillingSum.Add(billingSum);
            //aggReceiptSum.Add(receiptSum);
             
            return Ok(new
            {
                aggTotal,
                total,
                male ,
                female,
            });
        }
        [Route("getAdvancedPaid"), HttpGet]
        public IHttpActionResult getAdvancedPaid()
        {
            var data = HttpContext.Current.Request;
            var StudentId = data["StudentId"];
            var CompanyId = Convert.ToInt32(data["CompanyId"]);

            var advanced = BaseDbServices.Instance.GetData("select * from tbladvancedpaid where StudentId='" + StudentId + "' and Status='Not-Used' and CompanyId='"+CompanyId+"'", null);
            return Ok(advanced);
        }
    }
}
