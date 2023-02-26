using System.Net;
using FastDeliveryApi.Exceptions;
using Newtonsoft.Json;

namespace FastDeliveryApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            _logger.LogError(ex, $"Something went wrong while processing {context.Request.Path}");
            await HandleExceptionAsync(context, ex);
        }
    }
    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        var errorDetail = new ErrorDetails
        {
            ErrorType = "Failure",
            ErrorMessage = ex.Message
        };

        switch (ex)
        {
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorDetail.ErrorType = "Not Found";
                break;

            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                errorDetail.ErrorType = "Bad Request";
                break;

            case StatusChangementException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                errorDetail.ErrorType = "Bad Request";
                break;

            default:
                break;
        }

        string response = JsonConvert.SerializeObject(errorDetail);
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(response);
    }
    public class ErrorDetails
    {
        public string? ErrorType { get; set; }
        public string? ErrorMessage { get; set; }
    }
}