using CommandLine;

using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using PCL.DataModel;
using PCL.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
        static void HandleParseError(IEnumerable<Error> errs)
        {
            throw new Exception("Parse argumnet exception");
        }

        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            var parsedOptions = Parser.Default.ParseArguments<Options>(args).WithNotParsed<Options>((errs) => HandleParseError(errs));
            var options = (parsedOptions as Parsed<Options>).Value;

            var appSettings = new ApplicationSettings(options.ConfigFile);

            int enpId = appSettings.SnmpServer.EnterpriceId;

            var co = new ConnectionOptions(options.IpAddress, options.Port);

            var availableDevices = appSettings.Devices;

            Func< string, List <IModbusDevice>, string> enumerateDevices = (title, devices) => {
                string output =  title + "\n";
                devices.ForEach(d => output += string.Format("\t {0} - {1}\n", d.Name, d.DisplayName));
                return output + "\n";
            };

            Console.WriteLine(enumerateDevices("Available devices:", availableDevices.ToList()));

            if (options.DevicesFilter.Any())
            {
                availableDevices = availableDevices.Where(d => options.DevicesFilter.Contains(d.Name));
                Console.WriteLine(enumerateDevices("Selected devices:", availableDevices.ToList()));
            }
            if (!availableDevices.Any())
            {
                Console.WriteLine("no devices");
                return;
            }

            var timeout = 1000 * options.Timeout;
            if (timeout < 1000)
            {
                timeout = 1000;
            }

            while (true)
            {

                foreach (var device in availableDevices)
                {
                    var availableChannels = device.Channels;

                    if (options.ChannelsFilter.Any())
                    {
                        availableChannels = availableChannels.Where(c => options.ChannelsFilter.Contains(c.Name));
                    }

                    foreach (var channel in availableChannels)
                    {
                        string sum = channel.Summary;
                        string id = string.Format("1.3.6.1.4.1.{0}.{1}.{2}.0", enpId, device.DeviceId, channel.SnmpId);
                        var value = Main2(co, new ObjectIdentifier(id));

                        var floatValue = Int32.Parse(value) * options.Factor;
                        Console.WriteLine("{0} : {1}", sum, floatValue);

                        
                        Thread.Sleep(timeout);
                    }
                }
                
            }
        }


        public static string Main2(ConnectionOptions co, ObjectIdentifier oid)
        {
            IPEndPoint endpoint = new IPEndPoint(co.SnmpServerIp, co.SnmpServerPort);
            try
            {
                List<Variable> vList = new List<Variable> { new Variable(oid) };
                VersionCode version = VersionCode.V2;
                var communityName = new OctetString(co.CommunityName);

                foreach (Variable variable in Messenger.Get(version, endpoint, communityName, vList, co.SnmpServerTimeout))
                {
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
