using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Pipeline;
using ModbusTcpToSnmp.PCL.DataModel;
using ModbusTcpToSnmp.PCL.ViewModel.Logging;
using System;
using System.Collections;

namespace ModbusTcpToSnmp.PCL.ViewModel
{
    public sealed class ModbusRequestDataObject : ScalarObject
    {
        public ModbusRequestDataObject(ObjectIdentifier objId, int index, ModbusDataType dt, ModbusConnection conn, int bitId = 0)
            : base(objId)
        {
            this.conn = conn;
            this.mdt = dt;
            this.index = index;
            this.oid = objId;
            this.bitId = bitId;
        }

        private Object thisLock = new Object();
        private int bitId;

        private ModbusConnection conn { get; set; }
        private int index { get; set; }
        private ModbusDataType mdt { get; set; }
        private ObjectIdentifier oid { get; set; }
        public override ISnmpData Data
        {
            get
            {
                Logger.Info("Requested OID: {0}", this.oid);

                lock (this.thisLock)
                {
                    try
                    {
                        if (this.mdt == ModbusDataType.Float32)
                        {
                            float value = this.conn.ReadFloat(this.index);
                            return new Integer32(Convert.ToInt32((double)value * 100));
                        }
                        else if (this.mdt == ModbusDataType.Int16)
                        {
                            return new Integer32(this.conn.ReadInt(this.index));
                        }
                        else
                        {
                            int value = this.conn.ReadInt(this.index);

                            var ba = new BitArray(new int[] { value });
                            var val = ba.Get(this.bitId);
                            return new Integer32(Convert.ToInt32(val));
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.LogException(e);
                    }
                    return new Integer32(-1);
                }
            }
            set
            {
                throw new AccessFailureException();
            }
        }
    }
}
