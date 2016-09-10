using System;

namespace ModbusTcpToSnmp.PCL.Interfaces.Logging
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogException(Exception ex);
        void LogInfo(string message);
    }
}
