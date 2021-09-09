using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Common
{
    public class LogService 
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LogService()
        {

        }

        public void LogError(string message)
        {
            logger.Error(message);

        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

    }
}
