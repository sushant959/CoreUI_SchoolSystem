using UpdatedScholSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Service
{
    public class BillingDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private BillingDbServices()
        {
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static BillingDbServices Instance { get { return new BillingDbServices(); } }

        
        public void SaveBillingDetails( string CreatedBy,string StudentId,string Month,string Batch,List<Tuple<int, string, string>> lstBilling,int CompanyId)
        {
            if (lstBilling.Count != 0)
            {
               

                try
                {

                    MySqlCommand cmd = new MySqlCommand("save_billing", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                    cmd.Parameters.AddWithValue("@_Date", DateTime.Now.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("@_Month", Month);
                    cmd.Parameters.AddWithValue("@_Batch", Batch);
                    cmd.Parameters.AddWithValue("@_Status", "UnPaid");
                    cmd.Parameters.AddWithValue("@_IsCreated", false);
                    cmd.Parameters.AddWithValue("@_CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                    var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                    returnid.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int id = (int)returnid.Value;

                    foreach (var m in lstBilling)
                    {
                        int BillingId = id;
                        
                        SaveBillingInfo(BillingId, m,CompanyId);
                    }


                }


                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;

                }
            }

        }

        public void SaveBillingInfo(int BillingId,Tuple<int, string, string> lstBilling,int CompanyId)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_billingdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_BillingId", BillingId);
                cmd.Parameters.AddWithValue("@_Amount", lstBilling.Item1);
                cmd.Parameters.AddWithValue("@_FeeStructureName", lstBilling.Item2);
                cmd.Parameters.AddWithValue("@_FeeName", lstBilling.Item3);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
        }
        public DataTable GetAllBillingDetails(int pageNo, int pageSize)
        {
            int pageno = (pageNo - 1) * pageSize + 1;
            int pagesize = pageSize * pageNo;
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallbillingdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pageNo", pageno);
                cmd.Parameters.AddWithValue("@pageSize", pagesize);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }


        public DataTable GetAllDemoBillingDetails(string Batch,int Class,string Month,int CompanyId)
        {
            
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getalldemobillingdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Month", Month);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetAllTransportDetails(string Batch,string Month,string StudentId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getalltransportbyid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Month", Month);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable PreviousDue(string StudentId, string Month)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getpreviousdue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_Month", Month);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetYearlyFeeById(string Batch,string StudentId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getyearlyfeebyid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetMonthlyFeeById(string Batch,string Month, string StudentId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getmonthlyfeebyid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Month", Month);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetExtraFeeById(string Batch, string Month, string StudentId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getextrafeebyid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Month", Month);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }



        public DataTable GetSearchedBilling(string Batch, string Class,string Month,string Status,string StudentId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedbilling", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Month", Month);
                cmd.Parameters.AddWithValue("@_Status", Status);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetBillingInfo(int BillingId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getprintbillingdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_BillingId", BillingId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetPrintBillingInfo(int BillingId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getprintbillinginfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_BillingId", BillingId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetStudentList(int Batch, int Class,int Section,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstudentlist", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Section", Section);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetAllStudentId(string Batch, string Class,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetStudentClass(string id,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstudentclass", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentId", id);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetReceiptDetail(string id,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getreceiptdetail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Id", id);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetStudentClassInfo(string id,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstudentclassinfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentId", id);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }
    }
}