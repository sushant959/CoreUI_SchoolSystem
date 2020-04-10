using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class Transportation
    {
        public int TransportationId { get; set; }
        public string PlaceTo { get; set; }
        public int DistanceFrom { get; set; }
        public int DistanceTo { get; set; }
        public int OneWayAmount { get; set; }
        public int TwoWayAmount { get; set; }
        public int CompanyId { get; set; }
    }
}