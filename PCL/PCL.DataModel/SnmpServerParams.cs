using System;

namespace ModbusTcpToSnmp.PCL.DataModel
{
    public interface ISnmpServerParams
    {
        int Port { get; }
        string BindIp { get; }
        int EnterpriceId { get; }
        string SysObjectId { get; }
        string Community { get; }
    }

    [Serializable]
    public class SnmpServerParams : ISnmpServerParams
    {
		public int Port { get; set; }
		public string BindIp { get; set; }
		public int EnterpriceId { get; set; }
        public string SysObjectId { get; set; }
        public string Community { get; set; }
	}
	
}
