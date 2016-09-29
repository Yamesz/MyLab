using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLab.Infrastructure.Log
{
    public class LogMessageContent : LogContent
    {
        /// <summary>
        /// Gets or sets the absolute path.
        /// </summary>
        /// <value>The absolute path.</value>
        public string AbsolutePath { get; set; }

        /// <summary>
        /// Process Start Time.
        /// </summary>
        /// <value>The process start time.</value>
        public string ProcessStartTime { get; set; }

        /// <summary>
        /// Process End Time.
        /// </summary>
        /// <value>The process end time.</value>
        public string ProcessEndTime { get; set; }

        /// <summary>
        /// Process Elapsed Ticks.
        /// </summary>
        /// <value>The process time elapsed.</value>
        public string ProcessElapsedTicks { get; set; }

        /// <summary>
        /// Process Elapsed Milliseconds.
        /// </summary>
        /// <value>The process elapsed milliseconds.</value>
        public string ProcessElapsedMilliseconds { get; set; }

        /// <summary>
        /// Response.
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; set; }

        /// <summary>
        /// Response Content.
        /// </summary>
        /// <value>The content of the response.</value>
        public string ResponseContent { get; set; }

        public LogMessageContent()
        {
            this.ID = Guid.NewGuid();
        }
    }
}