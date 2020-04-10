using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class StudentType
    {
        public int StudentTypeId { get; set; }
        public string StudentTypeName { get; set; }
        public int CompanyId { get; set; }
    }
}