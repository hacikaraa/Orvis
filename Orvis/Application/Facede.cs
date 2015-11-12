using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orvis.Application.Framework;

namespace Orvis.Application
{
    public partial class Facede
    {
        public Facede()
        {
            this.BindStaticValues();
        }

        public Language ActiveLanguage { get; set; }

        public string GetTitle(string alias)
        {
            Parameter p = Container.Parameters.Where(w => w.Alias == alias).FirstOrDefault();
            if (p == null)
            {
                p = new Parameter();
                p.Alias = alias;
                foreach (var lang in Container.Languages)
                    p.Values.Add(alias, lang);
                this.framework.CreateParameter(p);
                Container.Parameters.Add(p);
            }
            return p.Values.Where(w=>w.Language.ID == this.ActiveLanguage.ID).FirstOrDefault().Value;
        }

        public void BindStaticValues()
        {
            if (Container.Languages == null)
            {
                Container.Languages = this.Framework.GetLanguages();
            }
            if (Container.Parameters == null)
            {
                Container.Parameters = this.Framework.GetParameters();
            }

            #if DEBUG
                Container.CanBeConfigured = true;
            #else
                Container.CanBeConfigured = false;
            #endif

        }




    }
}
