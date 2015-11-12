using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Contact
{
    public class County:Base.Entity
    {
        public County() { }
        public City City { get; set; }
        public override void Configuration()
        {
            HasDateCreated = false;
            HasDateUpdated = false;
            HasUserCreated = false;
            HasUserUpdated = false;
            HasDescription = false;
        }
    }
}
