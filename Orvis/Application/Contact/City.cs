using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Contact
{
    public class City:Base.Entity
    {
        public override void Configuration()
        {
            HasDateCreated = false;
            HasDateUpdated = false;
            HasUserCreated = false;
            HasUserUpdated = false;
        }
    }
}
