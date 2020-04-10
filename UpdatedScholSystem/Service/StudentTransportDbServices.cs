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
    public class StudentTransportDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private StudentTransportDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static StudentTransportDbServices Instance { get { return new StudentTransportDbServices(); } }

        public DataTable GetAllStudentList(int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlist", conn);
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

        public DataTable GetAllStudentTransport(int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudenttransport", conn);
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

        public DataTable GetSearchedTransport(string Batch,string Class,string Faculty,string Section,string StudentId,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallsearchedtransport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Faculty", Faculty);
                cmd.Parameters.AddWithValue("@_Section", Section);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
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

        public DataTable GetAllStudentListByFaculty(string Faculty,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlistbyfaculty", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Faculty", Faculty);
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

        public DataTable GetAllStudentListByClass(string Faculty,string Class,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlistbyclass", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Faculty", Faculty);
                cmd.Parameters.AddWithValue("@_Class", Class);
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

        public DataTable GetAllStudentListBySection(string Faculty, string Class,string Section,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlistbysection", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Faculty", Faculty);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Section", Section);
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

        public DataTable GetAllStudentListByName(string Batch,string Faculty, string Class, string Section,string Name,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudentlistbyname", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Faculty", Faculty);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Section", Section);
                cmd.Parameters.AddWithValue("@_Name", Name);
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
        public void EditStudentTransportDetails(int Id,string Batch,string PlaceTo,string Type,int Amount,int CompanyId)
        {
            try
            {
               
                MySqlCommand cmd = new MySqlCommand("edit_studentTransport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Id", Id);
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_PlaceTo",PlaceTo);
                cmd.Parameters.AddWithValue("@_Type",Type);
                cmd.Parameters.AddWithValue("@_Amount",Amount);
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

        public void SaveStartPoint(StartPoint obj)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand("save_startpoint", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Id", obj.PlaceId);
                cmd.Parameters.AddWithValue("@_Place", obj.Place);
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

        public void SaveStudentTransportDetails(StudentTransport obj)
        {

            try
            {
                foreach (var m in obj.StudentTransportDetails)
                {
                    if (m.IsChecked == true)
                    {
                        List<MySqlParameter> Id = new List<MySqlParameter>()
                            {
                                new MySqlParameter("@id",m.StudentId),
                                new MySqlParameter("@batch",obj.Batch),
                                new MySqlParameter("@companyid",obj.CompanyId)
                            };
                        var check = BaseDbServices.Instance.GetData("select * from tblstudenttransport where Batch=@batch and StudentId=@id and CompanyId=@companyid", Id);
                        if(check.Rows.Count > 0)
                        {
                            BaseDbServices.Instance.RunQuery("delete from tblstudenttransport where Batch=@batch and StudentId=@id and CompanyId=@companyid", Id);
                        }
                        MySqlCommand cmd = new MySqlCommand("save_studentTransport", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                        cmd.Parameters.AddWithValue("@_StudentId", m.StudentId);
                        cmd.Parameters.AddWithValue("@_PlaceTo", m.PlaceTo);
                        cmd.Parameters.AddWithValue("@_Type", m.Type);
                        cmd.Parameters.AddWithValue("@_Amount", m.Amount);
                        cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {

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
}