using PCL.ViewModel;
using PCL.Interfaces.IoC;
using System.Threading.Tasks;
using PCL.Interfaces.Logging;
using PCL.ViewModel.Logging;
using PCL.ViewModel.IoC;

namespace JsonToMib
{
    class Bootstrap
    {
        public async Task InitializeAsync()
        {
            await this.ConfigureAsync(ServiceLocator.Default);
        }
        protected async Task ConfigureAsync(IContainer container)
        {
            container.RegisterInstance<IApplicationSettings>(new ApplicationSettings());
            return;
        }
    }
}
