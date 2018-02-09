using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnmpClient
{
    class Options
    {
        public const string ALL_DEVICES = "ALL";
        public const string ALL_CHANNELS = "ALL";

        [Option('i', "ip", Required = true, HelpText = "SNMP server connection IP address")]
        public string IpAddress { get; set; }

        [Option('p', "port", Default = 161, HelpText = "SNMP server connection port (Default 161)")]
        public int Port { get; set; }

        [Option('c', "config", Required = true, HelpText = "Config file with error codes (ex. modbus-snmp-orig.json)")]
        public string ConfigFile { get; set; }

        [Option('f', "factor", Default = 0.01f, HelpText ="Default factor is 0.01 (ex. value * factor)")]
        public float Factor { get; set; }

        [Option("devices", HelpText ="Filter devices by device name")]
        public IEnumerable<string> DevicesFilter { get; set; }

        [Option("channels", HelpText = "Filter device channels by device name")]
        public IEnumerable<string> ChannelsFilter { get; set; }

        [Option('t', "timeout", Default = 1, HelpText = "Timeout (Default 1 sec)")]
        public int Timeout { get; set; }
    }
}
