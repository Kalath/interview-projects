using Microsoft.AspNetCore.Mvc.Filters;
namespace Invelop.Project.Client.Filters
{
    public class UnhandledExceptionsFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public UnhandledExceptionsFilter(ILogger<UnhandledExceptionsFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("Unhandled exception occurred.", context.Exception);
        }
    }
}
