using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLab.Infrastructure.Log
{
    public abstract class LogContent
    {
        /// <summary>
        /// ID.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the Host Name.
        /// </summary>
        /// <value>The name of the host.</value>
        public string HostName { get; set; }

        /// <summary>
        /// Request.
        /// </summary>
        /// <value>The request.</value>
        public string Request { get; set; }

        /// <summary>
        /// Request Content.
        /// </summary>
        /// <value>The content of the request.</value>
        public string RequestContent { get; set; }

        /// <summary>
        /// Parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public string Parameters { get; set; }

        /// <summary>
        /// Custom Properties.
        /// </summary>
        /// <value>The custom properties.</value>
        public List<string> CustomProperties { get; set; }

        /// <summary>
        /// Client IP.
        /// </summary>
        /// <value>The client ip.</value>
        public string ClientIp { get; set; }

        /// <summary>
        /// QueryString.
        /// </summary>
        /// <value>The query string.</value>
        public string QueryString { get; set; }

        /// <summary>
        /// QueryString Name Value.
        /// </summary>
        /// <value>The query string name value.</value>
        public string QueryStringNameValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        /// <value>The name of the controller.</value>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>The name of the action.</value>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the LoggingId.
        /// </summary>
        /// <value>The LoggingId.</value>
        public string LoggingId { get; set; }
    }

}