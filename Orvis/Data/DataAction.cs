using Orvis.Application.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Data
{
    public partial class DataAction
    {
        private Data.Query oQuery;
        public DataAction(BaseFacede baseFacede) { oQuery = new Data.Query(); }

        private string InsertSqlWithOutput = "INSERT INTO [TABLENAME]([COLUMNS]) VALUES([VALUES]) SELECT SCOPE_IDENTITY()";
        private string UpdateSql = "UPDATE [TABLENAME] SET [VALUES] WHERE [WHERE]";
        private string DeleteUpdateSql = "UPDATE [TABLENAME] SET IsDeleted = 1 WHERE [WHERE]";
        private string DeleteSql = "DELETE FROM [TABLENAME] WHERE [WHERE]";

        private bool IsReference(string propName)
        {
            return (propName.IndexOf("Orvis") > -1);
        }

        public T Insert<T>(Entity entity) where T : Entity
        {
            string sql = InsertSqlWithOutput.Replace("[TABLENAME]", entity.DB_TableName);
            string columns = "";
            string values = "";
            string tempParameter = "";
            List<System.Reflection.PropertyInfo> listProps = new List<PropertyInfo>();
            SqlCommand cmd = new SqlCommand();
            string nextFunction = "";
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (this.HasProcess(entity, prop))
                {
                    var data = prop.GetValue(entity, null);
                    if (data != null || (data.GetType().FullName.Contains("System.Int") && !data.Equals(0)) )
                    {
                        if (this.IsReference(prop.PropertyType.FullName))
                            columns += "[" + prop.Name + "ID],";
                        else
                            columns += "[" + prop.Name + "],";
                        values += "@" + prop.Name + ",";
                        tempParameter = "@" + prop.Name;
                        if (prop.Name.Equals("DateCreated") || prop.Name.Equals("DateUpdated"))
                            cmd.Parameters.Add(tempParameter, System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                        else
                        {
                            if (this.IsReference(prop.PropertyType.FullName))
                            {
                                var newdata = data.GetType().GetProperty("ID").GetValue(data, null);
                                if (Convert.ToInt32(newdata) == 0)
                                    nextFunction = "Insert";
                                else
                                    nextFunction = "Update";
                                MethodInfo generic = this.GetType().GetMethod(nextFunction).MakeGenericMethod(Type.GetType(prop.PropertyType.FullName));
                                var returnValue = generic.Invoke(this, new object[] { prop.GetValue(entity, null) });
                                prop.SetValue(entity, returnValue, null);
                                cmd.Parameters.Add(tempParameter, System.Data.SqlDbType.Int).Value = data.GetType().GetProperty("ID").GetValue(data, null);
                            }
                            else
                                cmd.Parameters.Add(tempParameter, this.GetSQLType(prop.PropertyType.FullName)).Value = data;
                        }
                    }
                }
                else
                {
                    if (prop.PropertyType.Name.Contains("List") && prop.PropertyType.FullName.Contains("Orvis"))
                    {
                        if (prop.GetValue(entity, null) != null)
                            listProps.Add(prop);
                    }
                }
            }

            if (!entity.CanBeDeleted)
            {
                columns += "[IsDeleted],";
                values += "0,";
            }

            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            sql = sql.Replace("[COLUMNS]", columns).Replace("[VALUES]", values);
            cmd.CommandText = sql;

            entity.ID = this.oQuery.ExecuteNonQueryWithOutput(cmd);
            cmd.Dispose();
            cmd = null;

            foreach (var list in listProps)
            {
                IList oTheList = list.GetValue(entity, null) as IList;
                sql = "INSERT INTO " + entity.DB_TableName + oTheList[0].GetType().Name + "(" + entity.GetType().Name + "ID," + oTheList[0].GetType().Name + "ID) VALUES ";
                for (int i = 0; i < oTheList.Count; i++)
                {

                    var newdata = oTheList[i].GetType().GetProperty("ID").GetValue(oTheList[i], null);
                    if (Convert.ToInt32(newdata) == 0)
                    {
                        MethodInfo generic = this.GetType().GetMethod("Insert").MakeGenericMethod(Type.GetType(oTheList[i].GetType().FullName));
                        var returnValue = generic.Invoke(this, new object[] { oTheList[i] });
                        oTheList[i] = returnValue;
                    }
                    sql += "(" + entity.ID + "," + oTheList[i].GetType().GetProperty("ID").GetValue(oTheList[i], null) + "),";
                }
                sql = sql.Substring(0, sql.Length - 1);
                this.oQuery.ExecuteNonQuery(sql);
            }

            return (T)entity;
        }

        public T Update<T>(Entity entity) where T : Entity
        {
            string sql = UpdateSql.Replace("[TABLENAME]", entity.DB_TableName);
            string values = "";
            string tempParameter = "";
            List<System.Reflection.PropertyInfo> listProps = new List<PropertyInfo>();
            SqlCommand cmd = new SqlCommand();
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (this.HasProcess(entity, prop))
                {
                    var data = prop.GetValue(entity, null);
                    if (data != null)
                    {
                        if (this.IsReference(prop.PropertyType.FullName))
                            values += "[" + prop.Name + "ID] = ";
                        else
                            values += "[" + prop.Name + "] = ";
                        tempParameter = "@" + prop.Name + ",";
                        if (prop.Name.Equals("DateUpdated"))
                            cmd.Parameters.Add(tempParameter, System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                        else
                        {
                            if (this.IsReference(prop.PropertyType.FullName))
                            {
                                cmd.Parameters.Add(tempParameter, System.Data.SqlDbType.Int).Value = data.GetType().GetProperty("ID").GetValue(data, null);
                                MethodInfo generic = this.GetType().GetMethod("Update").MakeGenericMethod(Type.GetType(prop.PropertyType.FullName));
                                var returnValue = generic.Invoke(this, new object[] { prop.GetValue(entity, null) });
                                prop.SetValue(entity, returnValue, null);
                            }
                            else
                                cmd.Parameters.Add(tempParameter, this.GetSQLType(prop.PropertyType.FullName)).Value = data;
                        }
                    }
                }
                else
                {
                    if (prop.PropertyType.Name.Contains("List") && prop.PropertyType.FullName.Contains("Orvis"))
                    {
                        if (prop.GetValue(entity, null) != null)
                            listProps.Add(prop);
                    }
                }


                values = values.Substring(0, values.Length - 1);
                sql = sql.Replace("[VALUES]", values);
                cmd.CommandText = sql;

                this.oQuery.ExecuteNonQuery(cmd);
                cmd.Dispose();
                cmd = null;

                foreach (var list in listProps)
                {
                    IList oTheList = list.GetValue(entity, null) as IList;
                    sql = "DELETE FROM " + entity.DB_TableName + oTheList[0].GetType().Name + " WHERE " + entity.GetType().Name + "ID = " + entity.ID;
                    this.oQuery.ExecuteNonQuery(sql);
                    sql = "INSERT INTO " + entity.DB_TableName + oTheList[0].GetType().Name + "(" + entity.GetType().Name + "ID," + oTheList[0].GetType().Name + "ID) VALUES ";
                    for (int i = 0; i < oTheList.Count; i++)
                    {
                        var newdata = oTheList[i].GetType().GetProperty("ID").GetValue(oTheList[i], null);
                        if (Convert.ToInt32(newdata) == 0)
                        {
                            MethodInfo generic = this.GetType().GetMethod("Insert").MakeGenericMethod(Type.GetType(oTheList[i].GetType().FullName));
                            var returnValue = generic.Invoke(this, new object[] { oTheList[i] });
                            oTheList[i] = returnValue;
                        }
                        sql += "(" + entity.ID + "," + oTheList[i].GetType().GetProperty("ID").GetValue(oTheList[i], null) + "),";
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    this.oQuery.ExecuteNonQuery(sql);
                }
            }

            return (T)entity;
        }

        public bool Delete(Entity entity)
        {
            string sql = "";
            if (entity.CanBeDeleted)
                sql = this.DeleteSql;
            else
                sql = this.DeleteUpdateSql;
            sql = sql.Replace("[TABLENAME]", entity.DB_TableName);
            string tempsql = "";
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.PropertyType.Name.Contains("List") && prop.PropertyType.FullName.Contains("Orvis"))
                {
                    IList oTheList = prop.GetValue(entity, null) as IList;
                    for (int i = 0; i < oTheList.Count; i++)
                    {
                        if (prop.GetValue(entity, null) != null)
                        {
                            tempsql = "DELETE FROM " + entity.DB_TableName + oTheList[0].GetType().Name + " WHERE " + entity.GetType().Name + "ID = " + entity.ID;
                            this.oQuery.ExecuteNonQuery(tempsql);
                        }
                    }
                }
            }
            sql = sql.Replace("[WHERE]", " ID = " + entity.ID);
            return this.oQuery.ExecuteNonQuery(sql);
        }

        public K GetEntityList<T, K>(Data.Framework.SearchEngine search) where T : Entity, new() where K : EntityList<T>, new()
        {
            return this.GetEntities<T, K>(this.oQuery.GetDataTable(this.GetSql<T>(search, new T())));
        }

        public K GetEntityList<T, K>(string sql) where T : Entity, new() where K : EntityList<T>, new()
        {
            return this.GetEntities<T, K>(this.oQuery.GetDataTable(sql));
        }

        public K GetEntityList<T, K>(SqlCommand command) where T : Entity, new() where K : EntityList<T>, new()
        {
            return this.GetEntities<T, K>(this.oQuery.GetDataTable(command));
        }

        public DataTable GetDataTable(SqlCommand command)
        {
            return this.oQuery.GetDataTable(command);
        }

        public SqlCommand GetSql<T>(Data.Framework.SearchEngine search) where T : Entity, new()
        {
            return this.GetSql<T>(search, new T());
        }

        private SqlCommand GetSql<T>(Data.Framework.SearchEngine search, T entity) where T : Entity, new()
        {
            SqlCommand cmd = new SqlCommand();
            string sql = "";
            sql = "SELECT ";
            if (search.ExistColumns.Count > 0)
            {
                foreach (var item in search.ExistColumns)
                {
                    if (item.IndexOf(".") == -1)
                        sql += entity.DB_TableName + ".";
                    sql += item;
                    if (item != search.ExistColumns.Last())
                        sql += ",";
                }
            }
            else if (search.NonExistColumns.Count > 0)
            {
                var props = entity.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (this.HasProcess(entity, prop, false))
                    {
                        if (!search.NonExistColumns.Contains(prop.Name))
                        {
                            sql += entity.DB_TableName + "." + prop.Name;
                            if (this.IsReference(prop.PropertyType.FullName))
                                sql += "ID";
                            if (prop != props.Last())
                                sql += ",";
                        }
                    }
                }
            }
            else
            {
                var props = entity.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (this.HasProcess(entity, prop, false))
                    {
                        sql += entity.DB_TableName + "." + prop.Name;
                        if (this.IsReference(prop.PropertyType.FullName))
                            sql += "ID";
                        if (prop != props.Last())
                            sql += ",";
                    }
                }
            }

            sql += " FROM " + entity.DB_TableName + " ";

            foreach (var item in search.Joins)
            {
                sql += " " + item + " ";
            }

            sql += " WHERE ";
            if (entity.CanBeDeleted)
                sql += " 1 = 1 ";
            else
                sql += " IsDeleted = 0 ";

            foreach (var item in search.Filters)
            {
                switch (item.Constraint)
                {
                    case Framework.FilterConstraint.OR:
                        sql += " OR ";
                        break;
                    case Framework.FilterConstraint.AND:
                    default:
                        sql += " AND ";
                        break;
                }

                sql += item.Column + " ";

                switch (item.Comparison)
                {
                    case Framework.FilterComparison.NonEquals:
                        sql += " <> @" + item.Column.Replace(".", "_");
                        cmd.Parameters.Add("@" + item.Column.Replace(".", "_"), this.GetSQLType(item.Parameter.GetType().FullName)).Value = item.Parameter;
                        break;
                    case Framework.FilterComparison.Greater:
                        sql += " > @" + item.Column.Replace(".", "_");
                        cmd.Parameters.Add("@" + item.Column.Replace(".", "_"), this.GetSQLType(item.Parameter.GetType().FullName)).Value = item.Parameter;
                        break;
                    case Framework.FilterComparison.GreateEquals:
                        sql += " >=  @" + item.Column.Replace(".", "_");
                        cmd.Parameters.Add("@" + item.Column.Replace(".", "_"), this.GetSQLType(item.Parameter.GetType().FullName)).Value = item.Parameter;
                        break;
                    case Framework.FilterComparison.Small:
                        sql += " <  @" + item.Column.Replace(".", "_");
                        cmd.Parameters.Add("@" + item.Column.Replace(".", "_"), this.GetSQLType(item.Parameter.GetType().FullName)).Value = item.Parameter;
                        break;
                    case Framework.FilterComparison.SmallEquals:
                        sql += " <=  @" + item.Column.Replace(".", "_");
                        cmd.Parameters.Add("@" + item.Column.Replace(".", "_"), this.GetSQLType(item.Parameter.GetType().FullName)).Value = item.Parameter;
                        break;
                    case Framework.FilterComparison.In:
                        sql += " IN ( " + item.Parameter + " ) ";
                        break;
                    case Framework.FilterComparison.NotIn:
                        sql += " NOT IN ( " + item.Parameter + " ) ";
                        break;
                    case Framework.FilterComparison.Bettween:
                        sql += " Between @" + item.Column.Replace(".", "_") + "_P1 AND @" + item.Column.Replace(".", "_") + "_P2 ";
                        cmd.Parameters.Add("@" + item.Column.Replace(".", "_") + "_P1", this.GetSQLType(item.Parameter.GetType().FullName)).Value = item.Parameter;
                        cmd.Parameters.Add("@" + item.Column.Replace(".", "_") + "_P2", this.GetSQLType(item.Parameter.GetType().FullName)).Value = item.Parameter2;
                        break;
                    case Framework.FilterComparison.Equals:
                    default:
                        sql += " = @" + item.Column.Replace(".", "_");
                        cmd.Parameters.Add("@" + item.Column.Replace(".", "_"), this.GetSQLType(item.Parameter.GetType().FullName)).Value = item.Parameter;
                        break;
                }
                sql += " ";
            }

            cmd.CommandText = sql;
            return cmd;
        }

        private bool HasProcess(Entity entity, System.Reflection.PropertyInfo prop, bool IDControl = true)
        {
            if (IDControl)
            {
                if (prop.Name.Equals("ID"))
                    return false;
            }
            if (prop.Name.Equals("Name") && !entity.HasName)
                return false;
            if (prop.Name.Equals("Description") && !entity.HasDescription)
                return false;
            if (prop.Name.Equals("UserCreated") && !entity.HasUserCreated)
                return false;
            if (prop.Name.Equals("UserUpdated") && !entity.HasUserUpdated)
                return false;
            if (prop.Name.Equals("DateCreated") && !entity.HasDateCreated)
                return false;
            if (prop.Name.Equals("DateUpdated") && !entity.HasDateUpdated)
                return false;
            return prop.CanWrite;
        }

        private SqlDbType GetSQLType(string type)
        {
            switch (type)
            {
                case "System.UInt64":
                case "System.Int64":
                    return SqlDbType.BigInt;
                case "byte[]":
                    return SqlDbType.VarBinary;
                case "System.Boolean":
                    return SqlDbType.Bit;
                case "System.String":
                    return SqlDbType.VarChar;
                case "System.DateTime":
                    return SqlDbType.DateTime;
                case "System.Decimal":
                    return SqlDbType.Money;
                case "System.Int16":
                case "System.UInt16":
                    return SqlDbType.SmallInt;
                case "System.Byte":
                case "System.SByte":
                    return SqlDbType.TinyInt;
                case "System.Char":
                    return SqlDbType.Char;
                case "System.Double":
                case "System.Single":
                    return SqlDbType.Float;
                case "System.UInt32":
                case "System.Int32":
                    return SqlDbType.Int;
                default:
                    return SqlDbType.Int;
            }
        }
    }
}
