using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Framework
{
    public class ParameterValueList : Base.EntityList<ParameterValue>
    {
        public void Add(string value,Language language)
        {
            ParameterValue pv = new ParameterValue();
            pv.Value = value;
            pv.Language = language;
            this.Add(pv);
        }
    }
}
