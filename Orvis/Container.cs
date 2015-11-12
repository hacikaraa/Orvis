using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis
{
    public class Container
    {
        public static string ConnectionString { get; set; }

        public static bool CanBeConfigured { get; set; }

        public static Application.Framework.ParameterList Parameters { get; set; }

        public static Application.Framework.LanguageList Languages { get; set; }
      

        public static void Exception(Exception ex,string Data)
        {
            if (CanBeConfigured)
            {
                throw new System.Exception(Data, ex);
            }
            else
            {

            }
        }
    }
}
