using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Data.Framework
{
    public class Filter 
    {
        public object Parameter { get; set; }

        public object Parameter2 { get; set; }

        public string Column { get; set; }

        public FilterComparison Comparison { get; set; }

        public FilterConstraint Constraint { get; set; }

    }
}
