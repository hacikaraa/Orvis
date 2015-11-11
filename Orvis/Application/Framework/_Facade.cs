using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orvis.Data.Framework;
using Orvis.Application.Base;

namespace Orvis.Application.Framework
{
    public class Facade : Base.BaseFacede
    {
        public Category CreateCategory(Category category)
        {
            if (category != null && category.ID == 0 && !String.IsNullOrEmpty(category.Name))
            {
                return base.Data.Insert<Category>(category);
            }
            return null;
        }

        public Category UpdateCategory(Category category)
        {
            if (category != null && category.ID > 0 && !String.IsNullOrEmpty(category.Name))
            {
                return base.Data.Update<Category>(category);
            }
            return null;
        }

        public Category GetCategory(int ID)
        {
            return this.Data.GetEntityObject<Category>(ID);
        }

        public CategoryList GetCategories(SearchEngine search = null, int baseCategory = 0)
        {
            if (search == null)
                search = new SearchEngine();
            if (baseCategory > 0)
                search.Filters.Add("BaseCategoryID", baseCategory);
            return (CategoryList)this.Data.GetEntityList<Category,CategoryList>(search);
        }

        public Property CreateProperty(Property property)
        {
            if (property != null && property.ID == 0 && !String.IsNullOrEmpty(property.Name))
                return this.Data.Insert<Property>(property);
            return null;
        }

        public Property UpdateProperty(Property property)
        {
            if (property != null && property.ID > 0 && !String.IsNullOrEmpty(property.Name))
                return this.Data.Update<Property>(property);
            return null;
        }

        public Property GetProperty(int id)
        {
            return this.Data.GetEntityObject<Property>(id);
        }

        public PropertyList GetProperties(SearchEngine search = null)
        {
            if (search == null)
                search = new SearchEngine();
            return this.Data.GetEntityList<Property, PropertyList>(search);
        }

        public PropertyValue CreatePropertyValue(PropertyValue propertyValue)
        {
            if (propertyValue != null && propertyValue.ID == 0 && propertyValue.Property != null && propertyValue.Value != null)
            {
                return this.Data.Insert<PropertyValue>(propertyValue);
            }
            return null;
        }

        public PropertyValue UpdatePropertyValue(PropertyValue propertyValue)
        {
            if (propertyValue != null && propertyValue.ID > 0 && propertyValue.Property != null && propertyValue.Value != null)
            {
                return this.Data.Insert<PropertyValue>(propertyValue);
            }
            return null;
        }

        public PropertyValue GetPropertyValue(int propertyID)
        {
            SearchEngine search = new SearchEngine();
            search.Filters.Add("PropertyID", propertyID);
            return this.Data.GetEntityObject<PropertyValue>(this.Data.GetDataTable(this.Data.GetSql<PropertyValue>(search)));
        }

        public PropertyValueList GetPropertyValues(SearchEngine search = null)
        {
            if (search == null)
                search = new SearchEngine();
            return this.Data.GetEntityList<PropertyValue, PropertyValueList>(search);
        }

        public Image CreateImage(Image image)
        {
            if (image != null && image.ID == 0 && ( !String.IsNullOrEmpty(image.Url) || !String.IsNullOrEmpty(image.ImageBase64)))
                return this.Data.Insert<Image>(image);
            return null;
        }

        public Image UpdateImage(Image image)
        {
            if (image != null && image.ID > 0 && (!String.IsNullOrEmpty(image.Url) || !String.IsNullOrEmpty(image.ImageBase64)))
                return this.Data.Update<Image>(image);
            return null;
        }

        public Image GetImage(int id)
        {
            return this.Data.GetEntityObject<Image>(id);
        }

        public ImageList GetImages(SearchEngine search = null)
        {
            if (search == null)
                search = new SearchEngine();
            return this.Data.GetEntityList<Image, ImageList>(search);
        }

        public Language CreateLanguage(Language language)
        {
            if (language != null && language.ID == 0 && !String.IsNullOrEmpty(language.Name))
            {
                return this.Data.Insert<Language>(language);
            }
            return null;
        }

        public Language UpdateLanguage(Language language)
        {
            if (language != null && language.ID > 0 && !String.IsNullOrEmpty(language.Name))
            {
                return this.Data.Update<Language>(language);
            }
            return null;
        }

        public LanguageList GetLanguages(SearchEngine search = null)
        {
            if (search == null)
                search = new SearchEngine();
            return this.Data.GetEntityList<Language, LanguageList>(search);
        }

        public Parameter CreateParameter(Parameter parameter)
        {
            if (parameter != null && parameter.ID == 0 && !String.IsNullOrEmpty(parameter.Alias))
            {
                return this.Data.Insert<Parameter>(parameter);
            }
            return null;
        }

        public Parameter UpdateParameter(Parameter parameter)
        {
            if (parameter != null && parameter.ID > 0 && !String.IsNullOrEmpty(parameter.Alias))
            {
                return this.Data.Update<Parameter>(parameter);
            }
            return null;
        }

        public ParameterList GetParameters(SearchEngine search = null)
        {
            if (search == null)
                search = new SearchEngine();
            return this.Data.GetEntityList<Parameter, ParameterList>(search);
        }

        public ParameterValue CreateParameterValue(ParameterValue parameterValue)
        {
            if (parameterValue != null && parameterValue.ID == 0 && !String.IsNullOrEmpty(parameterValue.Value))
            {
                return this.Data.Insert<ParameterValue>(parameterValue);
            }
            return null;
        }

        public ParameterValue UpdateParameterValue(ParameterValue parameterValue)
        {
            if (parameterValue != null && parameterValue.ID > 0 && !String.IsNullOrEmpty(parameterValue.Value))
            {
                return this.Data.Update<ParameterValue>(parameterValue);
            }
            return null;
        }

        public ParameterValueList GetParameterValues(SearchEngine search = null)
        {
            if (search == null)
                search = new SearchEngine();
            return this.Data.GetEntityList<ParameterValue, ParameterValueList>(search);
        }

    }
}
