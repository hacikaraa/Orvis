using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Framework
{
    public class ParameterValue : Base.Entity
    {
        public string Value { get; set; }

        public Language Language { get; set; }

        public override void Configuration()
        {
            this.HasName = false;
            this.HasDescription = false;
        }
    }
}
