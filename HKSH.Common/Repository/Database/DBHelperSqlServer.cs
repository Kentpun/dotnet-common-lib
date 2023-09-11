using System.Data.SqlClient;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// DBHelper SqlServer
    /// </summary>
    public class DBHelperSqlServer
    {
        /// <summary>
        /// The connection default SqlServer ConnectionString
        /// </summary>
        private readonly SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBHelperSqlServer"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DBHelperSqlServer(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// 傳入SQL語句,返回執行語句後受影響的行數count
        /// </summary>
        /// <param name="sqlString">傳入的SQL語句字符串，insert、delete、update</param>
        /// <returns>受影響的行數count</returns>
        public int ExecuteSql(string sqlString)
        {
            //傳入調用該方法時傳入的SQL語句【sqlString】，和數據庫打開連接【connection】，得到一個對數據庫執行的命令語句
            using SqlCommand command = new(sqlString, connection);
            try
            {
                connection.Open();//打開數據庫連接
                int count = command.ExecuteNonQuery();//對【command】連接執行後返回受影響的行數
                return count;//返回執行語句後受影響的行數count
            }
            catch (SqlException ex)//捕獲SQL返回的警告或異常
            {
                throw ex;
            }
            //即使trycatch中有return，finally中的代碼依然會繼續執行
            finally
            {
                connection.Close();//不管是try還是catch，最後都要關閉與數據庫之間的連接
            }
        }

        ///<summary>
        ///傳入帶參數的SQL語句,返回執行語句後受影響的行數count
        /// </summary>
        /// <param name="SQLString">傳入的SQL語句字符串，insert、delete、update</param>
        /// <param name="parm">參數化</param>
        /// <returns>受影響的行數count</returns>
        public int ExecuteSqlWithParm(string SQLString, SqlParameter[] parm)
        {
            using SqlCommand command = new(SQLString, connection);
            try
            {
                connection.Open();
                // Parameters.Add：將指定的SqlParameter對象添加到SqlParameterCollection
                command.Parameters.AddRange(parm);//Parameters.AddRange：向SqlParameter的末尾添加一個SqlParameterCollection的數組值
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