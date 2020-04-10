using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class CompanyDetails
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string RegistrationDate { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string PhoneNo { get; set; }
        public string PAN { get; set; }
        public User Users { get; set; }
    }
}