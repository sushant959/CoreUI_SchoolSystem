using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class Scholarship
    {
        public int Scholarshipid { get; set; }
        public string ScholarshipName { get; set; }
        public int CompanyId { get; set; }
    }
}