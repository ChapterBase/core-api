namespace admin_bff.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode = exception switch
            {
                InvalidOperationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                message = exception.Message,
                error = exception.GetType().Name
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }

}
