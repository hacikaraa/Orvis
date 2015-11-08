using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Base
{
    public partial class DataAction
    {
        private string ObjectSql = "SELECT * FROM [TABLENAME] WHERE ID = [@ID]";

        public List<T> GetEntity<T>(DataTable dt) where T : Application.Base.Entity, new()
        {
            List<T> list = new List<T>();
            try
            {
                T obj;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    obj = new T();
                    var queryProps = obj.GetType().GetProperties();
                    foreach (var prop in queryProps)
                    {
                        if (this.HasProcess(obj, prop, false))
                        {
                            if (this.IsReference(prop.PropertyType.FullName))
                            {
                                if (dt.Columns.Contains(prop.Name + "ID") && dt.Rows[i][prop.Name + "ID"] != DBNull.Value)
                                {
                                    MethodInfo generic = this.GetType().GetMethod("GetEntityObject").MakeGenericMethod(Type.GetType(prop.PropertyType.FullName));
                                    var returnValue = generic.Invoke(this, new object[] { this.oQuery.GetDataTable(ObjectSql.Replace("[TABLENAME]", obj.DB_TableName).Replace("[@ID]", dt.Rows[i][prop.Name + "ID"].ToString())) });
                                    prop.SetValue(obj, returnValue, null);
                                }
                            }
                            else
                            {
                                if (dt.Columns.Contains(prop.Name) && dt.Rows[i][prop.Name] != DBNull.Value)
                                {
                                    prop.SetValue(obj, dt.Rows[i][prop.Name], null);
                                }
                            }
                        }
                    }
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Container.Exception(ex, "DataAction/GetEntity");
                list = new List<T>();
            }
            return list;
        }

        public T GetEntityObject<T>(DataTable dt) where T : Application.Base.Entity, new()
        {
            T obj = new T();
            try
            {
                if (dt.Rows.Count == 1)
                {
                    var queryProps = obj.GetType().GetProperties();
                    foreach (var prop in queryProps)
                    {
                        if (this.IsReference(prop.PropertyType.FullName))
                        {
                            if (dt.Columns.Contains(prop.Name + "ID") && dt.Rows[0][prop.Name + "ID"] != DBNull.Value)
                            {
                                MethodInfo generic = this.GetType().GetMethod("GetEntityObject").MakeGenericMethod(Type.GetType(prop.PropertyType.FullName));
                                var returnValue = generic.Invoke(this, new object[] { this.oQuery.GetDataTable(ObjectSql.Replace("[TABLENAME]", obj.DB_TableName).Replace("[@ID]", dt.Rows[0][prop.Name + "ID"].ToString())) });
                                prop.SetValue(obj, returnValue, null);
                            }
                        }
                        else
                        {
                            if (dt.Columns.Contains(prop.Name) && dt.Rows[0][prop.Name] != DBNull.Value)
                            {
                                prop.SetValue(obj, dt.Rows[0][prop.Name], null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Container.Exception(ex, "DataAction/GetEntityObject");
                obj = new T();
            }
            return obj;
        }

        public T GetEntityObject<T>(int ID) where T : Application.Base.Entity, new()
        {
            T obj = new T();
            return this.GetEntityObject<T>(this.oQuery.GetDataTable(ObjectSql.Replace("[TABLENAME]", obj.DB_TableName).Replace("[@ID]", ID.ToString())));
        }
    }
}
