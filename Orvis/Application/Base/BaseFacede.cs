using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Base
{
    public abstract class BaseFacede
    {
        private Data.DataAction data;
        public Data.DataAction Data
        {
            get
            {
                if (data == null) data = new Data.DataAction(this);
                return data;
            }
        }
    }
}
