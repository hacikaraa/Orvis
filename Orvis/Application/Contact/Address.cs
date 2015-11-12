using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Contact
{
    public class Address : Base.Entity
    {
        public City City { get; set; }
        public County County { get; set; }
        public Phone Phone { get; set; }
        public override void Configuration()
        {
           
        }
    }
}
