using log4net.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Interfaces.Logging
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogException(Exception ex);
        void LogInfo(string message);
    }
}
