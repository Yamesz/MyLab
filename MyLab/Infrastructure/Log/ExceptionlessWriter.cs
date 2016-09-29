using Exceptionless;
using Exceptionless.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using z98.Utilities.Extensions;

namespace MyLab.Infrastructure.Log
{
    public class ExceptionlessWriter
    {
        /// <summary>
        /// Writes the LogMessageContent.
        /// </summary>
        /// <param name="logMessageContent">Content of the log message.</param>
        public void WriteInfo(LogMessageContent logMessageContent)
        {
            StringBuilder message = new StringBuilder();

            if (string.IsNullOrWhiteSpace(logMessageContent.HostName) == false)
            {
                message.AppendFormat("[ Host:{0} ] ",
                    logMessageContent.HostName);
            }

            if (string.IsNullOrWhiteSpace(logMessageContent.ControllerName) == false
                && string.IsNullOrWhiteSpace(logMessageContent.ActionName) == false)
            {
                message.AppendFormat("[ Controller:{0} , Action:{1} ] ",
                    logMessageContent.ControllerName,
                    logMessageContent.ActionName);
            }

            if (string.IsNullOrWhiteSpace(logMessageContent.LoggingId) == false)
            {
                message.AppendFormat("[ {0}:{1} ] ",
                    "LoggingId",
                    logMessageContent.LoggingId);
            }

            if (string.IsNullOrWhiteSpace(logMessageContent.ProcessElapsedMilliseconds) == false)
            {
                message.Append(logMessageContent.ProcessElapsedMilliseconds);
            }

            ExceptionlessClient.Default
                               .CreateLog(
                                    source: logMessageContent.AbsolutePath,
                                    message: message.ToString(),
                                    level: LogLevel.Info)
                               .AddObject(logMessageContent)
                               .Submit();
        }

        /// <summary>
        /// Writes the specified ErrorLogContent.
        /// </summary>
        /// <param name="errorLogContent">Content of the error log.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WriteError(ErrorLogContent errorLogContent)
        {
            StringBuilder message = new StringBuilder();

            if (!errorLogContent.HostName.IsNullOrWhiteSpace())
            {
                message.AppendFormat("[ Host:{0} ] ",
                    errorLogContent.HostName);
            }

            if (!errorLogContent.ControllerName.IsNullOrWhiteSpace() &&
                !errorLogContent.ActionName.IsNullOrWhiteSpace())
            {
                message.AppendFormat("[ Controller:{0} , Action:{1} ] ",
                    errorLogContent.ControllerName,
                    errorLogContent.ActionName);
            }
            if (!errorLogContent.LoggingId.IsNullOrWhiteSpace())
            {
                message.AppendFormat("[ {0}:{1} ] ",
                    "LoggingId",
                    errorLogContent.LoggingId);
            }

            ExceptionlessClient.Default
                               .CreateLog(
                                    source: errorLogContent.AbsolutePath,
                                    message: message.ToString(),
                                    level: LogLevel.Error)
                               .AddObject(errorLogContent)
                               .Submit();
        }
    }
}