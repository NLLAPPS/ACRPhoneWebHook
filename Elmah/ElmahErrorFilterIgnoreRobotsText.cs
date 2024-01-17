using ElmahCore;

namespace ACRPhone.Webhook.Elmah
{
    public class ElmahErrorFilterIgnoreRobotsText : IErrorFilter
    {
        public void OnErrorModuleFiltering(object sender, ExceptionFilterEventArgs args)
        {
            if (args.Exception.GetBaseException() is FileNotFoundException)
                args.Dismiss();

            if (args.Context is HttpContext httpContext)
            {
                if (httpContext.Response.StatusCode == 404 && httpContext.Request.Path == "/robots.txt")
                    args.Dismiss();

            }
        }
    }
}
