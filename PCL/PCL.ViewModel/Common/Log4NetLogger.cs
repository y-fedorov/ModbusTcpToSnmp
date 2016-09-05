using log4net.Config;
using PCL.Interfaces.Logging;
using System;
using System.IO;

namespace PCL.ViewModel.Logging
{
    public class Log4NetLogger : ILogger
    {
        private readonly log4net.ILog log;
        private string category;

        public static void Init()
        {
            XmlConfigurator.Configure();
        }
        public static void Init(string configFile)
        {
            XmlConfigurator.ConfigureAndWatch(
                new FileInfo(configFile));
        }
        public Log4NetLogger(string category)
        {
            this.log = log4net.LogManager.GetLogger(category);
            this.category = category;
        }

        public void LogInfo(string message)
        {
            if (this.log.IsInfoEnabled)
            {
                this.log.Info(message);
            }
        }
        public void LogError(string message)
        {
            if (this.log.IsErrorEnabled)
            {
                this.log.Error(message);
            }
        }
        public void LogException(Exception ex)
        {
            if (this.log.IsFatalEnabled)
            {
                this.log.Fatal("Handled exception: {0}", ex);
            }
        }
        public void LogDebug(string message)
        {
            if (this.log.IsDebugEnabled)
            {
                this.log.Debug(message);
            }
        }
    }
}
