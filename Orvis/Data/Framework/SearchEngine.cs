using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Data.Framework
{
    public class SearchEngine
    {

        private List<string> eColumns;
        public List<string> ExistColumns
        {
            get
            {
                if (eColumns == null) eColumns = new List<string>();
                return eColumns;
            }
            set { eColumns = value; }
        }

        private List<string> neColumns;
        public List<string> NonExistColumns
        {
            get
            {
                if (neColumns == null) neColumns = new List<string>();
                return neColumns;
            }
            set { neColumns = value; }
        }

        private List<string> joins;
        public List<string> Joins
        {
            get
            {
                if (joins == null) joins = new List<string>();
                return joins;
            }
            set { joins = value; }
        }

        private FilterList filters;
        public FilterList Filters
        {
            get
            {
                if (filters == null) filters = new FilterList();
                return filters;
            }
            set { filters = value; }
        }
    }
}
