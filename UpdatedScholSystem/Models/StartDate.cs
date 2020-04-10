using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class StartDate
    {
        public int StartDateId { get; set; }
        public string Batch { get; set; }
        public string Date { get; set; }
        public int CompanyId { get; set; }
    }
}