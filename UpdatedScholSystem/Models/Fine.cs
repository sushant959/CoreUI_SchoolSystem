using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class Fine
    {
        public int FineId { get; set; }
        public int DayFrom { get; set; }
        public int DayTo { get; set; }
        public string FineType { get; set; }
        public int FineAmount { get; set; }
        public int CompanyId { get; set; }
    }
}