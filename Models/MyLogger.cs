using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class MyLogger : ILogger
    {

        private static MyLogger instance;
        private static Logger logger;

        public static MyLogger GetInstance()
        {
            if (instance == null)
                instance = new MyLogger();
            return instance;

        }
        private Logger GetLogger(string theLogger)
        {
            if (MyLogger.logger == null)
                MyLogger.logger = LogManager.GetLogger(theLogger);

            return MyLogger.logger;
        }
        public void Debug(string msg, string arg = null)
        {
            if (arg == null)
                GetLogger("myAppLoggerRules").Debug(msg);
            else
                GetLogger("myAppLoggerRules").Debug(msg, arg);
        }

        public void Error(string msg, string arg = null)
        {
            if (arg == null)
                GetLogger("myAppLoggerRules").Error(msg);
            else
                GetLogger("myAppLoggerRules").Error(msg, arg);
        }

        public void Info(string msg, string arg = null)
        {
            if (arg == null)
                GetLogger("myAppLoggerRules").Info(msg);
            else
                GetLogger("myAppLoggerRules").Info(msg, arg);
        }

        public void Warning(string msg, string arg = null)
        {
            if (arg == null)
                GetLogger("myAppLoggerRules").Warn(msg);
            else
                GetLogger("myAppLoggerRules").Warn(msg, arg);
        }
    }
}