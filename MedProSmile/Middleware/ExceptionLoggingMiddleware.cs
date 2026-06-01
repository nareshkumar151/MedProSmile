using MedProSmile.Repository;

namespace MedProSmile.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionLoggingMiddleware> _logger;
        private readonly IExceptionLogger _exceptionLogger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger, IExceptionLogger exceptionLogger)
        {
            _next = next;
            _logger = logger;
            _exceptionLogger = exceptionLogger;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                await LogExceptionAsync(ex, context.Request.Path);

                // Re-throw the exception to be handled by the error handling middleware
                throw;
            }
        }
        private async Task LogExceptionAsync(Exception ex, string requestPath)
        {
            // Create a structured log entry
            var logMessage = new
            {
                Timestamp = DateTime.UtcNow,
                ExceptionType = ex.GetType().Name,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                RequestPath = requestPath,
                Source = ex.Source
            };

            // Log as information (could be changed to error based on your logging level)
            _logger.LogInformation("{@ExceptionDetails}", logMessage);

            // If you want to store in a database or external service:
            await _exceptionLogger.LogExceptionAsync(ex, nameof(requestPath) + " " + requestPath);

        }


    }
}

