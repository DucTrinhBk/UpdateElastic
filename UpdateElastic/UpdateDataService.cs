using Elasticsearch.Net;
using System;
using System.Data.Common;
using System.ServiceProcess;
using UpdateElastic.Utilities;
using UpdateElastic.Daoes;
using UpdateElastic.Managers;

namespace UpdateElastic
{
    partial class UpdateDataService : ServiceBase
    {
        private ProcessManager manager;
        public UpdateDataService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            
            manager = ProcessManager.getInstance(DateTime.Now.ToFileTimeUtc() + "-" + DateTime.Now.GetHashCode());
            manager.startExecuteService(DataConfigs.INTERVAL);
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            manager.stopExecuteService();
        }

     
  

       
        
    }
}
