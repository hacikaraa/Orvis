using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Framework
{
    public class PropertyValue : Base.Entity
    {
        public Property Property { get; set; }

        public object Value { get; set; }

        public override void Configuration()
        {
            this.HasName = false;
        }
    }
}
