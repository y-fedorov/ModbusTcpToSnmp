using System;
using System.IO;
using System.ServiceProcess;

namespace ModbusTcpToSnmp.WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ModbusTcpToSnmpService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
