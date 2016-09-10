using ModbusTcpToSnmp.PCL.ViewModel;
using ModbusTcpToSnmp.PCL.ViewModel.IoC;

namespace ModbusTcpToSnmp
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var bootStrap = new Bootstrap();
            bootStrap.Initialize();

            ServiceLocator.Default.Resolve<ModbusTcpToSnmpViewModel>().Start();
        }
    }
}
