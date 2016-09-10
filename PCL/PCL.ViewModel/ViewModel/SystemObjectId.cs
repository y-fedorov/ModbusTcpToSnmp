using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Pipeline;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTcpToSnmp
{
    public sealed class SystemObjectId : ScalarObject
    {
        private readonly ObjectIdentifier sysObjectId;

        public SystemObjectId(ObjectIdentifier sysObjectId)
            : base(new ObjectIdentifier("1.3.6.1.2.1.1.2.0"))
        {
            this.sysObjectId = sysObjectId;
        }

        public override ISnmpData Data
        {
            get
            {
                return this.sysObjectId;
            }
            set
            {
                throw new AccessFailureException();
            }
        }
    }

    public sealed class MySysDescr : ScalarObject
    {
        private readonly OctetString _description =
            new OctetString(string.Format(CultureInfo.InvariantCulture, "#SNMP to Modbus Agent on {0}", Environment.OSVersion));

        public MySysDescr()
            : base(new ObjectIdentifier("1.3.6.1.2.1.1.1.0"))
        {
        }

        public override ISnmpData Data
        {
            get
            {
                return this._description;
            }
            set
            {
                throw new AccessFailureException();
            }
        }
    }
}
