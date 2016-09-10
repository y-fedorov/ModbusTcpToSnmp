# ModbusTcpToSnmp
Mediator between [Modbus TCP](https://en.wikipedia.org/wiki/Modbus) and [SNMP](https://en.wikipedia.org/wiki/Simple_Network_Management_Protocol) agent that allows you to configure the system to generate [MIB](https://en.wikipedia.org/wiki/Management_information_base) files and query the data from the Modbus TCP protocol and give via SNMP server.

### Dependencies
  - [Microsoft Visual Studio 2015 Installer Projects](https://visualstudiogallery.msdn.microsoft.com/f1cc3f3e-c300-40a7-8797-c509fb8933b9)
- Nuget packages
    -  [NModbus](https://github.com/NModbus4/NModbus4) 4.2.1.0 - [The MIT License](https://github.com/NModbus4/NModbus4/blob/portable-3.0/LICENSE.txt)
    -  [Lextm.SharpSnmpLib](https://sharpsnmplib.codeplex.com/) 9.0.1 - [The MIT License](https://sharpsnmplib.codeplex.com/license)
    -  [Newtonsoft.Json](http://www.newtonsoft.com/json) 8.0.3 - [The MIT License (MIT)](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md)
    -  [Unity](https://unity.codeplex.com/) 4.0.1 - [Apache License 2.0 (Apache)](https://unity.codeplex.com/license)
    -  [Log4net](https://logging.apache.org/log4net/) 2.0.5 - [Apache License 2.0 (Apache)](https://logging.apache.org/log4net/license.html)
    -  [CommonServiceLocator](https://commonservicelocator.codeplex.com/) 1.3 - [Apache License 2.0 (Apache)](https://logging.apache.org/log4net/license.html)

### Contents
##### ModbusTcpToSnmp.Console
Console application that receives SNMP Agent requests and converts them into ModbusTCP requests. Also supports configuration and addressing options.

##### ModbusTcpToSnmp.WinService
Windows Service receives SNMP Agent requests and converts them into ModbusTCP requests. Also supports configuration and addressing options.
##### Utils :: JsonConfigToMib.Console
Provides the ability to generate the MIB file from the ModbusToSnmp configuration file (`*.conf`).

##### Utils :: ModbusTcpDataReader.Console
The console utility is carrying out a request Modbus TCP data according to set parameters.

License
---
MIT