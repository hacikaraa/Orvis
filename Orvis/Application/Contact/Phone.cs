using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Contact
{
    public class Phone:Base.Entity
    {
        public string PhoneChar { get; set; }
        public PhoneType PhoneType { get; set; } 
        public override void Configuration()
        {
            HasName = false;
            HasUserCreated = false;
            HasUserUpdated = false;
            HasDescription = false;
        }
    }
}
