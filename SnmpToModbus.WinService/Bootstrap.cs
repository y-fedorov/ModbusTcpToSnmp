using PCL.ViewModel;using PCL.Interfaces.IoC;using System.Threading.Tasks;using PCL.Interfaces.Logging;
using PCL.ViewModel.Logging;
using PCL.ViewModel.IoC;

namespace SnmpToModbus{    class Bootstrap    {        public async Task InitializeAsync()        {            await this.ConfigureAsync(ServiceLocator.Default);        }        protected async Task ConfigureAsync(IContainer container)        {            Log4NetLogger.Init();

            container.RegisterInstance(new Log4NetLogger("General"));
            container.RegisterInstance(new Logging.RollingLogger());
            container.RegisterInstance<ILogger>(Logger.Current);


            container.RegisterInstance<IApplicationSettings>(new ApplicationSettings());

            container.Register<SnmpServer, SnmpServer>();
            container.Register<ModbusConnection, ModbusConnection>();
            return;        }    }}