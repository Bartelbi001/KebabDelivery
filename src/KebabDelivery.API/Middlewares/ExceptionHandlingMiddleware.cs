using Serilog;
using System.Net;
using System.Text.Json;
using KebabDelivery.Domain.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace KebabDelivery.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (DomainValidationException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Business validation error.");
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "Authorization error.");
        }
        catch (KeyNotFoundException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, "The resource was not found.");
        }
        catch (InvalidOperationException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict, "Data conflict.");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "Internal server error.");
        }
    }

    private Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var errors = ex.Errors.Select(e => new
        {
            Field = e.PropertyName,
            Error = e.ErrorMessage
        });

        var response = new
        {
            Message = "Validation error.",
            Errors = errors
        };

        Log.Warning("Validation error: {@Errors}", errors);

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            Message = message,
            Details = ex.Message
        };

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            Log.Fatal(ex, "Fatal error: {Message}", ex.Message);
        }
        else
        {
            Log.Error(ex, "An error has occurred: {Message}", ex.Message);
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}