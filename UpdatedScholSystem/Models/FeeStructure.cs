using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class FeeStructure
    {
        public int FeeStructureId { get; set; }
        public string FeeStructureName { get; set; }
        public int CompanyId { get; set; }
    }
}