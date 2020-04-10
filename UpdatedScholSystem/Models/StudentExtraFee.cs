using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class StudentExtraFee
    {
        public int StudentExtraFeeId { get; set; }
        public string Batch { get; set; }
        public string Class { get; set; }
        public string Faculty { get; set; }
        public string Section { get; set; }
        public string Month { get; set; }
        public int CompanyId { get; set; }
        public List<ExtraFeeDetails> ExtraFeeDetails { get; set; }
    }

    public class ExtraFeeDetails
    {
        public string StudentId { get; set; }
        public List<StudentExtraDetails> StudentExtraDetails { get; set; }
        public bool IsChecked { get; set; }
    }

    public class StudentExtraDetails
    {
        public int StudentExtraId { get; set; }
        public string FeeName { get; set; }
        public int Amount { get; set; }
        public int StudentExtraFeeId { get; set; }
        public bool IsChecked { get; set; }
        public int CompanyId { get; set; }
    }
}