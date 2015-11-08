using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Orvis.Data
{
    public class Query
    {
        private SqlConnection conn;

        private bool Connect()
        {
            try
            {
                conn = new SqlConnection(Container.ConnectionString);
                conn.Open();
                return true;
            }
            catch (Exception)
            {
               
                return false;
            }
        }

        public object ExecuteScalar(string sql)
        {
            return this.ExecuteScalar(new SqlCommand(sql));
        }

        public object ExecuteScalar(SqlCommand command)
        {
            try
            {
                this.Connect();
                command.Connection = conn;
                return command.ExecuteScalar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool ExecuteNonQuery(string sql)
        {
            return this.ExecuteNonQuery(new SqlCommand(sql));
        }

        public bool ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                Connect();
                command.Connection = conn;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Container.Exception(ex, "Query/ExecuteNonQuery");
                return false;
            }
            finally { conn.Close(); }
        }

        public int ExecuteNonQueryWithOutput(string sql)
        {
            return this.ExecuteNonQueryWithOutput(new SqlCommand(sql));
        }

        public int ExecuteNonQueryWithOutput(SqlCommand command)
        {
            try
            {
                Connect();
                command.Connection = conn;
                var id = command.ExecuteScalar();
                return Convert.ToInt32(id);
            }
            catch (Exception ex)
            {
                Container.Exception(ex, "Query/ExecuteNonQueryWithOutput");
                return 0;
            }
            finally { conn.Close(); }
        }

        public System.Data.DataTable GetDataTable(string sql)
        {
            return this.GetDataTable(new SqlCommand(sql));
        }

        public System.Data.DataTable GetDataTable(SqlCommand command)
        {
            try
            {
                Connect();
                command.Connection = conn;
                SqlDataAdapter sdp = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sdp.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Container.Exception(ex, "Query/GetDataTable");
                return new DataTable();
            }
            finally { conn.Close(); }
        }
    }
}
