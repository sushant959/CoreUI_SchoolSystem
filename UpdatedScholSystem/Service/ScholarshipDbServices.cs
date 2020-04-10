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
    public class ScholarshipDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private ScholarshipDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static ScholarshipDbServices Instance { get { return new ScholarshipDbServices(); } }

        public void SaveScholarshipName(Scholarship obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_scholarshipname", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ScholarshipId", obj.Scholarshipid);
                cmd.Parameters.AddWithValue("@_ScholarshipName", obj.ScholarshipName);
                cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw;

            }
        }

        public void SaveDiscountDetails(ScholarshipDetails obj)
        {

            try
            {
                foreach (var m in obj.ScholarshipDiscount)
                {
                    if (m.IsChecked == true)
                    {
                        MySqlCommand cmd = new MySqlCommand("save_discountdetails", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@_Id", obj.Id);
                        cmd.Parameters.AddWithValue("@_ScholarshipName", obj.ScholarshipName);
                        cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                        cmd.Parameters.AddWithValue("@_Class", obj.Class);
                        cmd.Parameters.AddWithValue("@_Faculty", obj.Faculty);
                        cmd.Parameters.AddWithValue("@_DiscountType", m.DiscountType);
                        cmd.Parameters.AddWithValue("@_Discount", m.Discount);
                        cmd.Parameters.AddWithValue("@_FeeId", m.FeeId);
                        cmd.Parameters.AddWithValue("@_StudentType", m.StudentType);
                        cmd.Parameters.AddWithValue("@_FeeStructureName", m.FeeStructureName);
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

        public void SaveSpecialScholarship(SpecialScholarship obj)
        {

            try
            {
                foreach (var m in obj.ScholarshipDiscount)
                {
                    if (m.IsChecked == true)
                    {
                        MySqlCommand cmd = new MySqlCommand("save_specialscholarship", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@_SpecialScholarshipId", obj.SpecialScholarshipId);
                        cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
                        cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                        cmd.Parameters.AddWithValue("@_Class", obj.Class);
                        cmd.Parameters.AddWithValue("@_Faculty", obj.Faculty);
                        cmd.Parameters.AddWithValue("@_BatchClassId", obj.BatchClassId);
                        cmd.Parameters.AddWithValue("@_FeeStructureName", m.FeeStructureName);
                        cmd.Parameters.AddWithValue("@_FeeId", m.FeeId);
                        cmd.Parameters.AddWithValue("@_StudentType", m.StudentType);
                        cmd.Parameters.AddWithValue("@_Discount", m.Discount);
                        cmd.Parameters.AddWithValue("@_DiscountType", m.DiscountType);
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

        public DataTable GetStudentClass(string Studentid,int Companyid)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstudentclassinfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentId",Studentid);
                cmd.Parameters.AddWithValue("@_CompanyId", Companyid);
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

        public DataTable GetAllSpecialScholarship(int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallspecialscholarship", conn);
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

        public DataTable GetScholarshipDetails(int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallscholarshipdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetSearchedScholarship(string Batch, string Class, string StudentId,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedscholarship", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close();
                throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }
    }
}