using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLab.Infrastructure.Log
{
    public class NLogWriter
    {
        /// <summary>
        /// Writes the log message content.
        /// </summary>
        /// <param name="messageContent">Content of the message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WriteInfo(LogMessageContent messageContent)
        {
            List<string> logContent = new List<string>();
            logContent.Add(Environment.NewLine);

            if (string.IsNullOrWhiteSpace(messageContent.LoggingId) == false)
            {
                logContent.Add("LoggingId: ");
                logContent.Add(messageContent.LoggingId);
            }

            if (string.IsNullOrWhiteSpace(messageContent.ControllerName) == false)
            {
                logContent.Add("Controller: ");
                logContent.Add(messageContent.ControllerName);
            }
            if (string.IsNullOrWhiteSpace(messageContent.ActionName) == false)
            {
                logContent.Add("Action: ");
                logContent.Add(messageContent.ActionName);
            }

            logContent.Add("Request: ");
            logContent.Add(messageContent.Request);

            logContent.Add("Request Content: ");
            logContent.Add(messageContent.RequestContent);

            if (string.IsNullOrWhiteSpace(messageContent.Parameters) == false)
            {
                logContent.Add("Parameters: ");
                logContent.Add(messageContent.Parameters);
                logContent.Add("-------------------------------------------------");
            }

            if (messageContent.CustomProperties.Any())
            {
                logContent.AddRange(messageContent.CustomProperties);
                logContent.Add("-------------------------------------------------");
            }

            logContent.Add("Client IP: ");
            logContent.Add(string.Format("[{0}]", messageContent.ClientIp));

            logContent.Add("QueryString: ");
            logContent.Add(messageContent.QueryString);

            logContent.Add("QueryStringNameValue: ");
            logContent.Add(messageContent.QueryStringNameValue);

            logContent.Add("-------------------------------------------------");

            logContent.Add("Response: ");
            logContent.Add(messageContent.Response);

            logContent.Add("Response Content: ");
            logContent.Add(messageContent.ResponseContent);

            logContent.Add("=================================================");

            string logMessage = string.Join(Environment.NewLine, logContent.ToArray());

            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(logMessage);
        }

        ///// <summary>
        ///// Writes the specified error log content.
        ///// </summary>
        ///// <param name="errorLogContent">Content of the error log.</param>
        ///// <exception cref="System.NotImplementedException"></exception>
        //public void WriteError(ErrorLogContent errorLogContent)
        //{
        //    List<string> logContent = new List<string>();

        //    if (!errorLogContent.LoggingId.IsNullOrWhiteSpace())
        //    {
        //        logContent.Add("LoggingId: ");
        //        logContent.Add(errorLogContent.LoggingId);
        //    }

        //    if (!errorLogContent.ControllerName.IsNullOrWhiteSpace())
        //    {
        //        logContent.Add("Controller: ");
        //        logContent.Add(errorLogContent.ControllerName);
        //    }
        //    if (!errorLogContent.ActionName.IsNullOrWhiteSpace())
        //    {
        //        logContent.Add("Action: ");
        //        logContent.Add(errorLogContent.ActionName);
        //    }

        //    logContent.Add("Request: ");
        //    logContent.Add(errorLogContent.Request);

        //    logContent.Add("Request Content: ");
        //    logContent.Add(errorLogContent.RequestContent);

        //    if (!errorLogContent.Parameters.IsNullOrWhiteSpace())
        //    {
        //        logContent.Add("Parameters: ");
        //        logContent.Add(errorLogContent.Parameters);
        //        logContent.Add("-------------------------------------------------");
        //    }

        //    if (errorLogContent.CustomProperties.Any())
        //    {
        //        logContent.AddRange(errorLogContent.CustomProperties);
        //        logContent.Add("-------------------------------------------------");
        //    }

        //    logContent.Add("Client IP: ");
        //    logContent.Add(string.Format("[{0}]", errorLogContent.ClientIp));

        //    logContent.Add("QueryString: ");
        //    logContent.Add(errorLogContent.QueryString);

        //    logContent.Add("QueryStringNameValue: ");
        //    logContent.Add(errorLogContent.QueryStringNameValue);

        //    logContent.Add("-------------------------------------------------");

        //    if (errorLogContent.Exception.IsNotNull())
        //    {
        //        var exceptionContent =
        //            JsonConvert.SerializeObject(errorLogContent.Exception.Result);

        //        logContent.Add("Response Content: ");
        //        logContent.Add(exceptionContent);
        //        logContent.Add("-------------------------------------------------");
        //    }

        //    string logMessage = string.Join(Environment.NewLine, logContent.ToArray());

        //    NLog.Logger logger = null;

        //    if (errorLogContent.Exception.IsNull())
        //    {
        //        logger = NLog.LogManager.GetLogger("WithoutException");
        //        logger.Error(logMessage);
        //    }
        //    else
        //    {
        //        logger = NLog.LogManager.GetLogger("WithException");
        //        logger.ErrorException(logMessage, errorLogContent.Exception);
        //    }
        //}
    }
}