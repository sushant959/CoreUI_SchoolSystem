using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class StaffAttendance
    {
        public int StaffId { get; set; }
        public string Date { get; set; }
        public string Batch { get; set; }
        public int CompanyId { get; set; }
        public List<AttendanceDetails> Attendance { get; set; }
    }
    public class AttendanceDetails
    {
        public string TeacherId { get; set; }
        public string Attendance { get; set; }
    }
}