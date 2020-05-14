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
    public class UserAccessDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        public UserAccessDbServices()
        {
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }

        public static UserAccessDbServices Instance { get { return new UserAccessDbServices(); } }


        public void PostGroup(Group group)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_group", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ID", group.ID);
                cmd.Parameters.AddWithValue("@_UserRole", group.UserRole);
                cmd.Parameters.AddWithValue("@_Company_ID", group.Company_ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {

                conn.Close();
                conn.Dispose();
            }
        }


        public void PostUserControl(UserControl usercontrol)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_usercontrol", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Group_ID", usercontrol.Group_ID);
                cmd.Parameters.AddWithValue("@_Company_ID", usercontrol.Company_ID);
                cmd.Parameters.AddWithValue("@_Feature_ID", usercontrol.Feature_ID);
                cmd.Parameters.AddWithValue("@_Action_ID", usercontrol.Action_ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {

                conn.Close();
                conn.Dispose();
            }
        }
        public void PostFeature(Feature feature)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_feature", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ID", feature.ID);
                cmd.Parameters.AddWithValue("@_Name", feature.Name);
                cmd.Parameters.AddWithValue("@_Company_ID", feature.Company_ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {

                conn.Close();
                conn.Dispose();
            }
        }
        public void PostFeatureAction(FeatureAction featureAction)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_featureaction", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ID", featureAction.ID);
                cmd.Parameters.AddWithValue("@_Name", featureAction.Name);
                cmd.Parameters.AddWithValue("@_Company_ID", featureAction.Company_ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {

                conn.Close();
                conn.Dispose();
            }
        }
    }
}