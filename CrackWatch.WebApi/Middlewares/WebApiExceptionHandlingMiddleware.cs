using System.Net;
using System.Text.Json;
using CrackWatch.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CrackWatch.WebApi.Middlewares;

public class WebApiExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<WebApiExceptionHandlingMiddleware> _logger;

    public WebApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<WebApiExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        _logger.LogError("An unhandled error has occured, {EMessage}", e.Message);
        
        string result;

        if (e is DomainException)
        {
            var problemDetails = new ValidationProblemDetails(new Dictionary<string, string[]> { { "Error", new[] { e.Message } } })
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = (int)HttpStatusCode.BadRequest,
                Instance = context.Request.Path,
            };
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            result = JsonSerializer.Serialize(problemDetails);
        }else
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Internal Server Error",
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.Request.Path,
                Detail = "Internal server error occured!"
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            result = JsonSerializer.Serialize(problemDetails);
        }
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }
}