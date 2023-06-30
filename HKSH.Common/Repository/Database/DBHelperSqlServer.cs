using System.Configuration;
using System.Data.SqlClient;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// DBHelper SqlServer
    /// </summary>
    public class DBHelperSqlServer
    {
        /// <summary>
        /// The connection string default SqlServer
        /// </summary>
        private static string connectionString = ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;

        /// <summary>
        /// The connection default SqlServer ConnectionString 
        /// </summary>
        private static SqlConnection connection = new(connectionString);

        /// <summary>
        /// Initializes a new instance of the <see cref="DBHelperSqlServer"/> class.
        /// </summary>
        public DBHelperSqlServer()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DBHelperSqlServer"/> class.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        public DBHelperSqlServer(string dbName)
        {
            if (!string.IsNullOrEmpty(dbName))
            {
                connectionString = ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
        }

        /// <summary>
        /// 传入SQL语句,返回执行语句后受影响的行数count
        /// </summary>
        /// <param name="sqlString">传入的SQL语句字符串，insert、delete、update</param>
        /// <returns>受影响的行数count</returns>
        public int ExecuteSql(string sqlString)
        {
            //传入调用该方法时传入的SQL语句【sqlString】，和数据库打开连接【connection】，得到一个对数据库执行的命令语句
            using SqlCommand command = new(sqlString, connection);
            try
            {
                connection.Open();//打开数据库连接
                int count = command.ExecuteNonQuery();//对【command】连接执行后返回受影响的行数
                return count;//返回执行语句后受影响的行数count
            }
            catch (SqlException ex)//捕获SQL返回的警告或异常
            {
                throw ex;
            }
            //即使trycatch中有return，finally中的代码依然会继续执行
            finally
            {
                connection.Close();//不管是try还是catch，最后都要关闭与数据库之间的连接
            }
        }

        ///<summary>
        ///传入带参数的SQL语句,返回执行语句后受影响的行数count
        /// </summary>
        /// <param name="SQLString">传入的SQL语句字符串，insert、delete、update</param>
        /// <param name="parm">参数化</param>
        /// <returns>受影响的行数count</returns>
        public int ExecuteSqlWithParm(string SQLString, SqlParameter[] parm)
        {
            using SqlCommand command = new(SQLString, connection);
            try
            {
                connection.Open();
                //command.Parameters.Add(parm[0]);// Parameters.Add：将指定的SqlParameter对象添加到SqlParameterCollection
                //command.Parameters.Add(parm[1]);
                command.Parameters.AddRange(parm);//Parameters.AddRange：向SqlParameter的末尾添加一个SqlParameterCollection的数组值
                int count = command.ExecuteNonQuery();
                return count;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}