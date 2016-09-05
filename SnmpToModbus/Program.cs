
using PCL.ViewModel;
using PCL.ViewModel.IoC;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SnmpToModbus
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var bootStrap = new Bootstrap();
            Task.Run(() => bootStrap.InitializeAsync()).Wait();

            var vm = ServiceLocator.Default.Resolve<SnmpToModbusViewModel>();

            vm.Start();


            Console.ReadKey();

            vm.Stop();
        }
    }
}
