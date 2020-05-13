using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class UserControl
    {
       
        public int ID { get; set; }
        public int? Company_ID { get; set; }
        public int Feature_ID { get; set; }
        public int Group_ID { get; set; }
        public int Action_ID { get; set; }
        public IList<ActionFeature> ActionFeatures { get; set; }
    }
}