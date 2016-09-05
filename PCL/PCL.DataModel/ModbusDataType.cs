using System;
using System.Collections.Generic;

namespace PCL.DataModel
{
    [Serializable]
    public enum ModbusDataType
	{
        Byte,       // 1 byte
        Int16,      // 2 bytes
		Float32     // 4 bytes
	}
}
