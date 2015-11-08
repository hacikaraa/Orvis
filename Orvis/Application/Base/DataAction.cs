using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Base
{
    public partial class DataAction
    {
        private Data.Query oQuery;
        public DataAction(BaseFacede baseFacede) { oQuery = new Data.Query(); }

        private string InsertSqlWithOutput = "INSERT INTO [TABLENAME]([COLUMNS]) VALUES([VALUES]) SELECT SCOPE_IDENTITY()";
        private string UpdateSql = "UPDATE [TABLENAME] SET [VALUES] WHERE [WHERE]";

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
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (this.HasProcess(entity, prop))
                {
                    var data = prop.GetValue(entity, null);
                    if (data != null)
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
                                {
                                    MethodInfo generic = this.GetType().GetMethod("Insert").MakeGenericMethod(Type.GetType(prop.PropertyType.FullName));
                                    var returnValue = generic.Invoke(this, new object[] { prop.GetValue(entity, null) });
                                    prop.SetValue(entity, returnValue, null);
                                }
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

        private bool HasProcess(Entity entity, System.Reflection.PropertyInfo prop, bool IDControl = true)
        {
            if (IDControl)
            {
                if (prop.Name.Equals("ID"))
                    return false;
            }
            if (prop.Name.Equals("Name") && !entity.HasName)
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
