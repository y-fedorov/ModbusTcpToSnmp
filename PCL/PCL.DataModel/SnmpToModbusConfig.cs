using System.Collections.Generic;

namespace ModbusTcpToSnmp.PCL.DataModel
{
    public class SnmpToModbusConfig
	{
		public string Version
		{
			get;
			set;
		}

		public SnmpServerParams	SnmpServer
		{
			get;
			set;
		}

        public string ProjectName
        {
            get;
            set;
        }
		public IEnumerable<ModbusDevice> Devices
		{
			get;
			set;
		}
	}
}

