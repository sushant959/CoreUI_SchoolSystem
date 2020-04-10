using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class Billing
    {
        public int BillingId { get; set; }
        public string StudentId { get; set; }
        public string CreatedBy { get; set; }
        public string[]  Month { get; set; }
        public string Batch { get; set; }
        public string[] Class { get; set; }
        public string[] Section { get; set; }
        public int Amount { get; set; }
        public int CompanyId { get; set; }
        public List<BillingFee> BillingFees { get; set; }
        
    }
}