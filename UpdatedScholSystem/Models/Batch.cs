using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class Batch
    {
        public int BatchId { get; set; }
        public int SessionFrom { get; set; }
        public int SessionTo { get; set; }
        public string Status { get; set; }
        public int CompanyId { get; set; }
    }
}