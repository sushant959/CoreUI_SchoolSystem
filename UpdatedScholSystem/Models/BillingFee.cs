using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class BillingFee
    {
        public int BillingFeeId { get; set; }
        public string FeeStructureName { get; set; }
        public string BillingId { get; set; }
        public bool IsChecked { get; set; }
        public int CompanyId { get; set; }
    }
}