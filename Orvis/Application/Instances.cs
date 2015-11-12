using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application
{
    public partial class Facede
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

        private Corporation.Facade corporation;
        public Corporation.Facade Corporation
        {
            get
            {
                if (corporation == null) corporation = new Application.Corporation.Facade();
                return corporation;
            }
        }

        private Finance.Facade finance;
        public Finance.Facade Finance
        {
            get
            {
                if (finance == null) finance = new Application.Finance.Facade();
                return finance;
            }
        }

        private Framework.Facade framework;
        public Framework.Facade Framework
        {
            get
            {
                if (framework == null) framework = new Application.Framework.Facade();
                return framework;
            }
        }

        public Member.Facade member;
        public Member.Facade Member
        {
            get
            {
                if (member == null) member = new Application.Member.Facade();
                return member;
            }
        }

        
        
    }
}
