using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Base
{
    public class EntityList<T> : List<T>
    {

        public void Add(EntityList<T> list)
        {
            foreach (var item in list)
            {
                this.Add(item);
            }
        }

        public void Add(List<T> list)
        {
            foreach (var item in list)
            {
                this.Add(item);
            }
        }

        public void Remove(EntityList<T> list)
        {
            foreach (var item in list)
            {
                this.Remove(item);
            }
        }

        public void Remove(List<T> list)
        {
            foreach (var item in list)
            {
                this.Remove(item);
            }
        }

    }
}
