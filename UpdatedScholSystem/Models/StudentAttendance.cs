using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class StudentAttendance
    {
        public int StudentAttendanceId { get; set; }
        public string Date { get; set; }
        public string Batch { get; set; }
        public int CompanyId { get; set; }
        public List<AttendanceInfo> Attendance { get; set; }

    }

    public class AttendanceInfo
    {
        public string StudentId { get; set; }
        public string Attendance { get; set; }
    }
}