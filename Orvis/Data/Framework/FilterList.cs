using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Data.Framework
{
    public class FilterList : Application.Base.EntityList<Filter>
    {
        public Filter Add(string Column, object Parameter)
        {
            Filter f = new Filter();
            f.Column = Column;
            f.Parameter = Parameter;
            f.Comparison = FilterComparison.Equals;
            this.Add(f);
            return f;
        }

        public Filter Add(string Column, object Parameter, object Parameter2)
        {
            Filter f = new Filter();
            f.Column = Column;
            f.Parameter = Parameter;
            f.Parameter2 = Parameter2;
            f.Comparison = FilterComparison.Bettween;
            this.Add(f);
            return f;
        }

        public Filter Add(string Column, object Parameter, object Parameter2, FilterConstraint Constraint)
        {
            Filter f = new Filter();
            f.Column = Column;
            f.Parameter = Parameter;
            f.Parameter2 = Parameter2;
            f.Comparison = FilterComparison.Bettween;
            f.Constraint = Constraint;
            this.Add(f);
            return f;
        }

        public Filter Add(string Column, object Parameter, FilterComparison Comparison)
        {
            Filter f = new Filter();
            f.Column = Column;
            f.Parameter = Parameter;
            f.Comparison = Comparison;
            this.Add(f);
            return f;
        }

        public Filter Add(string Column, object Parameter, FilterComparison Comparison, FilterConstraint Constraint)
        {
            Filter f = new Filter();
            f.Column = Column;
            f.Parameter = Parameter;
            f.Comparison = Comparison;
            f.Constraint = Constraint;
            this.Add(f);
            return f;
        }
        
    }
}
