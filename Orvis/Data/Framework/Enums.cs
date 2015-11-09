using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Data.Framework
{
    public enum FilterComparison
    {
        Equals = 0,
        NonEquals = 1,
        Greater = 2,
        GreateEquals = 3,
        Small = 4,
        SmallEquals = 5,
        In = 6,
        NotIn = 7,
        Bettween = 8
    }

    public enum FilterConstraint
    {
        AND = 0,
        OR = 1
    }
}
