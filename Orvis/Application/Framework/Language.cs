using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Framework
{
    public class Language : Base.Entity
    {
        public string Code { get; set; }

        public override void Configuration()
        {
            this.HasDescription = false;
        }
    }
}
