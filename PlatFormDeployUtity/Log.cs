using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployUtility
{
    public class Log
    {
        private static readonly ILog InfoLogger = LogManager.GetLogger("infoAppender");
        private static readonly ILog ErrorLogger = LogManager.GetLogger("errorAppender");
        private static readonly ILog DebugLogger = LogManager.GetLogger("debugAppender");
        private static readonly ILog WarnLogger = LogManager.GetLogger("warnAppender");

        private static readonly bool isErrorEnabled = ErrorLogger.IsErrorEnabled;
        private static readonly bool isInfoEnabled = InfoLogger.IsInfoEnabled;
        private static readonly bool isDebugEnabled = DebugLogger.IsDebugEnabled;
        private static readonly bool isWarnEnabled = WarnLogger.IsWarnEnabled;

        public static void Warn(string msg)
        {
            if (isWarnEnabled)
            {
                WarnLogger.Warn(msg);
            }
        }
        public static void Info(string msg)
        {
            if (isInfoEnabled)
            {
                InfoLogger.Info(msg);
            }
        }
        public static void Debug(string msg)
        {
            if (isDebugEnabled)
            {
                DebugLogger.Debug(msg);
            }
        }
        public static void Error(string msg)
        {
            if (isErrorEnabled)
            {
                ErrorLogger.Error(msg);
            }
        }
    }
}
