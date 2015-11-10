using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Framework
{
    public class Image : Base.Entity
    {
        public string Url { get; set; }

        public string ImageBase64 { get; set; }

        public bool HasUrl
        {
            get { return !String.IsNullOrEmpty(this.Url); }
        }

        public bool HasImageBase64
        {
            get { return !String.IsNullOrEmpty(this.ImageBase64); }
        }

        public override void Configuration()
        {
            this.HasName = false;
            this.HasDescription = false;
        }
    }
}
