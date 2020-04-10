using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class BatchClass
    {
        public int BatchClassId { get; set; }
        public string Batch { get; set; }
        public string Class { get; set; }
        public string Faculty { get; set; }
        public int CompanyId { get; set; }
        public List<FeeManagement> ClassFeeManagement { get; set; }
    }
   
}