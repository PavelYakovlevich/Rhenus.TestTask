﻿using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ManagementApp.API.Middlewares;

internal class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (NotFoundException exception)
        {
            await HandleException(httpContext,
                exception,
                StatusCodes.Status404NotFound,
                "Not found");
        }
        catch (AlreadyExistsException exception)
        {
            await HandleException(httpContext,
                exception,
                StatusCodes.Status400BadRequest,
                "Bad request");
        }
        catch (WrongCredentialsException exception)
        {
            await HandleException(httpContext,
                exception,
                StatusCodes.Status401Unauthorized,
                "Unauthorized");
        }
        catch (RegistrationFailedException exception)
        {
            await HandleException(httpContext,
                exception,
                StatusCodes.Status400BadRequest,
                "Bad request");
        }
        catch (Exception exception) 
        {
            await HandleException(httpContext,
                exception,
                StatusCodes.Status500InternalServerError,
                "Internal error");
        }
    }
    
    private static string GetExceptionDescriptionJson(Exception exception) =>
        JsonConvert.SerializeObject(new { exception.Message, exception.StackTrace },
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            });

    private static async Task HandleException(HttpContext context, Exception exception, int statusCode, string statusMessage)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = statusCode;
        var allMessageText = GetFullMessage(exception);

        await response.WriteAsync(JsonConvert.SerializeObject(new ProblemDetails
        {
            Detail = allMessageText,
            Status = statusCode,
            Title = statusMessage,
            Instance = context.Request.Path,
        }));
    }

    private static string GetFullMessage(Exception ex)
    {
        if (ex.InnerException != null)
        {
            return ex.Message + "; " + GetFullMessage(ex.InnerException);
        }

        return ex.Message;
    }
}

internal static class ExceptionHandlerMiddlewareExtension
{
    public static void UseExceptionHandler(this WebApplication app) => app.UseMiddleware<ExceptionHandlerMiddleware>();
}
