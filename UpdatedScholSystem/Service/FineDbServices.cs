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
    public class FineDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private FineDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static FineDbServices Instance { get { return new FineDbServices(); } }

        public void PostFineDetails(Fine obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_finedetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_FineId", obj.FineId);
                cmd.Parameters.AddWithValue("@_DayFrom", obj.DayFrom);
                cmd.Parameters.AddWithValue("@_DayTo", obj.DayTo);
                cmd.Parameters.AddWithValue("@_FineType", obj.FineType);
                cmd.Parameters.AddWithValue("@_FineAmount", obj.FineAmount);
                cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
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