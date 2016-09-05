using System;
using System.Collections.Generic;

namespace PCL.DataModel
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

		public IEnumerable<ModbusDevice> Devices
		{
			get;
			set;
		}
	}
}

