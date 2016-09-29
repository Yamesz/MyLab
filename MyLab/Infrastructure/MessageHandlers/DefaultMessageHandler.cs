using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyLab.Infrastructure.MessageHandlers
{
    public class DefaultMessageHandler : DelegatingHandler
    {
        private Guid loggingId;

        internal Guid LoggingId
        {
            get
            {
                this.loggingId = Guid.NewGuid();
                return loggingId;
            }
            set
            {
                loggingId = value;
            }
        }

        //public IUnityContainer DIContainer
        //{
        //    get
        //    {
        //        return DependencyInjectionHelper.GetContainer();
        //    }
        //}

        //-----------------------------------------------------------------------------------------

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var nameOfLoggingId = "X-loggingID";

            request.Headers.Add(nameOfLoggingId, this.LoggingId.ToString());
            return base.SendAsync(request, cancellationToken);
        }
    }
}