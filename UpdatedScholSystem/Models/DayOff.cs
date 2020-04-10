using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class DayOff
    {
        public int DayOffId { get; set; }
        public string Batch { get; set; }
        public int Id { get; set; }
        public string[] Department { get; set; }
        public string[] Faculty { get; set; }
        public string[] Class { get; set; }
        public string Title { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int CompanyId { get; set; }
       
    }
}