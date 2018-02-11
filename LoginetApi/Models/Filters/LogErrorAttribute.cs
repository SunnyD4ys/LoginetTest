using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace LoginetApi.Models.Filters
{
    public class LogErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            WebApiApplication.Container.Logger.Log(actionExecutedContext.Exception.Message);
            actionExecutedContext.ActionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);

        }
    }
}