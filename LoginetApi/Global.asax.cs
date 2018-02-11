using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace LoginetApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Models.Interfaces.IContainer container;
        public static Models.Interfaces.IContainer Container
        {
            get { return container; }
            set { container = value; }
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            Container = new Models.TestContaiter();

            //для вывода json - ответа нужно добавить в запрос парамерт type=json
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

            //для вывода xml - ответа нужно добавить в запрос парамерт type=xml
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));
        }
    }
}
