using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public string ClassType { get; set; }
        public int StudentLimit { get; set; }
        public int FacultyId { get; set; }
        public int CompanyId { get; set; }

    }
}