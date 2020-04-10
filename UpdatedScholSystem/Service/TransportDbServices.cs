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
    public class TransportDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private TransportDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static TransportDbServices Instance { get { return new TransportDbServices(); } }

        public void PostTransportDetails(Transportation obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_transportdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_TransportationId", obj.TransportationId);
                cmd.Parameters.AddWithValue("@_PlaceTo", obj.PlaceTo);
                cmd.Parameters.AddWithValue("@_DistanceFrom", obj.DistanceFrom);
                cmd.Parameters.AddWithValue("@_DistanceTo", obj.DistanceTo);
                cmd.Parameters.AddWithValue("@_OneWayAmount", obj.OneWayAmount);
                cmd.Parameters.AddWithValue("@_TwoWayAmount", obj.TwoWayAmount);
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