using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using ModbusTcpToSnmp.PCL.DataModel;

namespace ModbusTcpToSnmp.PCL.ViewModel
{
    public interface IApplicationSettings
    {
        string ConfigVersion { get; }
        string AppVersion { get; }
        ISnmpServerParams SnmpServer { get; }
        IEnumerable<IModbusDevice> Devices { get; }
    }


    public class ApplicationSettings : IApplicationSettings
    {
		private static readonly string fileName = "Resources/modbus-snmp-orig.json";
        private readonly SnmpToModbusConfig config;

        public ApplicationSettings(string configFile)
        {
            try
            {
                using (var configStreamReader = new StreamReader(configFile))
                {
                    var jsonTextReader = new JsonTextReader(configStreamReader);
                    var jsonSerializer = new JsonSerializer();
                    this.config = jsonSerializer.Deserialize<SnmpToModbusConfig>(jsonTextReader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

		public ApplicationSettings() : this(fileName)
        {

        }

        public string AppVersion
        {
            get
            {
                return "0.0.0.1";
            }
        }
        public string ConfigVersion
        {
            get
            {
                return config.Version;
            }
        }
        public ISnmpServerParams SnmpServer {
            get
            {
                return config.SnmpServer;
            }
        }
        public IEnumerable<IModbusDevice> Devices
        {
            get
            {
                return config.Devices;
            }
        }
    }
}

