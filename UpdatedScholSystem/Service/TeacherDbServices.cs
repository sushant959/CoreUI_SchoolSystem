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
    public class TeacherDbServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private TeacherDbServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static TeacherDbServices Instance { get { return new TeacherDbServices(); } }

        public int SaveTeacherDetails(TeacherPersonalInfo obj)
        {
            if (obj.TeacherId != 0)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("save_editteacher", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_TeacherId", obj.TeacherId);
                    cmd.Parameters.AddWithValue("@_FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@_LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@_Designation", obj.Designation);
                    cmd.Parameters.AddWithValue("@_Department", obj.Department);
                    cmd.Parameters.AddWithValue("@_Faculty", obj.Faculty);
                    cmd.Parameters.AddWithValue("@_Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@_Email", obj.Email);
                    cmd.Parameters.AddWithValue("@_DateOfBirth", obj.DateOfBirth);
                    cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                    cmd.Parameters.AddWithValue("@_Citizenship", obj.Citizenship);
                    cmd.Parameters.AddWithValue("@_JoiningDate", obj.JoiningDate);
                    cmd.Parameters.AddWithValue("@_Status", obj.Status);
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    foreach (var m in obj.Educations)
                    {
                        m.TeacherId = obj.TeacherId;
                        m.CompanyId = obj.CompanyId;
                        SaveEducation(m);
                    }
                    foreach (var m in obj.Experiences)
                    {
                        m.TeacherId = obj.TeacherId;
                        m.CompanyId = obj.CompanyId;
                        SaveExperience(m);
                    }

                    obj.CurrentAddress.TeacherId = obj.TeacherId;
                    obj.CurrentAddress.CompanyId = obj.CompanyId;
                    SaveCurrentAddress(obj.CurrentAddress);
                    obj.PermanentAddress.TeacherId = obj.TeacherId;
                    obj.PermanentAddress.CompanyId = obj.CompanyId;
                    SavePermanentAddress(obj.PermanentAddress);
                    return 0;
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
                    MySqlCommand cmd = new MySqlCommand("save_teacher", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@_LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@_Designation", obj.Designation);
                    cmd.Parameters.AddWithValue("@_Department", obj.Department);
                    cmd.Parameters.AddWithValue("@_Faculty", obj.Faculty);
                    cmd.Parameters.AddWithValue("@_Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@_Email", obj.Email);
                    cmd.Parameters.AddWithValue("@_DateOfBirth", obj.DateOfBirth);
                    cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                    cmd.Parameters.AddWithValue("@_Citizenship", obj.Citizenship);
                    cmd.Parameters.AddWithValue("@_JoiningDate", obj.JoiningDate);
                    cmd.Parameters.AddWithValue("@_Status", obj.Status);
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                    returnid.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int id = Convert.ToInt32(returnid.Value);
                    foreach (var m in obj.Educations)
                    {
                        m.TeacherId = id;
                        m.CompanyId = obj.CompanyId;
                        SaveEducation(m);
                    }
                    foreach (var m in obj.Experiences)
                    {
                        m.TeacherId = id;
                        m.CompanyId = obj.CompanyId;
                        SaveExperience(m);
                    }
                    obj.CurrentAddress.TeacherId = id;
                    obj.CurrentAddress.CompanyId = obj.CompanyId;
                    SaveCurrentAddress(obj.CurrentAddress);
                    obj.PermanentAddress.TeacherId = id;
                    obj.PermanentAddress.CompanyId = obj.CompanyId;
                    SavePermanentAddress(obj.PermanentAddress);
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
        public void SaveEducation(Education obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_education", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Degree", obj.Degree);
                cmd.Parameters.AddWithValue("@_Institution", obj.Institution);
                cmd.Parameters.AddWithValue("@_TotalMarks", obj.TotalMarks);
                cmd.Parameters.AddWithValue("@_Division", obj.Division);
                cmd.Parameters.AddWithValue("@_TeacherId", obj.TeacherId);
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

        public void SaveExperience(Experience obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_experience", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Organisation", obj.Organisation);
                cmd.Parameters.AddWithValue("@_Post", obj.Post);
                cmd.Parameters.AddWithValue("@_DateFrom", obj.DateFrom);
                cmd.Parameters.AddWithValue("@_DateTo", obj.DateTo);
                cmd.Parameters.AddWithValue("@_TeacherId", obj.TeacherId);
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

        public void SaveCurrentAddress(TeacherAddress obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_teachercurrentaddress", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_AddressId", obj.AddressId);
                cmd.Parameters.AddWithValue("@_HouseNo", obj.HouseNo);
                cmd.Parameters.AddWithValue("@_Street", obj.Street);
                cmd.Parameters.AddWithValue("@_Municipality", obj.Municipality);
                cmd.Parameters.AddWithValue("@_Country", obj.Country);
                cmd.Parameters.AddWithValue("@_State", obj.State);
                cmd.Parameters.AddWithValue("@_MobileNo", obj.MobileNo);
                cmd.Parameters.AddWithValue("@_PhoneNo", obj.PhoneNo);
                cmd.Parameters.AddWithValue("@_TeacherId", obj.TeacherId);
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

        public void SavePermanentAddress(TeacherAddress obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_teacherpermanentaddress", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_AddressId", obj.AddressId);
                cmd.Parameters.AddWithValue("@_HouseNo", obj.HouseNo);
                cmd.Parameters.AddWithValue("@_Street", obj.Street);
                cmd.Parameters.AddWithValue("@_Municipality", obj.Municipality);
                cmd.Parameters.AddWithValue("@_Country", obj.Country);
                cmd.Parameters.AddWithValue("@_State", obj.State);
                cmd.Parameters.AddWithValue("@_MobileNo", obj.MobileNo);
                cmd.Parameters.AddWithValue("@_PhoneNo", obj.PhoneNo);
                cmd.Parameters.AddWithValue("@_TeacherId", obj.TeacherId);
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

        public DataTable GetAllTeachers(int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallteacher", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                //conn.Close();  throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }


        public DataTable GetStaffListByBatch(string batch,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstafflistbybatch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", batch);
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

        public DataTable CheckStaffAttendance(string batch, string date,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstaffattendancelistbybatch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", batch);
                cmd.Parameters.AddWithValue("@_Date", date);
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

        public DataTable GetStaffListByDepartment(string batch, string department,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstafflistbydepartment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", batch);
                cmd.Parameters.AddWithValue("@_Department", department);
                cmd.Parameters.AddWithValue("@_CompanyId",CompanyId);
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

        public DataTable GetStaffListByDesignation(string batch, string department, string designation,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstafflistbydesignation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", batch);
                cmd.Parameters.AddWithValue("@_Department", department);
                cmd.Parameters.AddWithValue("@_Designation", designation);
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

        public void SaveStaffAttendance(StaffAttendance obj)
        {
            try
            {
                foreach (var m in obj.Attendance)
                {
                    MySqlCommand cmd = new MySqlCommand("save_staffattendance", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                    cmd.Parameters.AddWithValue("@_Date", obj.Date);
                    cmd.Parameters.AddWithValue("@_TeacherId", m.TeacherId);
                    cmd.Parameters.AddWithValue("@_Attendance", m.Attendance);
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                conn.Close();
                conn.Dispose();
                throw;
            }
        }

        public DataTable GetPresentAttendance(string Batch, string Department, string Designation, string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstaffpresentattendance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Department", Department);
                cmd.Parameters.AddWithValue("@_Designation", Designation);
                cmd.Parameters.AddWithValue("@_DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@_DateTo", DateTo);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;     
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetAllPresentAttendance(string Batch,string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstaffpresentattendance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@_DateTo", DateTo);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetAbsentAttendance(string Batch, string Department, string Designation, string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstaffabsentattendance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Department", Department);
                cmd.Parameters.AddWithValue("@_Designation", Designation);
                cmd.Parameters.AddWithValue("@_DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@_DateTo", DateTo);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetAllAbsentAttendance(string Batch,string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstaffabsentattendance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@_DateTo", DateTo);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetTotalTeacherHolidays(string Batch, string Department,string Faculty,string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("gettotalteacherholidays", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Department", Department);
                cmd.Parameters.AddWithValue("@_Faculty", Faculty);
                cmd.Parameters.AddWithValue("@_DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@_DateTo", DateTo);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetTotalHolidays(string Batch, string Department,string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("gettotaldepartmentholidays", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Department", Department);
                cmd.Parameters.AddWithValue("@_DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@_DateTo", DateTo);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetAttendanceDetails(string Batch, string TeacherId, string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstaffattendancedetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_TeacherId", TeacherId);
                cmd.Parameters.AddWithValue("@_DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@_DateTo", DateTo);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
                conn.Close(); throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }
    }
}