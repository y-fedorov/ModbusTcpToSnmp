using Lextm.SharpSnmpLib;
using System.Collections.Generic;
using System.Net;
using ModbusTcpToSnmp.PCL.ViewModel.IoC;
using ModbusTcpToSnmp.PCL.ViewModel.Logging;

namespace ModbusTcpToSnmp.PCL.ViewModel
{
    public class ModbusTcpToSnmpViewModel
    {
        private SnmpServer snmpServer;
        private bool initialized = false;
        IApplicationSettings appSettings;

        public ModbusTcpToSnmpViewModel(IApplicationSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        private void Initialize()
        {
            if (this.initialized)
            {
                return;
            }

            Logger.Info("App version: {0}", appSettings.AppVersion);
            Logger.Info("Config version: {0}", appSettings.ConfigVersion);


            var snmpServerBindIP = IPAddress.Parse(appSettings.SnmpServer.BindIp);
            this.snmpServer = ServiceLocator.Default.Resolve<SnmpServer>(
                new ParameterOverride("args", new SnmpServerArgs(snmpServerBindIP, appSettings.SnmpServer.Port)));


            var systemObjectId = new ObjectIdentifier(appSettings.SnmpServer.SysObjectId);
            snmpServer.AddToStore(new SystemObjectId(systemObjectId));
            Logger.Info("System OID: {0}", systemObjectId);

            snmpServer.AddToStore(new MySysDescr());

            var modbusConnections = new List<ModbusConnection>();

            {
                var rootOid = string.Format("1.3.6.1.4.1.{0}", appSettings.SnmpServer.EnterpriceId);

                foreach (var device in appSettings.Devices)
                {
                    var deviceOid = string.Format("{0}.{1}", rootOid, device.DeviceId);

                    Logger.Info("Add device: {0}", device);

                    var modbusHostIP = IPAddress.Parse(device.Host);
                    var modbusConnection = ServiceLocator.Default.Resolve<ModbusConnection>(
                        new ParameterOverride("args", new ModbusConnectionArgs(modbusHostIP, device.Port, device.SlaveId)));

                    modbusConnections.Add(modbusConnection);

                    foreach (var channel in device.Channels)
                    {
                        var deviceParamOID = new ObjectIdentifier(string.Format("{0}.{1}.0", deviceOid, channel.SnmpId));

                        snmpServer.AddToStore(
                                            new ModbusRequestDataObject(
                                                deviceParamOID,
                                                channel.Position,
                                                channel.Size,
                                                modbusConnection,
                                                channel.BitPos
                                            )
                                        );
                    }
                }
            }
        }

        public void Start()
        {
            if (this.initialized)
            {
                return;
            }
            this.Initialize();

            this.snmpServer.StartServer();
            this.initialized = true;
            Logger.Info("Snmp server started");
        }
        public void Stop()
        {
            if (this.initialized)
            {
                this.snmpServer.StopServer();
                Logger.Info("Snmp server stopped");
                this.initialized = false;
            }
        }
    }


}
