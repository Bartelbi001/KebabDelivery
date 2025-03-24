using Serilog;
using System.Net;
using System.Text.Json;
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
            await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "Ошибка авторизации");
        }
        catch (KeyNotFoundException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, "Ресурс не найден");
        }
        catch (InvalidOperationException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict, "Конфликт данных");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "Внутренняя ошибка сервера");
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
            Message = "Ошибка валидации",
            Errors = errors
        };

        Log.Warning("Ошибка валидации: {@Errors}", errors);

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
            Log.Fatal(ex, "Фатальная ошибка: {Message}", ex.Message);
        }
        else
        {
            Log.Error(ex, "Произошла ошибка: {Message}", ex.Message);
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}