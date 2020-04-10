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
    public class StudentDBServices
    {
        MySqlConnection conn;
        MySqlDataAdapter adap;

        private StudentDBServices()
        {
            //_dbService = new DbServices();
            conn = new MySqlConnection(AppSettings.MySqlConnectionString);
            adap = new MySqlDataAdapter();
        }
        public static StudentDBServices Instance { get { return new StudentDBServices(); } }

        public void SaveStudentAttendance(StudentAttendance obj)
        {
            try
            {
                foreach (var m in obj.Attendance)
                {
                    MySqlCommand cmd = new MySqlCommand("savestudentattendance", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                    cmd.Parameters.AddWithValue("@_Date", obj.Date);
                    cmd.Parameters.AddWithValue("@_StudentId", m.StudentId);
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

        public string SaveStudentdetails(StudentPersonalInfo obj)
        {
            if (obj.StudentId != null)
            {

                try
                {
                    MySqlCommand cmd = new MySqlCommand("save_editstudent", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
                    cmd.Parameters.AddWithValue("@_FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@_LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@_DateOfBirth_BS", obj.DateOfBirth_BS);
                    cmd.Parameters.AddWithValue("@_DateOfBirth_AD", obj.DateOfBirth_AD);
                    cmd.Parameters.AddWithValue("@_Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@_EmailId", obj.EmailId);
                    cmd.Parameters.AddWithValue("@_MobileNo", obj.MobileNo);
                    cmd.Parameters.AddWithValue("@_Batch", obj.CurrentEducation.Batch);
                    cmd.Parameters.AddWithValue("@_FatherName", obj.FatherName);
                    cmd.Parameters.AddWithValue("@_FatherNumber", obj.FatherNumber);
                    cmd.Parameters.AddWithValue("@_MotherName", obj.MotherNumber);
                    cmd.Parameters.AddWithValue("@_MotherNumber", obj.MotherNumber);
                    cmd.Parameters.AddWithValue("@_Status", obj.Status);
                    cmd.Parameters.AddWithValue("@_StudentType", obj.StudentType);
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    foreach (var m in obj.PastEducation)
                    {
                        m.StudentId = obj.StudentId;
                        m.CompanyId = obj.CompanyId;
                        SavePastEducation(m);
                    }
                    obj.CurrentEducation.StudentId = obj.StudentId;
                    obj.CurrentEducation.CompanyId = obj.CompanyId;
                    SaveCurrentEducation(obj.CurrentEducation);
                    obj.CurrentAddress.StudentId = obj.StudentId;
                    obj.CurrentAddress.CompanyId = obj.CompanyId;
                    SaveCurrentAddress(obj.CurrentAddress);
                    obj.PermanentAddress.StudentId = obj.StudentId;
                    obj.PermanentAddress.CompanyId = obj.CompanyId;
                    SavePermanentAddress(obj.PermanentAddress);
                    obj.EmergencyDetails.StudentId = obj.StudentId;
                    obj.EmergencyDetails.CompanyId = obj.CompanyId;
                    SaveEmergencyContact(obj.EmergencyDetails);
                    obj.Scholarship.StudentId = obj.StudentId;
                    obj.Scholarship.CompanyId = obj.CompanyId;
                    SaveScholarship(obj.Scholarship);
                    return "";
                }
                catch (Exception ex)
                {

                    conn.Close();
                    conn.Dispose();
                    return "";

                }
            }
            else
            {
                var Countstudentid = BaseDbServices.Instance.GetData("select StudentId from tblstudentinfo where CompanyId='" + obj.CompanyId + "' order by ID DESC", null);
                    var batch = obj.CurrentEducation.Batch.Substring(5);
                if(Countstudentid.Rows.Count > 0)
                {

                var stdid = Countstudentid.Rows[0]["StudentId"].ToString();
                    var check = Convert.ToInt32(stdid.Substring(5));

                obj.StudentId = batch + "-" + (check + 1); 
                }
                else
                {
                    obj.StudentId = batch + "-" + "1";
                }
                try
                {
                    MySqlCommand cmd = new MySqlCommand("save_student", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@StudentId", obj.StudentId);
                    cmd.Parameters.AddWithValue("@_FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@_LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@_DateOfBirth_BS", obj.DateOfBirth_BS);
                    cmd.Parameters.AddWithValue("@_DateOfBirth_AD", obj.DateOfBirth_AD);
                    cmd.Parameters.AddWithValue("@_Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@_EmailId", obj.EmailId);
                    cmd.Parameters.AddWithValue("@_MobileNo", obj.MobileNo);
                    cmd.Parameters.AddWithValue("@_Batch", obj.CurrentEducation.Batch);
                    cmd.Parameters.AddWithValue("@_FatherName", obj.FatherName);
                    cmd.Parameters.AddWithValue("@_FatherNumber", obj.FatherNumber);
                    cmd.Parameters.AddWithValue("@_MotherName", obj.MotherNumber);
                    cmd.Parameters.AddWithValue("@_MotherNumber", obj.MotherNumber);
                    cmd.Parameters.AddWithValue("@_Status", obj.Status);
                    cmd.Parameters.AddWithValue("@_StudentType", obj.StudentType);
                    cmd.Parameters.AddWithValue("@_CompanyId", obj.CompanyId);
                    cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);

                    var returnid = cmd.Parameters.Add("@_id", MySqlDbType.Int32);
                    returnid.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int id =Convert.ToInt32(returnid.Value);
                    List<MySqlParameter> Id = new List<MySqlParameter>()
                    {
                        new MySqlParameter("@Id", id)
                    };
                    var studentid = BaseDbServices.Instance.GetData("select StudentId from tblstudentinfo where ID=@Id", Id);

                    foreach (var m in obj.PastEducation)
                    {
                        m.StudentId =studentid.Rows[0]["StudentId"].ToString();
                        m.CompanyId = obj.CompanyId;
                        SavePastEducation(m);
                    }
                    obj.CurrentEducation.StudentId =studentid.Rows[0]["StudentId"].ToString();
                    obj.CurrentEducation.CompanyId = obj.CompanyId;
                    SaveCurrentEducation(obj.CurrentEducation);
                    obj.CurrentAddress.StudentId = studentid.Rows[0]["StudentId"].ToString();
                    obj.CurrentAddress.CompanyId = obj.CompanyId;
                    SaveCurrentAddress(obj.CurrentAddress);
                    obj.PermanentAddress.StudentId = studentid.Rows[0]["StudentId"].ToString();
                    obj.PermanentAddress.CompanyId = obj.CompanyId;
                    SavePermanentAddress(obj.PermanentAddress);
                    obj.EmergencyDetails.StudentId = studentid.Rows[0]["StudentId"].ToString();
                    obj.EmergencyDetails.CompanyId = obj.CompanyId;
                    SaveEmergencyContact(obj.EmergencyDetails);
                    obj.Scholarship.StudentId = studentid.Rows[0]["StudentId"].ToString();
                    obj.Scholarship.CompanyId = obj.CompanyId;
                    SaveScholarship(obj.Scholarship);
                    return studentid.Rows[0]["StudentId"].ToString();
                }
                catch (Exception ex)
                {

                    conn.Close();
                    conn.Dispose();
                    throw;

                }
            }
        }

        public void SavePastEducation(StudentPastEducation obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_pasteducation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_PastEducationId",obj.PastEducationId);
                cmd.Parameters.AddWithValue("@_Degree",obj.Degree);
                cmd.Parameters.AddWithValue("@_School", obj.School);
                cmd.Parameters.AddWithValue("@_TotalMarks", obj.TotalMarks);
                cmd.Parameters.AddWithValue("@_Division", obj.Division);
                cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
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

        public void SaveCurrentEducation(StudentCurrentEducation obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_currenteducation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CurrentEducationId", obj.CurrentEducationId);
                cmd.Parameters.AddWithValue("@_Batch", obj.Batch);
                cmd.Parameters.AddWithValue("@_Faculty", obj.Faculty);
                cmd.Parameters.AddWithValue("@_Class", obj.Class);
                cmd.Parameters.AddWithValue("@_Section", obj.Section);
                cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
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

        public void SaveCurrentAddress(Address obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_currentaddress", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_AddressId", obj.AddressId);
                cmd.Parameters.AddWithValue("@_Qno", obj.Qno);
                cmd.Parameters.AddWithValue("@_Street", obj.Street);
                cmd.Parameters.AddWithValue("@_Municipality", obj.Municipality);
                cmd.Parameters.AddWithValue("@_State", obj.State);
                cmd.Parameters.AddWithValue("@_Country", obj.Country);
                cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
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

        public void SavePermanentAddress(Address obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_permanentaddress", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_AddressId", obj.AddressId);
                cmd.Parameters.AddWithValue("@_Qno", obj.Qno);
                cmd.Parameters.AddWithValue("@_Street", obj.Street);
                cmd.Parameters.AddWithValue("@_Municipality", obj.Municipality);
                cmd.Parameters.AddWithValue("@_State", obj.State);
                cmd.Parameters.AddWithValue("@_Country", obj.Country);
                cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
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

        public void SaveEmergencyContact(EmergencyContact obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_emergencycontact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_EmergencyContactId", obj.EmergencyContactId);
                cmd.Parameters.AddWithValue("@_ParentName", obj.ParentName);
                cmd.Parameters.AddWithValue("@_ContactNumber", obj.ContactNumber);
                cmd.Parameters.AddWithValue("@_Location", obj.Location);
                cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
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

        public void SaveScholarship(StudentScholarship obj)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_scholarship", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_StudentScholarshipId", obj.StudentScholarshipId);
                cmd.Parameters.AddWithValue("@_ScholarshipName", obj.ScholarshipName);
                cmd.Parameters.AddWithValue("@_Description", obj.Description);
                cmd.Parameters.AddWithValue("@_DateOfAdmission", obj.DateOfAdmission);
                cmd.Parameters.AddWithValue("@_StudentId", obj.StudentId);
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

        public void RunQuery(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
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

        public DataTable GetStudentListByBatch(string Date,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstudentlistbybatch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Date", Date);
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
        public DataTable CheckStudentAttendance(string batch,string date,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstudentattendancelistbybatch", conn);
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

       

        public DataTable GetStudentListByClass(string _class,string date,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstudentlistbyclass", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Class", _class);
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

        public DataTable GetStudentListBySection(string _class,string section,string date,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getstudentlistbysection", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Class", _class);
                cmd.Parameters.AddWithValue("@_Section", section);
                cmd.Parameters.AddWithValue("@_Date", date);
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


        public DataTable GetAllStudents(string Batch, string Class,int CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallstudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
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
        public DataTable GetSearchedStudent(string Batch,string Class,string Section,string Name,string Type,int CompanyId)
        {
           
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getsearchedstudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Section", Section);
                cmd.Parameters.AddWithValue("@_FirstName", Name);
                cmd.Parameters.AddWithValue("@_Type", Type);
                cmd.Parameters.AddWithValue("@_CompanyId", CompanyId);
                adap.SelectCommand = cmd;
                conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)

            {
               conn.Close();  throw;
            }
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public DataTable GetPresentAttendance(string Batch, string Class, string Section, string DateFrom,string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getpresentattendance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Section", Section);
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
                MySqlCommand cmd = new MySqlCommand("getallpresentattendance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Batch", Batch);
                cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", DateTo);
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
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
        public DataTable GetAllAbsentAttendance(string Batch, string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getallabsentattendance", conn);
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

        public DataTable GetAbsentAttendance(string Batch, string Class, string Section, string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getabsentattendance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
                cmd.Parameters.AddWithValue("@_Section", Section);
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

        public DataTable GetTotalHolidays(string Batch, string Class,string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("gettotalholidays", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_Class", Class);
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

        public DataTable GetAttendanceDetails(string Batch, string StudentId, string DateFrom, string DateTo,int CompanyId)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getattendancedetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Batch", Batch);
                cmd.Parameters.AddWithValue("@_StudentId", StudentId);
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
