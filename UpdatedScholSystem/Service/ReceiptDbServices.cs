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
    public class ReceiptDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private ReceiptDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static ReceiptDbServices Instance { get { return new ReceiptDbServices(); } }

        public int SaveReceiptDetails(Receipt obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_receiptdetail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ReceiptId", obj.ReceiptId);
                cmd.Parameters.AddWithValue("@_BillingId", obj.BillingId);
                cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
                cmd.Parameters.AddWithValue("@_TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@_Month", obj.Month);
                cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                cmd.Parameters.AddWithValue("@_PaidAmount", obj.PaidAmount);
                cmd.Parameters.AddWithValue("@_Fine", obj.Fine);
                cmd.Parameters.AddWithValue("@_Discount", obj.Discount);
                cmd.Parameters.AddWithValue("@_DueAmount", obj.DueAmount);
                cmd.Parameters.AddWithValue("@_Date", obj.Date);
                cmd.Parameters.AddWithValue("@_PaymentMethod", obj.PaymentMethod);
                cmd.Parameters.AddWithValue("@_BankName", obj.BankName);
                cmd.Parameters.AddWithValue("@_ChequeNo", obj.ChequeNo);
                cmd.Parameters.AddWithValue("@_CreatedBy", obj.CreatedBy);
                cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                returnid.Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                int id = (int)returnid.Value;
                return id;

            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                return 0;

            }
        }

        public DataTable GetAllReceiptDetails(string Batch, string Class, string Month,int CompanyId)
        {
           
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallreceiptdetails", conn);
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
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetSearchedReceipt(string Batch, string Class,string Month, string StudentId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedreceipt", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Month", Month);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetPrintReceiptInfo(int ReceiptId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getprintreceiptinfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ReceiptId", ReceiptId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }
        
        public void SaveAdvancedPaid(string StudentId,int Amount,int CompanyId,int ReceiptId)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_advancedpaid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentId",StudentId);
                cmd.Parameters.AddWithValue("@_Amount", Amount);
                cmd.Parameters.AddWithValue("@_Status", "Not-Used");
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                cmd.Parameters.AddWithValue("@_ReceiptId", ReceiptId);
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
    }
}