using System;
using System.Timers;
using UpdateElastic.Daoes;
using UpdateElastic.Utilities;
using UpdateElastic.Models;
using Newtonsoft.Json;

namespace UpdateElastic.Managers
{
    public class ProcessManager
    {
        private static ProcessManager instance;
        private Timer timer;
        private DataLogger logger;
        private DatabaseLogger dbLogger;
        private ElasticBranchDao elasticDao;
        private string sessionId;
        private ProcessManager()
        {
            timer = new Timer();
            logger = DataLogger.getInstance();
            elasticDao = new ElasticBranchDao();

        }
        public static ProcessManager getInstance(string sessionId)
        {
            if(instance == null)
            {
                instance = new ProcessManager();
            }
            instance.sessionId = sessionId;
            instance.logger = DataLogger.getInstance();
            instance.elasticDao = new ElasticBranchDao();
            instance.dbLogger = DatabaseLogger.getInstance(null);
            return instance;
        }
        public void startExecuteService(long interval)
        {
            instance.timer.Interval = interval;
            timer.Elapsed += new ElapsedEventHandler(execute);
            elasticDao.getDatabaseHelper().Open();
            ElasticUpdateServiceLog log = new ElasticUpdateServiceLog();
            log.SessionId = sessionId;
            log.SoLuong = 0;
            log.ThoiGian = DateTime.Now;
            log.SoLuongThanhCong = 0;
            log.GhiChu = "";
            log.TrangThai = "start";
            logger.writeToFile("start service "+dbLogger);
            dbLogger.logTo("ElasticUpdateServiceLog", log);
            elasticDao.getDatabaseHelper().Close();
            timer.Start();
        }
        public void execute(object sender, ElapsedEventArgs args)
        {
            try
            {
                elasticDao.getDatabaseHelper().Open();
                logger.writeToFile("");
                logger.writeToFile("ping: " + DateTime.Now.ToString());
                var log = (ElasticUpdateServiceLog)elasticDao.execute();
                log.SessionId = sessionId;
                dbLogger.logTo("ElasticUpdateServiceLog", log);
                logger.writeToFile("ping end at " + DateTime.Now.ToString());
                elasticDao.getDatabaseHelper().Close();
            }catch (Exception e)
            {
                logger.writeToFile(JsonConvert.SerializeObject(e));
            }
        }
        public void stopExecuteService()
        {
            elasticDao.getDatabaseHelper().Open();
            ElasticUpdateServiceLog log = new ElasticUpdateServiceLog();
            log.SessionId = sessionId;
            log.SoLuong = 0;
            log.ThoiGian = DateTime.Now;
            log.SoLuongThanhCong = 0;
            log.GhiChu = "";
            log.TrangThai = "stop";
            dbLogger.logTo("ElasticUpdateServiceLog", log);
            logger.writeToFile("\n\n\n");
            logger.writeToFile("");
            elasticDao.getDatabaseHelper().Close();
        }
        public string getSessionId()
        {
            return sessionId;
        }
    }
}
