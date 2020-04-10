using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class FollowUp
    {
        public int FollowUpId { get; set; }
        public int BillingId { get; set; }
        public string StudentId { get; set; }
        public string Response { get; set; }
        public int Month { get; set; }
        public string Batch { get; set; }
        public string FollowUpDate { get; set; }
        public string ExpectedDate { get; set; }
        public int CompanyId { get; set; }

    }
}