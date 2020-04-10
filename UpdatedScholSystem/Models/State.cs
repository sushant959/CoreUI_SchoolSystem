using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class State
    {
       public int StateId { get; set; }
       public string StateName { get; set; }
       public string CountryName { get; set; }
       public int CompanyId { get; set; } 
    }
}