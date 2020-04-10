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
    public class SuperAdminDbService
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private SuperAdminDbService()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static SuperAdminDbService Instance { get { return new SuperAdminDbService(); } }

        public void UpdateSuperAdmin(User obj)
        {
            try
            {
               
                MySqlCommand cmd = new MySqlCommand("updatesuperadmin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@_Email", obj.Email);
                cmd.Parameters.AddWithValue("@_Password", obj.Password);
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