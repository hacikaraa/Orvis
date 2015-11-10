using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Catalog
{
    public class Item : Base.Entity
    {
        public string Code { get; set; }

        public string SecondName { get; set; }

        public string WarningText { get; set; }

        public Manufacturer Manufacturer { get; set; }

        private Framework.CategoryList categories;
        public Framework.CategoryList Categories
        {
            get
            {
                if (categories == null)
                {
                    Orvis.Data.Framework.SearchEngine search = new Data.Framework.SearchEngine();
                    search.ExistColumns.Add("Framework_Category.*");
                    search.Joins.Add(" INNER JOIN Catalog_ItemCategory ON Catalog_ItemCategory.CategoryID = Framework_Category.ID");
                    search.Filters.Add("Catalog_ItemCategory.ItemID", this.ID);
                    categories = this.Application.Framework.GetCategories(search);
                }
                return categories;
            }
        }

        private Framework.PropertyList properties;
        public Framework.PropertyList Properties
        {
            get
            {
                if (properties == null)
                {
                    Orvis.Data.Framework.SearchEngine search = new Data.Framework.SearchEngine();
                    search.ExistColumns.Add("Framework_Property.*");
                    search.Joins.Add(" INNER JOIN Catalog_ItemProperty ON Framework_Property.ID = Catalog_ItemProperty.PropertyID ");
                    search.Filters.Add("Catalog_ItemProperty.ItemID", this.ID);
                    properties = this.Application.Framework.GetProperties(search);
                }
                return properties;
            }
        }

        private Framework.PropertyValueList propertyValues;
        public Framework.PropertyValueList PropertyValues
        {
            get
            {
                if (propertyValues == null)
                {
                    Data.Framework.SearchEngine search = new Data.Framework.SearchEngine();
                    search.ExistColumns.Add("Framework_PropertyValue.*");
                    search.Joins.Add(" INNER JOIN  Catalog_ItemPropertyValue ON Catalog_ItemPropertyValue.PropertyValueID = Framework_PropertyValue.ID");
                    search.Filters.Add("Catalog_ItemPropertyValue.ItemID", this.ID);
                    propertyValues = this.Application.Framework.GetPropertyValues(search);
                }
                return propertyValues;
            }
        }

        private Framework.ImageList images;
        public Framework.ImageList Images
        {
            get
            {
                if (images == null)
                {
                    Orvis.Data.Framework.SearchEngine search = new Data.Framework.SearchEngine();
                    search.ExistColumns.Add("Framework_Image.*");
                    search.Joins.Add(" INNER JOIN Catalog_ItemImage ON Catalog_ItemImage.ImageID = Framework_Image.ID ");
                    search.Filters.Add("Catalog_ItemImage.ItemID", this.ID);
                    images = this.Application.Framework.GetImages(search);
                }
                return images;
            }
        }

        private Finance.TaxList taxes;
        public Finance.TaxList Taxes
        {
            get
            {
                if (taxes == null)
                {
                    Orvis.Data.Framework.SearchEngine search = new Data.Framework.SearchEngine();
                    search.ExistColumns.Add("Finance_Tax.*");
                    search.Joins.Add(" INNER JOIN Catalog_ItemTax ON Catalog_ItemTax.TaxID = Finance_Tax.ID");
                    search.Filters.Add("Catalog_ItemTax.ItemID", this.ID);
                    taxes = this.Application.Finance.GetTaxes(search);
                }
                return taxes;
            }
        }

        public Framework.PropertyValue GetPropertyValue(int propertyID)
        {
            return this.PropertyValues.Where(w => w.Property.ID == propertyID).FirstOrDefault();
        }


        public override void Configuration()
        {
           
        }
    }
}
