using LoginEKO.FileProcessingService.Contracts.Responses;
using LoginEKO.FileProcessingService.Domain.Exceptions;

namespace LoginEKO.FileProcessingService.Api.Middlewares
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occured.");

            ErrorResponse response = exception switch
            {
                FilterValidationException filterValidationException => new ErrorResponse(System.Net.HttpStatusCode.BadRequest, "Invalid filter parameters", filterValidationException.Message),
                DataConversionException dataConversionException => new ErrorResponse(System.Net.HttpStatusCode.BadRequest, "Data conversion error", dataConversionException.Message),
                ArgumentException argumentException => new ErrorResponse(System.Net.HttpStatusCode.BadRequest, "Parameter is not valid", argumentException.Message),
                FileValidationException fileValidationException => new ErrorResponse(System.Net.HttpStatusCode.BadRequest, "Unable to process file", fileValidationException.Message),
                RepositoryException repositoryException => new ErrorResponse(System.Net.HttpStatusCode.InternalServerError, "Internal server error", repositoryException.Message),
                _ => new ErrorResponse(System.Net.HttpStatusCode.InternalServerError, "Internal server error", "Try again later"),
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}