using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using UpdateElastic.Utilities;
namespace UpdateElastic
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new UpdateDataService()
            };
            ServiceBase.Run(ServicesToRun);
            
        }
     
    }
}
