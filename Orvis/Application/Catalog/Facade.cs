using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orvis.Application.Framework;
using Orvis.Data.Framework;

namespace Orvis.Application.Catalog
{
    public class Facade : Base.BaseFacede
    {
        public Manufacturer CreateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer != null && manufacturer.ID == 0 && !String.IsNullOrEmpty(manufacturer.Name))
            {
                return this.Data.Insert<Manufacturer>(manufacturer);
            }
            return null;
        }

        public Manufacturer UpdateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer != null && manufacturer.ID > 0 && !String.IsNullOrEmpty(manufacturer.Name))
            {
                return this.Data.Update<Manufacturer>(manufacturer);
            }
            return null;
        }

        public ManufacturerList UpdateManufacturerList(SearchEngine search = null)
        {
            if (search == null)
                search = new SearchEngine();
            return this.Data.GetEntityList<Manufacturer,ManufacturerList>(search);
        }

        public Item CreateItem(Item item)
        {
            if (item != null && item.ID == 0 && !String.IsNullOrEmpty(item.Name))
            {
                this.Data.Insert<Item>(item);
            }
            return null;
        }

        public Item UpdateItem(Item item)
        {
            if (item != null && item.ID > 0 && !String.IsNullOrEmpty(item.Name))
            {
                this.Data.Update<Item>(item);
            }
            return null;
        }

        public Item GetItem(int id)
        {
            SearchEngine search = new SearchEngine();
            search.Filters.Add("ID", id);
            return this.Data.GetEntityObject<Item>(id);
        }

        public ItemList GetItemList(SearchEngine search = null)
        {
            if (search == null)
                search = new SearchEngine();
            return this.Data.GetEntityList<Item,ItemList>(search);
        }

    }
}
