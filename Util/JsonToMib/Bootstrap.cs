using ModbusTcpToSnmp.PCL.Interfaces.IoC;
using ModbusTcpToSnmp.PCL.ViewModel;
using ModbusTcpToSnmp.PCL.ViewModel.IoC;

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
