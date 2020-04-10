using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class FeeManagement
    {
        public int ID { get; set; }
        public int FeeId { get; set; }
        public bool IsChecked { get; set; }
        public string FeeStructureName { get; set; }
        public string FeeName { get; set; }
        public int Amount { get; set; }
        public string Refundable { get; set; }
        public string StudentType { get; set; }
        public int BatchClassId { get; set; } = 0;
        public int CompanyId { get; set; }
    }
}