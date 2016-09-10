#region Using directives

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

#endregion

[assembly: AssemblyTitle("ModbusTcpToSnmp.Console")]


[assembly: AssemblyProduct("ModbusTcpToSnmp")]
[assembly: AssemblyDescription("Part of the ModbusToSnmp distribution. Console applicetion that receives SNMP Agent requests and converts them into ModbusTCP requests. Also supports configuration and addressing options.")]

[assembly: AssemblyCompany("https://github.com/y-fedorov")]
[assembly: AssemblyCopyright("Licensed under the MIT License (MIT).")]


[assembly: AssemblyTrademark("Yaroslav Fedorov")]
[assembly: AssemblyCulture("")]

#if DEBUG
    [assembly: AssemblyConfiguration("Debug")]
#else
    [assembly: AssemblyConfiguration("Release")]
#endif

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
/*
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
*/
