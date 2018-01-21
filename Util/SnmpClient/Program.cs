using JsonToMib;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Security;
using PCL.ViewModel;
using PCL.ViewModel.IoC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SnmpClient
{
    class ConnectionOptions
    {
        public string CommunityName { get; set; }
        public IPAddress SnmpServerIp { get; set; }

        public int SnmpServerPort { get; set; }

        public int SnmpServerTimeout { get; set; }
        public ConnectionOptions(string snmpServerIp, int snmpServerPort)
        {
            this.SnmpServerIp = parseIPAddress(snmpServerIp);
            this.SnmpServerPort = snmpServerPort;
            this.CommunityName = "public";
            this.SnmpServerTimeout = 3000;
        }

        private IPAddress parseIPAddress(string snmpServerIp)
        {
            IPAddress ip;
            bool parsed = IPAddress.TryParse(snmpServerIp, out ip);
            if (!parsed)
            {
                var addresses = Dns.GetHostAddressesAsync(snmpServerIp);
                addresses.Wait();
                foreach (IPAddress address in addresses.Result.Where(address => address.AddressFamily == AddressFamily.InterNetwork))
                {
                    ip = address;
                    break;
                }

                if (ip == null)
                {
                    throw new Exception("invalid host or wrong IP address found: " + snmpServerIp);
                }
            }
            return ip;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            var bootStrap = new Bootstrap();
            Task.Run(() => bootStrap.InitializeAsync()).Wait();

            var appSettings = ServiceLocator.Default.Resolve<IApplicationSettings>();

            int enpId = appSettings.SnmpServer.EnterpriceId;
            int deviceNumber = 0;

            var co = new ConnectionOptions("127.0.0.1", 161);

            foreach (var device in appSettings.Devices)
            {
                deviceNumber++;
                foreach (var channel in device.Channels)
                {
                    string sum = channel.Summary;
                   // string id = ".1.3.6.1.2.1.1.1.0";
                    string id = ".1.3.6.1.2.1.2.1.0";
                    //string id = string.Format("1.3.6.1.4.1.{0}.{1}.{2}.0", enpId, deviceNumber, channel.SnmpId);
                    var value = Main2(co, new ObjectIdentifier(id));
                    Console.WriteLine("{0} {1} : {2}", sum, id, value);
                }
            }


            Console.WriteLine("");
        }


        public static string Main2(ConnectionOptions co, ObjectIdentifier oid)
        {
            IPEndPoint endpoint = new IPEndPoint(co.SnmpServerIp, co.SnmpServerPort);
            try
            {
                List<Variable> vList = new List<Variable> { new Variable(oid) };
                VersionCode version = VersionCode.V1;
                var communityName = new OctetString(co.CommunityName);

                foreach (Variable variable in Messenger.Get(version, endpoint, communityName, vList, co.SnmpServerTimeout))
                {
                    Console.WriteLine(variable);
                    if (variable.Data.TypeCode == SnmpType.OctetString)
                    {
                        return variable.Data.ToString();
                    } else if (variable.Data.TypeCode == SnmpType.Integer32)
                    {
                        var int32Value = variable.Data as Integer32;
                        var value = int32Value.ToInt32();

                        return value.ToString();
                    } else
                    {
                        throw new Exception("unsuported variable type");
                    }
                }
            }
            catch (SnmpException ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }

            throw new Exception("ex");
        }
    }
}
