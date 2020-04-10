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
    public class StudentExtraFeeDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private StudentExtraFeeDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static StudentExtraFeeDbServices Instance { get { return new StudentExtraFeeDbServices(); } }

        public void SaveStudentExtraFee(StudentExtraFee obj,string StudentId)
        {
            try
            {
                List<MySqlParameter> Id = new List<MySqlParameter>()
                        {
                            new MySqlParameter("@Id",StudentId),
                            new MySqlParameter("@batch", obj.Batch),
                            new MySqlParameter("@month",obj.Month),
                            new MySqlParameter("@CompanyId",obj.CompanyId)
                        };

                var checkdetail = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where StudentId=@Id and Batch=@batch and Month=@month and CompanyId=@CompanyId", Id);
                        if (checkdetail.Rows.Count > 0)
                        {
                            BaseDbServices.Instance.RunQuery("delete from tblstudentextrafeedetails where StudentExtraFeeId='" + checkdetail.Rows[0]["StudentExtraFeeId"] + "'", null);
                            BaseDbServices.Instance.RunQuery("delete from tblstudentextrafee where StudentExtraFeeId='" + checkdetail.Rows[0]["StudentExtraFeeId"] + "'", null);
                        }
                        MySqlCommand cmd = new MySqlCommand("save_studentextrafee", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@StudentId", obj.StudentId);
                        cmd.Parameters.AddWithValue("@_batch", obj.Batch);
                        cmd.Parameters.AddWithValue("@_month", obj.Month);
                        cmd.Parameters.AddWithValue("@_studentid", StudentId);
                        cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                        var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                        returnid.Direction = ParameterDirection.Output;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        int id = Convert.ToInt32(returnid.Value);

                        foreach (var m in obj.ExtraFeeDetails)
                        {
                            foreach (var r in m.StudentExtraDetails)
                            {
                                r.StudentExtraFeeId = id;
                                r.CompanyId = obj.CompanyId;
                                if (r.IsChecked == true)
                                {
                                    SaveStudentExtraFeeDetails(r);
                                }
                            }
                        }
                    
                
            }
            catch (Exception ex)
            {

                conn.Close();
                conn.Dispose();
                throw;
            }
        }

        public void SaveStudentExtraFeeDetails(StudentExtraFee obj)
        {
            if (obj.StudentExtraFeeId != 0)
            {
                try {
                    List<MySqlParameter> Info = new List<MySqlParameter>()
                        {
                            new MySqlParameter("@Id",obj.StudentExtraFeeId),
                            new MySqlParameter("@CompanyId",obj.CompanyId)
                        };
                    BaseDbServices.Instance.RunQuery("delete from tblstudentextrafeedetails where StudentExtraFeeId=@Id and CompanyId=@CompanyId", Info);
                    BaseDbServices.Instance.RunQuery("delete from tblstudentextrafee where StudentExtraFeeId=@Id and CompanyId=@CompanyId", Info);
                    foreach (var m in obj.ExtraFeeDetails)
                    {
                        MySqlCommand cmd = new MySqlCommand("save_studentextrafee", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@StudentId", obj.StudentId);
                        cmd.Parameters.AddWithValue("@_batch", obj.Batch);
                        cmd.Parameters.AddWithValue("@_month", obj.Month);
                        cmd.Parameters.AddWithValue("@_studentid", m.StudentId);
                        cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                        var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                        returnid.Direction = ParameterDirection.Output;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        int id = Convert.ToInt32(returnid.Value);
                        foreach (var r in m.StudentExtraDetails)
                        {
                            r.StudentExtraFeeId = id;
                            r.CompanyId = obj.CompanyId;
                            if (r.IsChecked == true)
                            {
                                SaveStudentExtraFeeDetails(r);
                            }
                        }
                    }
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
                    foreach (var m in obj.ExtraFeeDetails)
                    {
                        if (m.IsChecked == true)
                        {
                            List<MySqlParameter> Id = new List<MySqlParameter>()
                        {
                            new MySqlParameter("@Id",m.StudentId),
                            new MySqlParameter("@batch", obj.Batch),
                            new MySqlParameter("@month",obj.Month),
                            new MySqlParameter("@CompanyId",obj.CompanyId)
                        };
                            var checkdetail = BaseDbServices.Instance.GetData("select StudentExtraFeeId from tblstudentextrafee where StudentId=@Id and Batch=@batch and Month=@month and CompanyId=@CompanyId", Id);
                            if (checkdetail.Rows.Count > 0)
                            {
                                BaseDbServices.Instance.RunQuery("delete from tblstudentextrafeedetails where StudentExtraFeeId='" + checkdetail.Rows[0]["StudentExtraFeeId"] + "'", null);
                                BaseDbServices.Instance.RunQuery("delete from tblstudentextrafee where StudentExtraFeeId='" + checkdetail.Rows[0]["StudentExtraFeeId"] + "'", null);
                            }
                            MySqlCommand cmd = new MySqlCommand("save_studentextrafee", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@StudentId", obj.StudentId);
                            cmd.Parameters.AddWithValue("@_batch", obj.Batch);
                            cmd.Parameters.AddWithValue("@_month", obj.Month);
                            cmd.Parameters.AddWithValue("@_studentid", m.StudentId);
                            cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                            var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                            returnid.Direction = ParameterDirection.Output;
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            int id = Convert.ToInt32(returnid.Value);

                            foreach (var r in m.StudentExtraDetails)
                            {
                                r.StudentExtraFeeId = id;
                                r.CompanyId = obj.CompanyId;
                                if (r.IsChecked == true)
                                {
                                    SaveStudentExtraFeeDetails(r);
                                }
                            }
                        }
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
        public void SaveStudentExtraFeeDetails(StudentExtraDetails obj)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand("save_studentextrafeedetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_studentextrafeeid", obj.StudentExtraFeeId);
                cmd.Parameters.AddWithValue("@_feename", obj.FeeName);
                cmd.Parameters.AddWithValue("@_amount", obj.Amount);
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

        public DataTable GetAllStudentExtraFee(int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentextrafee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetSearchedExtraFee(string Batch, string Class, string Faculty, string Section, string StudentId,string Month,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallsearchedextrafee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Faculty", Faculty);
                cmd.Parameters.AddWithValue("@_Section", Section);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_Month", Month);
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