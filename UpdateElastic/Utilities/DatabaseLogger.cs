using System;
using UpdateElastic.Models;
namespace UpdateElastic.Utilities
{
    public class DatabaseLogger
    {
        private static DatabaseLogger instance;
        private DatabaseHelper helper;
        private DatabaseLogger()
        {

        }
        public static DatabaseLogger getInstance(DatabaseHelper helper)
        {
            if (instance == null)
            {
                instance = new DatabaseLogger();
            }
            instance.helper = helper??DatabaseHelper.getDefaultInstance();
            return instance;
        }
        public static DatabaseLogger getDefaultInstance()
        {
            return getInstance(DatabaseHelper.getDefaultInstance());
        }
        public int count(string column,string table,string where)
        {
            if (column == null) return 0;
            if (table == null) return 0;
            if (table.Equals("")) return 0;
            if (where == null) where = "1=1";
            if (where.Equals("")) where = "1=1";
            return (int)helper.execute("SELECT count(" + column + ") FROM " + table+" where "+where);
        }
        public void logTo(string table,object value)
        {
      
            if (table.Equals("ElasticUpdateServiceLog"))
            {
                var val = (ElasticUpdateServiceLog)value;
                var sql = string.Format(@"INSERT INTO ElasticUpdateServiceLog
                            ([Id]
                            ,[SessionId]
                            ,[TrangThai]
                            ,[ThoiGian]
                            ,[SoLuong]
                            ,[SoLuongThanhCong]
                            ,[GhiChu])
                            VALUES
                            ('{0}','{1}','{2}','{3}',{4},{5},'{6}')",Guid.NewGuid(),val.SessionId,val.TrangThai,val.ThoiGian,val.SoLuong,val.SoLuongThanhCong,val.GhiChu);
                DataLogger.getInstance().writeToFile(sql);
                helper.execute(sql);
            }
            if (table.Equals("ElasticUpdateLog"))
            {
                var val = (ElasticUpdateLog)value;
                var sql = string.Format(@"
                                        INSERT INTO ElasticUpdateLog
                                        (    [Id]
                                            ,[DataId]
                                            ,[ArticleId]
                                            ,[Action]
                                            ,[Time]
                                            ,[State])
                                         VALUES
                                               ('{0}','{1}',{2},'{3}','{4}','{5}')", Guid.NewGuid(), val.DataId,val.ArticleId,val.Action,val.Time,val.State );
                helper.execute(sql);
            }
        }
    }
}
