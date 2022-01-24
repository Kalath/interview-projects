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
            if (context.Exception is not null)
            {
                context.Result = new ObjectResult("Error.")
                {
                    StatusCode = 500
                };

                _logger.LogError("Unhandled exception occurred.", context.Exception);
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
