using Newtonsoft.Json.Linq;

namespace UpdateElastic.Utilities
{
    public static class JsonHelper
    {
        public static string getStringValue(JObject obj,string key,string defaultValue)
        {
            return (obj[key] ?? defaultValue).ToString();
        }
        public static int getIntValue(JObject obj, string key, int defaultValue)
        {
            return (int)(obj[key] ?? defaultValue);
        }
        public static double getDoubleValue(JObject obj, string key, double defaultValue)
        {
            return (double)(obj[key] ?? defaultValue);
        }
        public static float getFloatValue(JObject obj, string key, float defaultValue)
        {
            return (float)(obj[key] ?? defaultValue);
        }
        public static long getLongValue(JObject obj, string key, long defaultValue)
        {
            return (long)(obj[key] ?? defaultValue);
        }
        public static bool getBoolValue(JObject obj, string key, bool defaultValue)
        {
            return (bool)(obj[key] ?? defaultValue);
        }
    }
}
