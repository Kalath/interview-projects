using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Invelop.Project.Client.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            const string errMessage = "Unhandled exception with trace Id {0} occurred.";
            if (context.Exception is not null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);

                var traceId = System.Diagnostics.Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
                _logger.LogError(context.Exception, errMessage, traceId);
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
