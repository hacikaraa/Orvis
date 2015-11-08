using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application
{
    public class Facede
    {
        private Catalog.Facade catalog;
        public Catalog.Facade Catalog
        {
            get
            {
                if (catalog == null) catalog = new Application.Catalog.Facade();
                return catalog;
            }
        }
    }
}
