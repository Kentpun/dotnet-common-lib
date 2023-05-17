using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// DbOperate
    /// </summary>
    public class DbSqlOperate
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString = string.Empty;

        /// <summary>
        /// The replace string
        /// </summary>
        private readonly string replaceStr = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSqlOperate"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="replaceStr">The replace string.</param>
        public DbSqlOperate(string connectionString, string replaceStr = "_")
        {
            this.connectionString = connectionString;
            this.replaceStr = replaceStr;
        }

        /// <summary>
        /// Excutes the SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        public List<T> ExcuteSQL<T>(string sql)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();
            using var command = conn.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            List<T> ts = new();
            using (var result = command.ExecuteReader())
            {
                while (result.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        string ropertyName = prop.Name;
                        string newPropertyName = ropertyName.Replace(replaceStr, " ");
                        if (!Equals(result[newPropertyName], DBNull.Value))
                        {
                            prop.SetValue(obj, result[newPropertyName], null);
                        }
                    }
                    ts.Add(obj);
                }
            }
            conn.Close();
            return ts;
        }
    }
}