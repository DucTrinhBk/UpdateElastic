
using System;
using System.IO;

namespace UpdateElastic.Utilities
{
    public class DataLogger
    {
        private static DataLogger instance;
        private DataLogger()
        {

        }
        public static DataLogger getInstance()
        {
            if(instance == null)
            {
                instance = new DataLogger();
            }
            return instance;
        }
        private string getFolderPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        }
        private string getFilePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        }
        public void writeToFile(string Message)
        {
            string path = getFolderPath();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = getFilePath();
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
