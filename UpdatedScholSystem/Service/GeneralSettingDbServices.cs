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
    public class GeneralSettingDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private GeneralSettingDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static GeneralSettingDbServices Instance { get { return new GeneralSettingDbServices(); } }

        public void SaveBatchDetails(int from, int to,int companyid)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_batch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_FromYear", from);
                cmd.Parameters.AddWithValue("@_ToYear", to);
                cmd.Parameters.AddWithValue("@_CompanyId",companyid);
                cmd.Parameters.AddWithValue("@_Status", "Active");
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
        public void SaveClassDetails(Class obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_class", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ClassId", obj.ClassId);
                cmd.Parameters.AddWithValue("@_ClassName", obj.ClassName);
                cmd.Parameters.AddWithValue("@_ClassCode", obj.ClassCode);
                cmd.Parameters.AddWithValue("@_ClassType", obj.ClassType);
                cmd.Parameters.AddWithValue("@_StudentLimit", obj.StudentLimit);
                cmd.Parameters.AddWithValue("@_FacultyId", obj.FacultyId);
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

        public void EditBatchDetails(Batch obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("edit_batch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_BatchId", obj.BatchId);
                cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@_FromYear", obj.SessionFrom);
                cmd.Parameters.AddWithValue("@_ToYear", obj.SessionTo);
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

        public void SaveFacultyDetails(Faculty obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_faculty", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_FacultyId", obj.FacultyId);
                cmd.Parameters.AddWithValue("@_FacultyName", obj.FacultyName);
                cmd.Parameters.AddWithValue("@_FacultyCode", obj.FacultyCode);
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

        public void SaveStartDate(StartDate obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_startdate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StartDateId", obj.StartDateId);
                cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                cmd.Parameters.AddWithValue("@_Date", obj.Date);
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

        public void SaveSectionDetails(Section obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_section", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_SectionId", obj.SectionId);
                cmd.Parameters.AddWithValue("@_SectionName", obj.SectionName);
                cmd.Parameters.AddWithValue("@_StudentLimit", obj.StudentLimit);
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

        public void SaveCountryDetails(Country obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_country", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CountryId", obj.CountryId);
                cmd.Parameters.AddWithValue("@_CountryName", obj.CountryName);
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

        public void SaveResponseText(ResponseText obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_responsetext", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ResponseTextId", obj.ResponseTextId);
                cmd.Parameters.AddWithValue("@_ResponseText", obj.Response);
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

        public void SaveStateDetails(State obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_state", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StateId", obj.StateId);
                cmd.Parameters.AddWithValue("@_StateName", obj.StateName);
                cmd.Parameters.AddWithValue("@_CountryName", obj.CountryName);
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

        public void SaveFeeStructure(FeeStructure obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_feestructure", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_FeeStructureId",obj.FeeStructureId);
                cmd.Parameters.AddWithValue("@_FeeStructureName", obj.FeeStructureName);
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

        public void SaveStudentType(StudentType obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_studenttype", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentTypeId", obj.StudentTypeId);
                cmd.Parameters.AddWithValue("@_StudentTypeName", obj.StudentTypeName);
                cmd.Parameters.AddWithValue("@_CompanyId",obj.CompanyId);
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

        public void SaveFeeDetails(FeeManagement obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_feedetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_FeeId", obj.FeeId);
                cmd.Parameters.AddWithValue("@_FeeName", obj.FeeName);
                cmd.Parameters.AddWithValue("@_FeeStructureName", obj.FeeStructureName);
                cmd.Parameters.AddWithValue("@_Amount", obj.Amount);
                cmd.Parameters.AddWithValue("@_Refundable", obj.Refundable);
                cmd.Parameters.AddWithValue("@_StudentType", obj.StudentType);
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

        public void SaveClassFeeDetails(BatchClass obj)
        {
            try
            {
                if (obj.BatchClassId != 0)
                {
                    foreach (var m in obj.ClassFeeManagement)
                    {
                        if (m.IsChecked == true)
                        {
                            m.BatchClassId =obj.BatchClassId;
                            m.CompanyId = obj.CompanyId;
                            SaveBatchClassFee(m);
                        }
                        else
                        {
                            
                        }

                    }
                }
                else
                {

                    MySqlCommand cmd = new MySqlCommand("save_batchclass", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_BatchClassId", obj.BatchClassId);
                    cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                    cmd.Parameters.AddWithValue("@_Class", obj.Class);
                    cmd.Parameters.AddWithValue("@_Faculty", obj.Faculty);
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                    returnid.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int id = (int)returnid.Value;

                    foreach (var m in obj.ClassFeeManagement)
                    {
                        if (m.IsChecked == true)
                        {
                            m.BatchClassId = id;
                            m.CompanyId = obj.CompanyId;
                            SaveBatchClassFee(m);
                        }
                        else
                        {

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
    
        public void SaveBatchClassFee(FeeManagement obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_batchclassfee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ID", obj.ID);
                cmd.Parameters.AddWithValue("@_FeeId", obj.FeeId);
                cmd.Parameters.AddWithValue("@_FeeStructureName", obj.FeeStructureName);
                cmd.Parameters.AddWithValue("@_FeeName", obj.FeeName);
                cmd.Parameters.AddWithValue("@_Amount", obj.Amount);
                cmd.Parameters.AddWithValue("@_StudentType", obj.StudentType);
                cmd.Parameters.AddWithValue("@_Refundable", obj.Refundable);
                cmd.Parameters.AddWithValue("@_BatchClassId", obj.BatchClassId);
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

        public DataTable GetAllBatchClassFee(int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallbatchclassfee", conn);
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

        public DataTable GetSearchedClassFee(string Batch, string Class, string Faculty,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedclassfee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Faculty",Faculty);
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

        public DataTable GetSearchedFee(string FeeName, string Type,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedfee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Name", FeeName);
                cmd.Parameters.AddWithValue("@_Type", Type);
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

        public DataTable GetSearchedDayOff(string Batch, string Department,string Faculty,string Class,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearcheddayoff", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch",Batch);
                cmd.Parameters.AddWithValue("@_Department",Department);
                cmd.Parameters.AddWithValue("@_Faculty", Faculty);
                cmd.Parameters.AddWithValue("@_Class", Class);
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