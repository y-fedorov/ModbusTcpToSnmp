using System;
using Modbus.Device;
using System.Net.Sockets;
using Modbus.Utility;
using System.Threading;
using System.Net;
using ModbusTcpToSnmp.PCL.ViewModel.Logging;
using ModbusTcpToSnmp.PCL.ViewModel.Exceptions;

namespace ModbusTcpToSnmp.PCL.ViewModel
{
    public class ModbusConnection
    {
        public ModbusConnection(ModbusConnectionArgs args)
        {
            this.connectionArgs = args;
        }

        public ModbusConnectionArgs connectionArgs
        {
            get;
            private set;
        }

        private readonly int SleepTimeout = 100;
        private readonly int DefaultAttempts = 12;

        private TcpClient Connect(IPAddress ip, int port)
        {
            var attempts = 1;
            while (attempts < this.DefaultAttempts)
            {
                try
                {
                    var tcpClient = new TcpClient(AddressFamily.InterNetwork);
                    tcpClient.Connect(ip, port);

                    return tcpClient;

                }
                catch (Exception e)
                {
                    Logger.Debug(string.Format("Connection error. Attempt number: {0}  -> {1}", attempts, e.Message));
                }

                Thread.Sleep(this.SleepTimeout);
                attempts++;
            }

            throw new ConnectionProblemsException(string.Format("Connection error. fail {0} attempts.", attempts));
        }

        public float ReadFloat(int dataIndex)
        {
            using (var tcpClient = this.Connect(this.connectionArgs.Ip, this.connectionArgs.Port))
            {
                using (var master = ModbusIpMaster.CreateIp(tcpClient))
                {
                    byte slaveId = Convert.ToByte(this.connectionArgs.SlaveId);
                    ushort startAddress = Convert.ToUInt16(dataIndex);
                    ushort numberOfPoints = 2;

                    Console.WriteLine("Request: {0}:{1} SlaveId: {2:D2} start addr: {3:D4} length: {4:D2}", this.connectionArgs.Ip, this.connectionArgs.Port, slaveId, startAddress, numberOfPoints);

                    ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);

                    uint value = ModbusUtility.GetUInt32(registers[0], registers[1]);
                    var floatValue = ModbusUtility.GetSingle(registers[0], registers[1]);

                    Console.WriteLine("-> 32BitFloat val: {0}\nhex: 0x{0:X8}\nData: {1:00000}{2:00000} HEX: 0x{1:X4} 0x{2:X4} HEX-32: 0x{1:X4}{2:X4}\nFloatValue: {3}", value, registers[0], registers[1], floatValue);

                    return floatValue;
                }
            }
        }

        public int ReadInt(int dataIndex)
        {
            using (var tcpClient = this.Connect(this.connectionArgs.Ip, this.connectionArgs.Port))
            {
                using (var master = ModbusIpMaster.CreateIp(tcpClient))
                {
                    master.Transport.Retries = 0;

                    byte slaveId = Convert.ToByte(this.connectionArgs.SlaveId);
                    ushort startAddress = Convert.ToUInt16(dataIndex);
                    ushort numberOfPoints = 1;

                    Console.WriteLine("Request: {0}:{1} SlaveId: {2:D2} start addr: {3:D4} length: {4:D2}", this.connectionArgs.Ip, this.connectionArgs.Port, slaveId, startAddress, numberOfPoints);

                    ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);

                    var value = registers[0];
                    Console.WriteLine("-> 16BitInt val: {0}\nhex: 0x{0:X8}\nIntValue: {1}", value, (int)value);
                    return value;
                }
            }
        }
    }

}

