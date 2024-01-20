using System.Net;
using FastBubberDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FastBubberDinner.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ServiceException exception)
        {
            await HandleServiceExceptionAsync(context, exception);
        }
        catch (Exception exception)
        {
            await HandleNonServiceExceptionAsync(context, exception);
        }
    }

    public static Task HandleServiceExceptionAsync(HttpContext context, ServiceException exception)
    {
        var result = JsonConvert.SerializeObject(new
        {
            Title = exception.Message,
            exception.Detail,
            exception.Status
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.Status;

        return context.Response.WriteAsync(result);
    }

    public static Task HandleNonServiceExceptionAsync(HttpContext context, Exception exception)
    {
        var result = JsonConvert.SerializeObject(new
        {
            Title = "Internal Server Error",
            Detail = exception.Message,
            Status = 500
            
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        return context.Response.WriteAsync(result);
    }
}