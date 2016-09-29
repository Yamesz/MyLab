using MyLab.Infrastructure.Help;
using MyLab.Infrastructure.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyLab.Infrastructure.MessageHandlers
{

        public class LogMessageHandler : DelegatingHandler
        {

            private string ProcessStartTime { get; set; }

            private string ProcessEndTime { get; set; }

            private string ProcessElapsedTicks { get; set; }

            private string ProcessElapsedMilliseconds { get; set; }

            private LogMessageContent LogMessage { get; set; }

            private NLogWriter NLogWriter { get; set; }

            private ExceptionlessWriter ExceptionlessWriter { get; set; }

            public LogMessageHandler()
            {
                this.NLogWriter = new NLogWriter();
                this.ExceptionlessWriter = new ExceptionlessWriter();
        }

            //-----------------------------------------------------------------------------------------

            /// <summary>
            /// send as an asynchronous operation.
            /// </summary>
            /// <param name="request">The HTTP request message to send to the server.</param>
            /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
            /// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.</returns>
            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {

                Stopwatch sw = new Stopwatch();
                sw.Reset();

                this.ProcessStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                sw.Start();

                var response = await base.SendAsync(request, cancellationToken);

                sw.Stop();

                this.ProcessEndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                this.ProcessElapsedTicks = string.Concat(sw.ElapsedTicks, " ticks");
                this.ProcessElapsedMilliseconds = string.Concat(sw.ElapsedMilliseconds, " ms");

                request.Headers.Add("Process-Start-Time", this.ProcessStartTime);
                request.Headers.Add("Process--End--Time", this.ProcessEndTime);

                request.Headers.Add(
                    "Process-Time-Elapsed",
                    string.Format("Ticks: {0}, {1} ms", sw.ElapsedTicks, sw.ElapsedMilliseconds));

                this.PrepareLogMessageContent(request, response);

                // NLog
                this.NLogWriter.WriteInfo(this.LogMessage);

                // Exceptionless
                this.ExceptionlessWriter.WriteInfo(this.LogMessage);

                return response;
            }

           

            /// <summary>
            /// Prepares the content of the log message.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="response">The response.</param>
            private async void PrepareLogMessageContent(
                HttpRequestMessage request,
                HttpResponseMessage response)
            {
                if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    return;
                }

                var requestContent = await request.Content.ReadAsStringAsync();
                var responseContent = await response.Content.ReadAsStringAsync();

                LogMessageContent logMessage = new LogMessageContent();
                logMessage.HostName = Environment.MachineName;

                logMessage.Request = request.ToString();
                logMessage.RequestContent = requestContent.ToString();
                logMessage.AbsolutePath = request.RequestUri.AbsolutePath;

                logMessage.ProcessStartTime = this.ProcessStartTime;
                logMessage.ProcessEndTime = this.ProcessEndTime;
                logMessage.ProcessElapsedTicks = this.ProcessElapsedTicks;
                logMessage.ProcessElapsedMilliseconds = this.ProcessElapsedMilliseconds;

                // to dectect Request Properties, if contains "Parameters", log it.
                var parameters = string.Empty;
                var properties = request.Properties;
                if (properties != null)
                {
                    var propertiesKeys = new List<string>();
                    propertiesKeys.AddRange(properties.Keys);
                    var targetProperty = propertiesKeys.FirstOrDefault(x => x.Contains("Parameter"));

                    if (targetProperty != null)
                    {
                        parameters = properties[targetProperty].ToString();
                    }
                }
                logMessage.Parameters = parameters;

                // Custom Properties (Request Body)
                logMessage.CustomProperties =
                    Utilities.GetCustomRequestProperties(request);

                // Client IP
                logMessage.ClientIp = Utilities.GetClientIp(request);

                // QueryString
                logMessage.QueryString = string.IsNullOrWhiteSpace(request.RequestUri.Query)
                    ? request.RequestUri.AbsoluteUri
                    : WebUtility.UrlDecode(request.RequestUri.Query);

                // QueryStringNameValues
                var queryStringNameValue =
                    Utilities.GetQueryStringNameValues(request);
                logMessage.QueryStringNameValue = queryStringNameValue;

                // Response
                logMessage.Response = response.ToString();
                logMessage.ResponseContent = responseContent.ToString();

                // Controller, ActionName
                var controllerActionName =
                    Utilities.GetControllerActionName(request);

                if (controllerActionName != null)
                {
                    logMessage.ControllerName = controllerActionName.Item1;
                    logMessage.ActionName = controllerActionName.Item2;
                }

            // LoggingId
            var naoeOfLoggingId = "X-loggingID";
            if (request.Headers.Contains(naoeOfLoggingId))
            {
                var loggingId = request.Headers.GetValues(naoeOfLoggingId).FirstOrDefault();
                logMessage.LoggingId = loggingId;
            }

            this.LogMessage = logMessage;
            }
        }
}