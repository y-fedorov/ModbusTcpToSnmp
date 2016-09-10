using System;
using System.Collections.Generic;

namespace ModbusTcpToSnmp.PCL.DataModel
{
    public interface IModbusDevice
    {
        string Name { get; }
        string Model { get; }
        string Host { get; }
        int Port { get; }
        int DeviceId { get; }
        int SlaveId { get; }
        string Prefix { get; }
        IEnumerable<ModbusDeviceChannel> Channels { get; }
    }

    [Serializable]
    public class ModbusDevice : IModbusDevice
    {
		public string Name { get; set; }
		public string Model { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
        public int DeviceId { get; set; }
		public int SlaveId {get; set; }
        public string Prefix { get; set; }
        public IEnumerable<ModbusDeviceChannel> Channels { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1} \"{2}\" ({3}) SlaveId: {4}", this.Host, this.Port, this.Name, this.Model, this.SlaveId);
        }
    }
	
}
