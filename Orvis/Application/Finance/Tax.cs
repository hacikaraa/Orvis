using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Finance
{
    public class Tax : Base.Entity
    {
        public float Rate { get; set; }

        public override void Configuration()
        {
            this.HasDescription = false;
        }
    }
}
