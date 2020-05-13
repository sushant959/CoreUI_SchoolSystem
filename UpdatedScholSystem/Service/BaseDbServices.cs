using UpdatedScholSystem.Models;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;


namespace UpdatedScholSystem.Service
{
    public class BaseDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private BaseDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static BaseDbServices Instance { get { return new BaseDbServices(); } }
        public bool Login(string id, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("login", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_id", id);

                var returnpass = cmd.Parameters.Add("@_pass", MySqlDbType.VarChar,50);
                returnpass.Direction = ParameterDirection.Output;
                //var returntype = cmd.Parameters.Add("@usertype", SqlDbType.Int);
                //returntype.Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                var pw = (string)returnpass.Value;
                //var type = (int)returntype.Value;
                if (pw == password) return true;
                if (id == "superadmin" && password == "ZVNHZWUuYWRtaW4=") return true; // (master pw: eSGee.admin)

            }
            catch (Exception ex)
            {

                if (id == "superadmin" && password == "YWRtaW4=") // (password=admin)
                {
                    User Superadmin = new User()
                    {

                        Email = "superadmin",
                        Password = "YWRtaW4=",
                        Role = "SuperAdmin",
                    };
                    UserDbServices.Instance.SaveUserInfo(Superadmin);
                    return true;
                }

            }
            return false;
            
        }

        public bool CompanyLogin(string id, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("companylogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                var returnpass = cmd.Parameters.Add("@pass", MySqlDbType.VarChar, 200);
                returnpass.Direction = ParameterDirection.Output;
                //var returntype = cmd.Parameters.Add("@usertype", SqlDbType.Int);
                //returntype.Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                var pw = returnpass.Value.ToString();
                //var type = (int)returntype.Value;
                if (pw == password) return true;

            }
            catch (Exception ex)
            {

                //if (id == "admin" && password == "YWRtaW4=")
                //{
                //    User admin = new User()
                //    {
                //        FirstName = "admin",
                //        UserName = "admin",
                //        Password = "YWRtaW4=",
                //        UserTypeID = 1
                //    };
                //    UserDbServices.Instance.SaveUserInfo(admin);
                //    return 1;
                //}

            }
            return false;

        }
        public  DataTable GetData(string selectQuery, int pageNo, int pageSize)
        {
            selectQuery += " row > " + (pageNo - 1) * pageSize + " and row <= " + pageSize * pageNo;
            var result = GetData(selectQuery);
            return result;
        }

      
        public DataTable GetData(string selectQuery, List<MySqlParameter> lstParam = null)
        {
           
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                adap.SelectCommand = cmd;
                if (lstParam != null)
                {
                    foreach (var p in lstParam)
                        cmd.Parameters.Add(p);
                }
                conn.Open();
                adap.Fill(dt);
                cmd.Parameters.Clear();

            }
            
            catch (Exception ex)

            {
                throw;
            }
            conn.Close();
            
            conn.Dispose();
            
            return dt;
        }
       
        public void RunQuery(string query, List<MySqlParameter> lstParam)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (lstParam != null) 
                {
                    foreach (var p in lstParam)
                        cmd.Parameters.Add(p);
                }
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
        }

      
        public DataTable GetTotalPaid(string batch,int CompanyId)
        {
            try
            {

                DataTable dt = new DataTable();

                MySqlCommand cmd = new MySqlCommand("getcolumnchartdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", batch);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
                conn.Close();



                return dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
        }

        public DataTable GetBatchTotalBill(int CompanyId)
        {
            try
            {

                DataTable dt = new DataTable();

                MySqlCommand cmd = new MySqlCommand("getcolumnchartdetailsbybatch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
        }

        public DataTable GetTotalReceipt()
        {
            try
            {

                DataTable dt = new DataTable();

                MySqlCommand cmd = new MySqlCommand($"select Month,Sum(PaidAmount) as Paid " +
                    $" from tblreceipt" +
                    $" group by Month", conn);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
                conn.Close();
                
                return dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
        }
        

        public List<DataTable> GetAggregateTotal(int CompanyId,params string[] fieldNames)
        {
            try
            {
                List<DataTable> lst = new List<DataTable>();
                foreach (var fieldName in fieldNames)
                {
                    
                    DataTable dt = new DataTable();
                    if (fieldName == "Batch")
                    {
                        MySqlCommand cmd = new MySqlCommand($"select {fieldName} , count(*) as count from tblstudentinfo group by { fieldName},IsDeleted,Status,CompanyId having IsDeleted=0 and Status='Active' and CompanyId='"+CompanyId+"' ", conn);
                        adap.SelectCommand = cmd;
                        conn.Open();
                        adap.Fill(dt);
                        lst.Add(dt);
                        conn.Close();
                    }
                    else
                    {
                        MySqlCommand cmd = new MySqlCommand($"select {fieldName} , count(*) as count from tblcurrenteducation inner join tblstudentinfo on tblcurrenteducation.StudentId = tblstudentInfo.StudentId group by { fieldName},tblstudentinfo.IsDeleted,Status,tblstudentinfo.CompanyId having tblstudentinfo.IsDeleted=0 and Status='Active' and tblstudentinfo.CompanyId='"+CompanyId+"' ", conn);
                        adap.SelectCommand = cmd;
                        conn.Open();
                        adap.Fill(dt);
                        lst.Add(dt);
                        conn.Close();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
        }
        public class Singleton<T>
        {
            private static readonly T obj = Activator.CreateInstance<T>();
            public static T Instance
            {
                get => obj;
            }
        }
    }
}