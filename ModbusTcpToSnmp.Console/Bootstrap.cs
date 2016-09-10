using ModbusTcpToSnmp.PCL.Interfaces.IoC;
using ModbusTcpToSnmp.PCL.Interfaces.Logging;
using ModbusTcpToSnmp.PCL.ViewModel;
using ModbusTcpToSnmp.PCL.ViewModel.IoC;
using ModbusTcpToSnmp.PCL.ViewModel.Logging;

namespace ModbusTcpToSnmp
{
    class Bootstrap
    {
        public void Initialize()
        {
            this.Configure(ServiceLocator.Default);
        }
        protected void Configure(IContainer container)
        {
            Log4NetLogger.Init();

            container.RegisterInstance(new Log4NetLogger("General"));
            container.RegisterInstance(new RollingLogger());
            container.RegisterInstance<ILogger>(Logger.Current);

            container.RegisterInstance<IApplicationSettings>(new ApplicationSettings());

            container.Register<SnmpServer, SnmpServer>();
            container.Register<ModbusConnection, ModbusConnection>();
        }
    }
}
