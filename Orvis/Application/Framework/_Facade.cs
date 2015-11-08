using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Category GetCategory(int ID)
        {
            return this.Data.GetEntityObject<Category>(ID);
        }

        public CategoryList GetCategories(FilterList filters)
        {
            //sql oluşurma yazılacak
            return (CategoryList)this.Data.GetEntity<Category>(new System.Data.DataTable());
        }

    }
}
