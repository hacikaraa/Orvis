using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orvis.Data.Framework;

namespace Orvis.Application.Finance
{
    public class Facade : Base.BaseFacede
    {
        public Tax CreateTax(Tax tax)
        {
            if (tax != null && tax.ID == 0 && !String.IsNullOrEmpty(tax.Name) && tax.Rate >= 0 && tax.Rate <= 100)
            {
                return this.Data.Insert<Tax>(tax);
            }
            return null;
        }

        public Tax UpdateTax(Tax tax)
        {
            if (tax != null && tax.ID > 0 && !String.IsNullOrEmpty(tax.Name) && tax.Rate >= 0 && tax.Rate <= 100)
            {
                return this.Data.Update<Tax>(tax);
            }
            return null;
        }

        public TaxList GetTaxes(SearchEngine search = null)
        {
            if (search == null)
                search = new SearchEngine();
            return this.Data.GetEntityList<Tax, TaxList>(search);
        }
    }
}
