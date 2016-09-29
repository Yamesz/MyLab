using Exceptionless;
using MyLab.Infrastructure.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyLab
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ExceptionlessClient.Default.RegisterWebApi(config);

            // Web API 設定和服務
            config.MessageHandlers.Add(new DefaultMessageHandler());
            config.MessageHandlers.Add(new LogMessageHandler());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
