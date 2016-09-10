using System;
using System.Net.Sockets;
using Modbus.Device;
using Modbus.Utility;
using log4net.Config;

namespace ModbusTcpDataReader
{
    public class MainClass
    {
        public class ModbusConnectionArgs
        {
            public string IpAddress;
            public int Port;
            public int SlaveId;
            public int StartAddress;
            public int NumberOfPoints;

            public ModbusConnectionArgs(string ip, int port, int slaveId, int from, int to)
            {
                this.IpAddress = ip;
                this.Port = port;
                this.SlaveId = slaveId;
                this.StartAddress = from;
                this.NumberOfPoints = to;
            }
        }
        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var cmdString = "<tool.exe> --ip 127.0.0.1 --port 502 --slaveId 1 --from 3 --to 15";

            if (args.Length == 0)
            {
                Console.WriteLine(cmdString);
                return;
            }

            var ip = "127.0.0.1";
            var port = 502;
            var slaveId = 1;
            var from = 1;
            var to = 1;

            try
            {

                int ipIndex = Array.FindIndex(args, key => key == "--ip");
                if (ipIndex != -1)
                {
                    ip = args[ipIndex + 1];
                }

                int portIndex = Array.FindIndex(args, key => key == "--port");
                if (portIndex != -1)
                {
                    var value = args[portIndex + 1];
                    port = Convert.ToInt32(value);
                }

                int slaveIdIndex = Array.FindIndex(args, key => key == "--slaveId");
                if (slaveIdIndex != -1)
                {
                    var value = args[slaveIdIndex + 1];
                    slaveId = Convert.ToInt32(value);
                }

                int fromIndex = Array.FindIndex(args, key => key == "--from");
                if (fromIndex != -1)
                {
                    var value = args[fromIndex + 1];
                    from = Convert.ToInt32(value);
                }

                int toIndex = Array.FindIndex(args, key => key == "--to");
                if (toIndex != -1)
                {
                    var value = args[toIndex + 1];
                    to = Convert.ToInt32(value);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(cmdString);
                return;
            }

            Console.WriteLine(">>>> IP:{0} Port:{1} SlaveID: {2} StartPoint: {3} NumberOfPoints: {4}", ip, port, slaveId, from, to);
            Console.WriteLine("---------\n");
            try
            {
                ModbusTcpMasterReadInputs(new ModbusConnectionArgs(ip, port, slaveId, from, to));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ModbusTcpMasterReadInputs(ModbusConnectionArgs args)
        {
            using (var client = new TcpClient(args.IpAddress, args.Port))
            {
                var master = ModbusIpMaster.CreateIp(client);

                byte slaveId = Convert.ToByte(args.SlaveId);
                ushort startAddress = Convert.ToUInt16(args.StartAddress);
                ushort numberOfPoints = Convert.ToUInt16(args.NumberOfPoints);

                // read large value in two 16 bit chunks and perform conversion
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);
                for (int i = 0; i < registers.Length; ++i)
                {
                    Console.WriteLine("Input {0:00} - value: {1:00000} - HEX: 0x{1:X}", startAddress + i, registers[i]);
                }
                Console.WriteLine("---------\n");
                for (int i = 0; i + 1 < registers.Length; i += 2)
                {
                    uint value = ModbusUtility.GetUInt32(registers[i], registers[i + 1]);
                    var floatValue = ModbusUtility.GetSingle(registers[i], registers[i + 1]);

                    Console.WriteLine("\nID {0} 32Bit val: {1}\nhex: 0x{1:X8}\nData: {2:00000}{3:00000} HEX: 0x{2:X4} 0x{3:X4} HEX-32: 0x{2:X4}{3:X4}\nFloatValue:{4}", i, value, registers[i], registers[i + 1], floatValue);
                }
                Console.WriteLine("Done!");
            }
        }

    }
}