using PCL.ViewModel;
using PCL.Interfaces.IoC;
using PCL.ViewModel.IoC;

namespace JsonConfigToMib
{
    class Bootstrap
    {
        public void Initialize()
        {
            this.Configure(ServiceLocator.Default);
        }
        protected void Configure(IContainer container)
        {
            container.RegisterInstance<IApplicationSettings>(new ApplicationSettings());
        }
    }
}
