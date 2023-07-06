using Newtonsoft.Json;

namespace TaskManagementSystem.GlobalErrorHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        // Async => return Task Not Void
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotFoundBusinessException exception:
                        _logger.Log(LogLevel.Debug, exception, "NotFoundBusinessException");
                        await HandleExceptionAsync(httpContext, StatusCodes.Status404NotFound, "Catalog-NotFound",
                            exception.Message);
                        break;
                    //case KeyNotFoundException exception:
                    //    await HandleExceptionAsync(httpContext, StatusCodes.Status404NotFound, "Catalog-KeyNotFound",
                    //        exception.Message);
                    //    break;
                    case GenericBusinessException exception:
                        _logger.Log(LogLevel.Debug, exception, "GenericBusinessException");
                        await HandleExceptionAsync(httpContext, StatusCodes.Status400BadRequest, "Catalog-GenericBussinessValidation",
                            exception.Message);
                        break;

                    case ConflictBusinessException exception:
                        _logger.Log(LogLevel.Debug, exception, "ConflictBusinessException");
                        await HandleExceptionAsync(httpContext, StatusCodes.Status400BadRequest, "Error", exception.Message);
                        //await HandleExceptionAsync(httpContext, StatusCodes.Status400BadRequest, "Catalog-ConflictBussinessValidation",
                        //    exception.Message);
                        break;

                    case { } exception:
                        Console.WriteLine(exception);
                        await HandleExceptionAsync(httpContext, StatusCodes.Status500InternalServerError,
                            "Catalog-InternalServiceError", exception.Message);
                        break;
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string error, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var result = JsonConvert.SerializeObject(new
            {
                //type = $"https://pik.sa/catalog/errors/{error}",
                title = error,
                status = statusCode,
                detail = message,
                traceId = Guid.NewGuid().ToString()
            });
            await context.Response.WriteAsync(result);
        }
    }
}
