
using PCL.ViewModel;
using PCL.ViewModel.IoC;

namespace ModbusTcpToSnmp
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var bootStrap = new Bootstrap();
            bootStrap.Initialize();

            ServiceLocator.Default.Resolve<SnmpToModbusViewModel>().Start();
        }
    }
}
