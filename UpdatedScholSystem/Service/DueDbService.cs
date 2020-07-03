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
    public class DueDbService
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private DueDbService()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static DueDbService Instance { get { return new DueDbService(); } }


        public DataTable GetAllDueDetails(string Batch,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getduelist", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
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

        public DataTable GetAllFollowUp(int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallfollowup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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
        public DataTable GetAllStudentListDue(int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlistdue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetAllStudentListDueByBatch(string Batch,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlistduebybatch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
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
        public DataTable TotalDue()
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("_totaldue", conn);
                cmd.CommandType = CommandType.StoredProcedure;

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
        public DataTable GetAllStudentListDueByStudentId(string StudentId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlistduebystudentid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetAllStudentListDueByClass(string Class,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlistduebyclass", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetAllStudentListDueByBatchClass(string Batch,string Class,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlistduebybatchclass", conn);
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
        public DataTable GetAllExpectedPayment()
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallexpectedpayment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetSearchedFollowUp( string fromDate,string toDate,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedfollowup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_FromDate", fromDate);
                cmd.Parameters.AddWithValue("@_ToDate", toDate);
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

        public DataTable GetDueDetailsByStudentId(string StudentId, string Batch,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getduedetailsbystudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_Batch", Batch);
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

        public DataTable GetPartialDueByStudentId(string StudentId, string Batch,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getpartialduedetailsbystudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_CompanyId",CompanyId);
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

        public DataTable GetSearchedDueReportByBatch(string Batch,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedduebybatch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
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

        public DataTable GetSearchedDueReportByStudentId(string StudentId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("_totaldue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetSearchedDueReportByMonth(string Month,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedduebymonth", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetSearchedDueReportByClass(string Class,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedduebyclass", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetSearchedDueReportByBatchClass(string Batch,string Class,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedduebybatchclass", conn);
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
        public DataTable GetSearchedDueReportByBatchMonth(string Batch, string Month,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedduebybatchmonth", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
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

        public DataTable GetSearchedDueReportByClassMonth(string Class, string Month,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedduebyclassmonth", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetSearchedDueReportByBatchClassMonth(string Batch,string Class, string Month,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedduebybatchclassmonth", conn);
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

        public DataTable GetSearchedExpectedDate(string fromDate, string toDate,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedexpectedpayment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_FromDate", fromDate);
                cmd.Parameters.AddWithValue("@_ToDate", toDate);
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

        public void SaveFollowUpDetails(FollowUp obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_followupdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
                cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                cmd.Parameters.AddWithValue("@_Response", obj.Response);
                cmd.Parameters.AddWithValue("@_FollowUpDate", obj.FollowUpDate);
                cmd.Parameters.AddWithValue("@_ExpectedDate", obj.ExpectedDate);
                cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
        }

        //public DataTable GetSearchedDueReport(string Batch, string Class, string Month,string StudentId)
        //{

        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("getsearchedduereport", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Batch", Batch);
        //        cmd.Parameters.AddWithValue("@Class", Class);
        //        cmd.Parameters.AddWithValue("@Month", Month);
        //        cmd.Parameters.AddWithValue("@StudentId", StudentId);
        //        adap.SelectCommand = cmd;
        //        conn.Open();
        //        adap.Fill(dt);
        //    }
        //    catch (Exception ex)

        //    {
        //        conn.Close();
        //        throw;
        //    }
        //    conn.Close();
        //    conn.Dispose();
        //    return dt;
        //}
    }
}