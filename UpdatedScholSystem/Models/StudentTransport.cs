using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class StudentTransport
    {
        public int StudentTransportId { get; set; }
        public string Batch { get; set; }
        public int CompanyId { get; set; }
        public List<StudentTransportDetails> StudentTransportDetails { get; set; }
    }

    public class StudentTransportDetails
    {
        public string StudentId { get; set; }
        public string PlaceTo { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public bool IsChecked { get; set; }
    }
}