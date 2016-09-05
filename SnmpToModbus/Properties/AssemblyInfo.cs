#region Using directives

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

#endregion

[assembly: AssemblyTitle("SnmpToModbus.Console")]


[assembly: AssemblyProduct("SnmpToModbusTCP")]
[assembly: AssemblyDescription("SNMP agent application, that transform SNMP data requests to Modbus TCP ones")]

[assembly: AssemblyCompany("INDY-WORLD.NET")]
[assembly: AssemblyCopyright("Copyright © 2016 Yaroslav Fedorov")]


[assembly: AssemblyTrademark("")]
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
[assembly: NeutralResourcesLanguage("en-US")]
/*
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
*/
