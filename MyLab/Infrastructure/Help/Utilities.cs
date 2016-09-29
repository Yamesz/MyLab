using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Text.RegularExpressions;
using System.Web;

namespace MyLab.Infrastructure.Help
{
    public class Utilities
    {
        /// <summary>
        /// MS_HttpContext
        /// </summary>
        internal static readonly string HttpContextBaseKey = "MS_HttpContext";

        

        //-----------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        public static string GetUserAgent(HttpRequestMessage request)
        {
            var result = string.Empty;
            if (request.Properties.ContainsKey(HttpContextBaseKey))
            {
                HttpContextBase context = (HttpContextBase)request.Properties[HttpContextBaseKey];
                result = context.Request.UserAgent;
            }
            else if (HttpContext.Current != null)
            {
                result = HttpContext.Current.Request.UserAgent;
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// Gets the client ip.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        public static string GetClientIp(HttpRequestMessage request)
        {
            var result = string.Empty;
            if (request.Properties.ContainsKey(HttpContextBaseKey))
            {
                HttpContextBase context = (HttpContextBase)request.Properties[HttpContextBaseKey];
                result = context.Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                var prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                result = prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                result = string.Empty;
            }

            if (result.Equals("::1"))
            {
                result = "127.0.0.1";
            }
            return result;
        }

        /// <summary>
        /// Determines whether [is local ip address] [the specified HTTP request].
        /// </summary>
        /// <param name="httpContextBase">The HTTP context base.</param>
        /// <returns><c>true</c> if [is local ip address] [the specified HTTP request]; otherwise, <c>false</c>.</returns>
        public static bool IsLocalIPAddress(HttpContextBase httpContextBase)
        {
            bool result = false;
            string ipAddress = httpContextBase.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            try
            {
                if (!string.IsNullOrEmpty(ipAddress) &&
                    ipAddress.ToUpper().IndexOf("UNKNOWN") < 0)
                {
                    result = CheckIPAddress(ipAddress);
                }
                if (!result && string.IsNullOrWhiteSpace(ipAddress))
                {
                    ipAddress = httpContextBase.Request.ServerVariables["REMOTE_ADDR"];
                    if (ipAddress.Equals("::1"))
                    {
                        // 127.0.0.1
                        return true;
                    }
                    result = CheckIPAddress(ipAddress);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Checks the ip address.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CheckIPAddress(string ipAddress)
        {
            bool result = false;
            Regex reg = new Regex(@"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$");
            if (ipAddress.IndexOf(",") > -1 || ipAddress.IndexOf(";") > -1)
            {
                //有","或";"，估計為多代理。取第一個不是内網的IP。
                ipAddress = ipAddress.Replace('"', ' ').Replace(" ", "");
                string[] ipstr = ipAddress.Split(",;".ToCharArray());
                for (int i = 0; i < ipstr.Length; i++)
                {
                    if (reg.IsMatch(ipstr[i]) &&
                        ipstr[i].StartsWith("10.") ||
                        ipstr[i].StartsWith("192.168") ||
                        ipstr[i].StartsWith("172.16."))
                    {
                        result = true;
                        break;
                    }
                }
            }
            else
            {
                if (ipAddress.StartsWith("10.") ||
                    ipAddress.StartsWith("192.168") ||
                    ipAddress.StartsWith("172.16."))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 取得 Request 裡 Custom Properties 資料內容.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <remarks>多半都是以 Request.Body 傳遞到後端，然後在 ValidateAttribute 裡做過一次轉換</remarks>
        public static List<string> GetCustomRequestProperties(HttpRequestMessage request)
        {
            var properties = request.Properties;
            var customProperties = new Dictionary<string, object>();
            if (properties != null &&
                properties.Keys.Any(x => !x.StartsWith("MS_")))
            {
                var otherProperties =
                    properties.Where(x => !x.Key.StartsWith("MS_", StringComparison.OrdinalIgnoreCase));

                foreach (var item in otherProperties)
                {
                    customProperties.Add(item.Key, item.Value);
                }
            }

            List<string> customPropertiesContents = new List<string>();
            foreach (var item in customProperties)
            {
                customPropertiesContents.Add(string.Format("{0}: {1}", item.Key, item.Value));
            }
            if (customProperties.Any())
            {
                customPropertiesContents.Insert(0, "Request Body: ");
                customPropertiesContents.Add("-------------------------------------------------");
            }
            return customPropertiesContents;
        }

        /// <summary>
        /// Gets the query string name values.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        public static string GetQueryStringNameValues(HttpRequestMessage request)
        {
            var nameValues = new List<string>();
            foreach (var pair in request.GetQueryNameValuePairs())
            {
                nameValues.Add(string.Concat(
                    "{", string.Format("{0}:{1}", pair.Key, pair.Value), "}"));
            }
            var nameValuesResult = string.Format("[{0}]", string.Join(",", nameValues));
            return nameValuesResult;
        }

        /// <summary>
        /// Gets the name of the controller and action.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Tuple&lt;System.String, System.String&gt;.</returns>
        public static Tuple<string, string> GetControllerActionName(HttpRequestMessage request)
        {
            var httpConfiguration = request.GetConfiguration();
            var routeData = request.GetRouteData();

            var controllerContext =
                new System.Web.Http.Controllers.HttpControllerContext(
                    httpConfiguration, routeData, request);

            // get controller Name
            var controllerSelector =
                new System.Web.Http.Dispatcher.DefaultHttpControllerSelector(httpConfiguration);

            var controllerDescriptor = controllerSelector.SelectController(request);
            controllerContext.ControllerDescriptor = controllerDescriptor;

            var controllerName = controllerDescriptor.ControllerName;

            // get action name
            var actionName = request.GetActionDescriptor().ActionName;

            var result = new Tuple<string, string>(controllerName, actionName);
            return result;
        }
    }
}