using PCL.ViewModel;using PCL.Interfaces.IoC;using System.Threading.Tasks;using PCL.Interfaces.Logging;
using PCL.ViewModel.Logging;
using PCL.ViewModel.IoC;

namespace JsonToMib{    class Bootstrap    {        public async Task InitializeAsync()        {            await this.ConfigureAsync(ServiceLocator.Default);        }        protected async Task ConfigureAsync(IContainer container)        {           // Log4NetLogger.Init();

           // container.RegisterInstance(new Log4NetLogger("General"));
           // container.RegisterInstance(new Logging.RollingLogger());
           // container.RegisterInstance<ILogger>(Logger.Current);


            container.RegisterInstance<IApplicationSettings>(new ApplicationSettings());

            return;        }    }}