using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Base
{
    public abstract class BaseFacede
    {
        private Base.DataAction data;
        public Base.DataAction Data
        {
            get
            {
                if (data == null) data = new DataAction(this);
                return data;
            }
        }
    }
}
