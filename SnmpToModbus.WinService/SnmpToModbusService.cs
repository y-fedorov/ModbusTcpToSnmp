using PCL.ViewModel;
using PCL.ViewModel.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SnmpToModbus.WinService
{
    public partial class SnmpToModbusService : ServiceBase
    {
        private EventLog eventLog1;

        private SnmpToModbusViewModel viewModel;

        public readonly string EventLogIdentifier = "SnmpToModbusService";

        public SnmpToModbusService()
        {
            InitializeComponent();
            
            if (!EventLog.SourceExists(EventLogIdentifier))
            {
                EventLog.CreateEventSource(EventLogIdentifier, "Application");
            }
            eventLog1 = new System.Diagnostics.EventLog();
            eventLog1.Source = EventLogIdentifier;
        //    eventLog1.Log = "MyNewLog";
            eventLog1.WriteEntry("Init SnmpToModbusService.");

            var bootStrap = new Bootstrap();
            Task.Run(() => bootStrap.InitializeAsync()).Wait();

            this.viewModel = ServiceLocator.Default.Resolve<SnmpToModbusViewModel>();
        }

        protected override void OnStart(string[] args)
        {
            this.viewModel.Start();
            eventLog1.WriteEntry("In OnStart");
        }

        protected override void OnStop()
        {
            this.viewModel.Stop();
            eventLog1.WriteEntry("In OnStop");
        }
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }
    }
}
