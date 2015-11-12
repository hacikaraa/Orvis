using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Contact
{
    public class Contact : Base.Entity
    {
        public Contact()
        {

        }
        public City City { get; set; }
        public County County { get; set; }
        public AddressList AddressList { get; set; }
        public PhoneList PhoneList { get; set; }
        public override void Configuration()
        {
            
        }
    }
}
