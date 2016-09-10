using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTcpToSnmp
{
    public class SnmpServerArgs
    {
        public IPAddress IpAddress { get; set; }
        public int Port { get; set; }

        public SnmpServerArgs(IPAddress ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
        }
    }
}
