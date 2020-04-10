using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class SpecialScholarship
    {
        public int SpecialScholarshipId { get; set; }
        public string StudentId { get; set; }
        public string Batch { get; set; }
        public string Class { get; set; }
        public string Faculty { get; set; }
        public int BatchClassId { get; set; }
        public int CompanyId { get; set; }
        public List<ScholarshipDiscount> ScholarshipDiscount { get; set; }
    }
}