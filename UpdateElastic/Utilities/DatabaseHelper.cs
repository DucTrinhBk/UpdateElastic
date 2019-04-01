using System.Data.Common;
using System.Data.SqlClient;
namespace UpdateElastic.Utilities
{
    public class DatabaseHelper
    {
        private static DatabaseHelper instance;
        private SqlConnection connection;
        public delegate object OnRead(DbDataReader reader, string note);

        private DatabaseHelper()
        {

        }
        public static DatabaseHelper getInstance(string datasource, string database, string username, string password)
        {
            if(instance == null)
            {
                instance = new DatabaseHelper();
            }
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                   + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            
            instance.connection = new SqlConnection(connString);
            return instance;
        }
        public static DatabaseHelper getDefaultInstance()
        {
            string datasource = DataConfigs.getValue(DataConfigs.DATABASE_HOST);
            string database = DataConfigs.getValue(DataConfigs.DATABASE_NAME);
            string username = DataConfigs.getValue(DataConfigs.DATABASE_USER);
            string password = DataConfigs.getValue(DataConfigs.DATABASE_PASSWORD);
            return getInstance(datasource, database, username, password);
        }
        public void Open()
        {
            instance.connection.Open();
        }
        public void Close()
        {
            instance.connection.Close();
        }
        /**
         * Thực thi lệnh sql 
         * @sql: câu lệnh sql được thực thi vd: SELECT * FROM NhanVien
         * @onRead xử lý kết quả trả về
         * @note: ghi chú,có thể truyền biến số để bổ dung cho onRead
         * */
        public object execute(string sql,OnRead onRead,string note)
        {
            SqlCommand command = instance.connection.CreateCommand();
            command.CommandText = sql;
            using (DbDataReader reader = command.ExecuteReader())
            {
                return onRead(reader,note);
            }
            
        }
        /**
         * Thực thi lệnh sql trả về cột đầu tiên của hàng đầu tiên
         * @sql: câu lệnh sql được thực thi vd: SELECT * FROM NhanVien
         * @onRead xử lý kết quả trả về
         * @note: ghi chú,có thể truyền biến số để bổ dung cho onRead
         * */
        public object execute(string sql)
        {
            SqlCommand command = instance.connection.CreateCommand();
            command.CommandText = sql;
            return command.ExecuteScalar();
        }
        public string getStringValue(DbDataReader reader,string columnName,string defaultValue)
        {
            return (reader.GetValue(reader.GetOrdinal(columnName))?? defaultValue).ToString();
        }
        public int getIntValue(DbDataReader reader, string columnName,int defaultValue)
        {
            
            return (int)(reader.GetValue(reader.GetOrdinal(columnName))?? defaultValue);
        }
        public float getFloatValue(DbDataReader reader, string columnName,float defaultValue)
        {
            return (float)(reader.GetValue(reader.GetOrdinal(columnName))?? defaultValue);
        }
        public long getLongValue(DbDataReader reader, string columnName,long defaultValue)
        {
            return (long)(reader.GetValue(reader.GetOrdinal(columnName))??defaultValue);
        }
        public double getDoubleValue(DbDataReader reader, string columnName,double defaultValue)
        {
            return reader.GetDouble(reader.GetOrdinal(columnName));
        }
        public bool getBoolValue(DbDataReader reader, string columnName,bool defaultValue)
        {
            return reader.GetBoolean(reader.GetOrdinal(columnName));
        }
}
}
