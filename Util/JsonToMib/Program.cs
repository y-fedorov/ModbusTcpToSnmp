using System;
using System.IO;
using PCL.ViewModel;
using PCL.ViewModel.IoC;

namespace JsonConfigToMib
{
    class MainClass
	{
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            var bootStrap = new Bootstrap();
            bootStrap.Initialize();

            var appSettings = ServiceLocator.Default.Resolve<IApplicationSettings>();

            var productName = "<product-name>";

            var header = string.Format("{0}-mib DEFINITIONS ::= BEGIN\n\n" +
                        "IMPORTS\n" +
                        "        OBJECT-TYPE, Integer32, NOTIFICATION-TYPE, enterprises\n" +
                        "                     FROM SNMPv2-SMI;\n\n" +
                        "--\n" +
                        "-- Node definitions\n" +
                        "--",
                        "<product>"); //TODO


            var objIdentifierFormat = "{0} OBJECT IDENTIFIER ::= {{ {1} {2} }}";

            var fileName = string.Format("{0}-v{1}.mib", productName, appSettings.ConfigVersion);

            using (StreamWriter file = new StreamWriter(fileName))
            {
                file.WriteLine(header);
                file.WriteLine(string.Format(objIdentifierFormat, "techmeh", "enterprises", appSettings.SnmpServer.EnterpriceId));

                foreach (var device in appSettings.Devices)
                {
                    file.WriteLine(string.Format(objIdentifierFormat, device.Name, "techmeh", device.DeviceId));

                    foreach (var channel in device.Channels)
                    {
                        var rowString = string.Format("-- 1.3.6.1.4.1.{4}.{0}.{2}.0\n" +
                            "{5}{1} OBJECT-TYPE\n" +
                            "    SYNTAX Integer32\n" +
                            "    MAX-ACCESS read-only\n" +
                            "    STATUS current\n" +
                            "    DESCRIPTION \"A sample desription of something.\"\n" +
                            "    ::= {{ {3} {2} }}\n",
                            device.DeviceId,
                            channel.Name,
                            channel.SnmpId,
                            device.Name,
                            appSettings.SnmpServer.EnterpriceId,
                            device.Prefix
                        );
                        file.WriteLine(rowString);
                    }
                    file.WriteLine("\n\n");
                }
                file.WriteLine("END");
            }
        }
	}
}
