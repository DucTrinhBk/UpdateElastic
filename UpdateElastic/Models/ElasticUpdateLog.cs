using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateElastic.Models
{
    class ElasticUpdateLog
    {
        public string DataId { get; set; }
        public int ArticleId { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
        public string State { get; set; }
        
    }
}
