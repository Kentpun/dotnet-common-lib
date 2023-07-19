using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// DatabaseFacade Extension
    /// </summary>
    public static class DatabaseFacadeExtension
    {
        /// <summary>
        /// SQLs the query with timeout.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="facade">The facade.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static IEnumerable<T> SqlQueryWithTimeout<T>(this DatabaseFacade facade, string sql, object[] parameters, int timeout = 0) where T : class, new()
        {
            DataTable dt = SqlLoad(facade, sql, parameters, timeout);
            return dt.ToEnumerable<T>();
        }

        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="facade">The facade.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static IEnumerable<T> SqlQuery<T>(this DatabaseFacade facade, string sql, params object[] parameters) where T : class, new()
        {
            DataTable dt = SqlLoad(facade, sql, parameters);
            return dt.ToEnumerable<T>();
        }

        /// <summary>
        /// Converts to enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static IEnumerable<T> ToEnumerable<T>(this DataTable dt) where T : class, new()
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            T[] ts = new T[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                T t = new();
                foreach (PropertyInfo p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                    {
                        p.SetValue(t, row[p.Name], null);
                    }
                }
                ts[i] = t;
                i++;
            }
            return ts;
        }

        /// <summary>
        /// SQLs the load.
        /// </summary>
        /// <param name="facade">The facade.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static DataTable SqlLoad(this DatabaseFacade facade, string sql, object[] parameters, int timeout = 0)
        {
            DbCommand cmd = CreateCommand(facade, sql, out DbConnection conn, timeout, parameters);
            if (timeout > 0)
            {
                cmd.CommandTimeout = timeout;
            }

            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            reader.Close();
            conn.Close();
            return dt;
        }

        /// <summary>
        /// SQLs the scalar.
        /// </summary>
        /// <param name="facade">The facade.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static object? SqlScalar(this DatabaseFacade facade, string sql, object[] parameters, int timeout = 0)
        {
            DbCommand cmd = CreateCommand(facade, sql, out DbConnection conn, timeout, parameters);
            if (timeout > 0)
            {
                cmd.CommandTimeout = timeout;
            }
            var obj = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            conn.Close();
            return obj;
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="facade">The facade.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="dbConn">The database connection.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        private static DbCommand CreateCommand(DatabaseFacade facade, string sql, out DbConnection dbConn, int timeout, object[] parameters)
        {
            DbConnection conn = facade.GetDbConnection();
            dbConn = conn;
            conn.Open();
            DbCommand cmd = conn.CreateCommand();
            if (timeout > 0)
            {
                cmd.CommandTimeout = timeout;
            }
            if (facade.IsSqlServer())
            {
                cmd.CommandText = sql;
                CombineParams(ref cmd, parameters);
            }
            return cmd;
        }

        /// <summary>
        /// Combines the parameters.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameters">The parameters.</param>
        private static void CombineParams(ref DbCommand command, params object[] parameters)
        {
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    if (!parameter.ParameterName.Contains('@'))
                    {
                        parameter.ParameterName = $"@{parameter.ParameterName}";
                    }

                    command.Parameters.Add(parameter);
                }
            }
        }
    }
}