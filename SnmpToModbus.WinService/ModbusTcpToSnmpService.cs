using PCL.ViewModel;
using PCL.ViewModel.IoC;
using System.Diagnostics;
using System.ServiceProcess;

namespace ModbusTcpToSnmp.WinService
{
    public partial class ModbusTcpToSnmpService : ServiceBase
    {
        private EventLog eventLog;
        private SnmpToModbusViewModel viewModel;

        public readonly string EventLogIdentifier = "ModbusTcpToSnmp.WinService";

        public ModbusTcpToSnmpService()
        {
            InitializeComponent();
            
            if (!EventLog.SourceExists(EventLogIdentifier))
            {
                EventLog.CreateEventSource(EventLogIdentifier, "Application");
            }
            eventLog = new System.Diagnostics.EventLog();
            eventLog.Source = EventLogIdentifier;
            eventLog.WriteEntry(string.Format("Init {0}.", EventLogIdentifier));

            var bootStrap = new Bootstrap();
            bootStrap.Initialize();

            this.viewModel = ServiceLocator.Default.Resolve<SnmpToModbusViewModel>();
        }

        protected override void OnStart(string[] args)
        {
            this.viewModel.Start();
            eventLog.WriteEntry("In OnStart");
        }

        protected override void OnStop()
        {
            this.viewModel.Stop();
            eventLog.WriteEntry("In OnStop");
        }
    }
}
