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
    public class CompanyDbServices 
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private CompanyDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static CompanyDbServices Instance { get { return new CompanyDbServices(); } }

        public void SaveCompanyDetails(CompanyDetails obj)
        {
            if(obj.CompanyId !=0)
            {
                try
                {
                    obj.Status = obj.Status != null ? obj.Status : "Not-Approved";
                    MySqlCommand cmd = new MySqlCommand("save_companydetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@PAN", obj.PAN);
                    cmd.Parameters.AddWithValue("@PhoneNo", obj.PhoneNo);
                    cmd.Parameters.AddWithValue("@Address", obj.Address);
                    cmd.Parameters.AddWithValue("@Status", obj.Status);
                    cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                    var returnid = cmd.Parameters.Add("@id", MySqlDbType.Int32);
                    returnid.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int id = Convert.ToInt32(returnid.Value);
                    obj.Users.CompanyId = id;
                    SaveUserInfo(obj.Users);
                }
                catch(Exception ex)
                {

                }
            }
            else
            {

                try
                {
                    obj.Status = obj.Status != null ? obj.Status : "Not-Approved";
                    MySqlCommand cmd = new MySqlCommand("save_companydetails",conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    cmd.Parameters.AddWithValue("@_Name", obj.Name);
                    cmd.Parameters.AddWithValue("@_PAN", obj.PAN);
                    cmd.Parameters.AddWithValue("@_PhoneNo", obj.PhoneNo);
                    cmd.Parameters.AddWithValue("@_Address", obj.Address);
                    cmd.Parameters.AddWithValue("@_Status", obj.Status);
                    cmd.Parameters.AddWithValue("@_RegistrationDate", DateTime.Now);
                    var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                    returnid.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int id = Convert.ToInt32(returnid.Value);
                    obj.Users.CompanyId = id;
                    SaveUserInfo(obj.Users);
                }
                catch (Exception ex)
                {

                    conn.Close();
                    conn.Dispose();
                    throw;

                }
            }
        }
        public void SaveUserInfo(User obj)
        {
            if (obj.Password != null)
            {
                try
                {
                    byte[] byts = System.Text.Encoding.UTF8.GetBytes(obj.Password);
                    obj.Password = Convert.ToBase64String(byts);
                    MySqlCommand cmd = new MySqlCommand("save_userinfo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                    cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@Role", "Admin");
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

            else
            {
                try
                {
                    
                    MySqlCommand cmd = new MySqlCommand("save_userinfo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_UserID", obj.UserID);
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    cmd.Parameters.AddWithValue("@_Email", obj.Email);
                    cmd.Parameters.AddWithValue("@_Password", obj.Password);
                    cmd.Parameters.AddWithValue("@_Role", "Admin");
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

        public DataTable GetListOfCompany()
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallcompany", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch(Exception ex)
            {
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public  DataTable GetCompanyDetailsById(int id)
        {
            var dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getcompanydetailsbyid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CompanyId", id);
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

        public DataTable GetSearchedCompany(string Name,string Email,string Status)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SearchCompanyDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Name", Name);
                cmd.Parameters.AddWithValue("@_Email", Email);
                cmd.Parameters.AddWithValue("@_Status", Status);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch(Exception ex)
            {
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public string CheckCompanyStatus(string Email)
        {
            var status = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand("checkcompanystatus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Email", Email);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                   
                    status = rdr["Status"].ToString();

                }
                                  
                  conn.Close();
            }
            catch(Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
            return status;
            
        }
    }
}