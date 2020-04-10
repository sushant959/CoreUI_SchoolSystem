using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class ScholarshipDiscount
    {
        public string DiscountType { get; set; }
        public int Discount { get; set; }
        public int Amount { get; set; }
        public string FeeStructureName { get; set; }
        public int FeeId { get; set; }
        public string StudentType { get; set; }
        public bool IsChecked { get; set; }
        public int CompanyId { get; set; }
    }
}