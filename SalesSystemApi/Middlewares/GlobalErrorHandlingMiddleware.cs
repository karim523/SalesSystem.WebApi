using SalesSystem.Domain.ErrorModels;
using SalesSystem.Domain.Exceptions;
using System.Net;
using ValidationException = SalesSystem.Domain.Exceptions.ValidationException;

namespace SalesSystemApi.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate? _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandleNotFoundApiAsync(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                await HandleException(httpContext, ex);

                throw;
            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The end point {httpContext.Request.Path} not found"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        private async Task HandleException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorDetails
            {
                ErrorMessage = ex.Message
            };

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,//404
                ValidationException validationException => HandleValidationException(validationException, response),//400 
                _ => (int)HttpStatusCode.InternalServerError//500
            };

            response.StatusCode = httpContext.Response.StatusCode;

            await httpContext.Response.WriteAsync(response.ToString());
        }

        private int HandleValidationException(ValidationException validationException, ErrorDetails response)
        {
            response.Errors = validationException.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
