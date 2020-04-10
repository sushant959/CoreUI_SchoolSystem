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
    public class DayOffDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private DayOffDbServices()
        {
           
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static DayOffDbServices Instance { get { return new DayOffDbServices(); } }

        public int SaveDayOff(DayOff obj)
        {
            if (obj.DayOffId == 0)
            {
                try
                {

                    MySqlCommand cmd = new MySqlCommand("save_dayoff", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_DayOffId", obj.DayOffId);
                    cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                    cmd.Parameters.AddWithValue("@_Title", obj.Title);
                    cmd.Parameters.AddWithValue("@_DateFrom", obj.DateFrom);
                    cmd.Parameters.AddWithValue("@_DateTo", obj.DateTo);
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                    returnid.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int id = Convert.ToInt32(returnid.Value);
                    return id;
                }
                catch (Exception ex)
                {

                    conn.Close();
                    conn.Dispose();
                    return 0;

                }
            }
            else
            {
                try
                {

                    MySqlCommand cmd = new MySqlCommand("save_dayoff", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_DayOffId", obj.DayOffId);
                    cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                    cmd.Parameters.AddWithValue("@_Title", obj.Title);
                    cmd.Parameters.AddWithValue("@_DateFrom", obj.DateFrom);
                    cmd.Parameters.AddWithValue("@_DateTo", obj.DateTo);
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                    returnid.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int id = Convert.ToInt32(returnid.Value);
                    return id;
                  
                }
                catch (Exception ex)
                {

                    conn.Close();
                    conn.Dispose();
                    return 0;

                }

            }

        }
        public void SaveDayOffDetails(int DayOffId,string Department,string Faculty,string Class,int CompanyId)
            {
             try
             {
                MySqlCommand cmd = new MySqlCommand("save_dayoffdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_DayOffId",DayOffId);
                cmd.Parameters.AddWithValue("@_Department", Department);
                cmd.Parameters.AddWithValue("@_Faculty", Faculty);
                cmd.Parameters.AddWithValue("@_Class", Class);
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
        public void EditDayOffDetails(DayOff obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_dayoffdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_DayOffId",obj.DayOffId);
                cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                cmd.Parameters.AddWithValue("@_Title", obj.Title);
                cmd.Parameters.AddWithValue("@_DateFrom",obj. DateFrom);
                cmd.Parameters.AddWithValue("@_DateTo", obj.DateTo);
                cmd.Parameters.AddWithValue("@_Department", obj.Department);
                cmd.Parameters.AddWithValue("@_Faculty", obj.Faculty);
                cmd.Parameters.AddWithValue("@_Class", obj.Class);
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