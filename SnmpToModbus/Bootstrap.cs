using PCL.ViewModel;
using PCL.Interfaces.IoC;
using System.Threading.Tasks;
using PCL.Interfaces.Logging;
using PCL.ViewModel.Logging;
using PCL.ViewModel.IoC;

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
            container.RegisterInstance(new Logging.RollingLogger());
            container.RegisterInstance<ILogger>(Logger.Current);

            container.RegisterInstance<IApplicationSettings>(new ApplicationSettings());

            container.Register<SnmpServer, SnmpServer>();
            container.Register<ModbusConnection, ModbusConnection>();
        }
    }
}
