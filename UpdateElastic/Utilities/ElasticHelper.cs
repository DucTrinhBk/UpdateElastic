using Elasticsearch.Net;
using System;

namespace UpdateElastic.Utilities
{
    public class ElasticHelper
    {
        private static ElasticHelper instance;
        private ElasticLowLevelClient client;
        private ElasticHelper()
        {

        }
        public static ElasticHelper getInstance(string elasticUrl)
        {
            if(instance == null)
            {
                instance = new ElasticHelper();
            }
            var settings = new ConnectionConfiguration(new Uri(elasticUrl))
                .RequestTimeout(TimeSpan.FromMinutes(DataConfigs.ELASTIC_TIMEOUT_MINUTES));
            instance.client =  new ElasticLowLevelClient(settings);
            return instance;
        }
        public static ElasticHelper getDefaultInstance()
        {
            return getInstance(DataConfigs.getValue(DataConfigs.ELASTIC_LINK));
        }
        public StringResponse delete(string index,string type,string id)
        {
            return instance.client.Delete<StringResponse>(index,type, id);
        }
        public StringResponse updateOrCreate(string index, string type, string id, object value)
        {
            return instance.client.Index<StringResponse>(index, type, id, PostData.Serializable(value));
        }
        public StringResponse get(string index, string type,string id)
        {

            return instance.client.Get<StringResponse>(index,type, id);
        }
        public StringResponse delete(string id)
        {
            return instance.delete( DataConfigs.getValue(DataConfigs.INDEX), DataConfigs.getValue(DataConfigs.TYPE), id);
        }
        public StringResponse updateOrCreate(string id,object value)
        {
            return instance.updateOrCreate( DataConfigs.getValue(DataConfigs.INDEX), DataConfigs.getValue(DataConfigs.TYPE), id,value);
        }
        public StringResponse get(string id)
        {
           
            return instance.get(DataConfigs.getValue(DataConfigs.INDEX), DataConfigs.getValue(DataConfigs.TYPE), id);
        }
    }
}
