using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class FeatureAction
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Company_ID { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }
    }
}