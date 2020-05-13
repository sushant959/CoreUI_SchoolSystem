using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string UserRole { get; set; }
        public int? Company_ID { get; set; }
    }
}