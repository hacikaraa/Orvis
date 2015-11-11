using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orvis.Data.Framework;

namespace Orvis.Application.Framework
{
    public class Parameter : Base.Entity
    {
        public string Alias { get; set; }

        private ParameterValueList values;
        public ParameterValueList Values
        {
            get
            {
                if (values == null)
                {
                    SearchEngine search = new SearchEngine();
                    search.ExistColumns.Add("ParameterValue.*");
                    search.Joins.Add(" INNER JOIN Framework_ParameterParameterValue ON Framework_ParameterParameterValue.ParameterValueID = Framework_ParameterValue.ID ");
                    search.Filters.Add("Framework_ParameterParameterValue.ParameterID", this.ID);
                    values = this.Application.Framework.GetParameterValues(search);
                }
                return values;
            }
        }

        public override void Configuration()
        {
            this.HasName = false;
            this.HasDescription = false;
        }
    }
}
