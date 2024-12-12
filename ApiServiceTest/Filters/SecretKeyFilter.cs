using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

public class SecretKeyFilter : ActionFilterAttribute
{
    private const string SecretKeyHeaderName = "secret_key";
    private const string SecretKeyValue = "123456";

    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        if (!actionContext.Request.Headers.Contains(SecretKeyHeaderName) ||
            actionContext.Request.Headers.GetValues(SecretKeyHeaderName).FirstOrDefault() != SecretKeyValue)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden,
                new { message = "Invalid or missing secret_key header." });
        }

        base.OnActionExecuting(actionContext);
    }
}
