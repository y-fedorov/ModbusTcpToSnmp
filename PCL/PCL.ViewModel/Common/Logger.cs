using System;
using PCL.Interfaces.Logging;
using PCL.ViewModel.IoC;

namespace PCL.ViewModel.Logging
{
    public static class Logger
    {
        static Logger()
        {
        }

        private static readonly ILogger CurrentLogger = ServiceLocator.Default.Resolve<Log4NetLogger>();

        public static ILogger Current
        {
            get
            {
                return CurrentLogger;
            }
        }
        public static void Info(string message, params object[] args)
        {
            Current.LogInfo(string.Format(message, args));
        }
        public static void Debug(string message, params object[] args)
        {
            Current.LogDebug(string.Format(message, args));
        }

        public static void LogException(Exception e)
        {
            Current.LogException(e);
        }
    }
}
