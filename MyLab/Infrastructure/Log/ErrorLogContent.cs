using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLab.Infrastructure.Log
{
    public class ErrorLogContent : LogContent
    {
        /// <summary>
        /// Gets or sets the ValidateException.
        /// </summary>
        /// <value>The exception.</value>
        public ValidateException Exception { get; set; }

        /// <summary>
        /// Gets or sets the request URI.
        /// </summary>
        /// <value>The request URI.</value>
        public string RequestUri { get; set; }

        /// <summary>
        /// Gets or sets the RequestUri.AbsolutePath.
        /// </summary>
        /// <value>The absolute path.</value>
        public string AbsolutePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLogContent"/> class.
        /// </summary>
        public ErrorLogContent()
        {
            this.ID = Guid.NewGuid();
            this.Exception = null;
        }
    }
}