using System;
using System.Collections.Generic;

namespace PCL.DataModel
{
    public class IModbusDeviceChannel
    {
        ModbusValueType Type { get; }
        string Name { get; }
        string Summary { get; }
        ModbusDataType Size { get; }
        int Position { get; }
        int BitPos { get; }
        int SnmpId { get; }
    }

    [Serializable]
    public class ModbusDeviceChannel : IModbusDeviceChannel
    {
		public ModbusValueType Type { get; set; }
		public string Name { get; set; }
		public string Summary { get; set; }
		public ModbusDataType Size { get; set;}
		public int Position { get; set; }
        public int BitPos { get; set; }
        public int SnmpId {get; set; }
	}
	
}
