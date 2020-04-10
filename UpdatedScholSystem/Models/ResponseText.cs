using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class ResponseText
    {
        public int ResponseTextId { get; set; }
        public string Response { get; set; }
        public int CompanyId { get; set; }
    }
}