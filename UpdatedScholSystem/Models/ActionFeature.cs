using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Models
{
    public class ActionFeature
    {
        public int Feature_ID { get; set; }

        public IList<FeatureAction> FeatureActions { get; set; }
    }
}