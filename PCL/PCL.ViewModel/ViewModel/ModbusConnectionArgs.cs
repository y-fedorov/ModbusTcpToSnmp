using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTcpToSnmp.PCL.ViewModel
{
    public class ModbusConnectionArgs
    {
        public IPAddress Ip { get; private set; }
        public int Port { get; private set; }
        public int SlaveId { get; private set; }

        public ModbusConnectionArgs(IPAddress hostIp, int hostPort, int slaveId = 1)
        {
            this.Ip = hostIp;
            this.Port = hostPort;
            this.SlaveId = slaveId;
        }
    }
}
