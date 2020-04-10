using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class Receipt
    {
        public int ReceiptId { get; set; }
        public string BillingId { get; set; }
        public string StudentId { get; set; }
        public string Month { get; set; }
        public int Fine { get; set; }
        public string CreatedBy { get; set; }
        public int Discount { get; set; }
        public string Batch { get; set; }
        public int TotalAmount { get; set; }
        public int PaidAmount { get; set; }
        public int DueAmount { get; set; }
        public int PreviousDue { get; set; }
        public string Date { get; set; }
        public string PaymentMethod { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public int CompanyId { get; set; }
    }
}