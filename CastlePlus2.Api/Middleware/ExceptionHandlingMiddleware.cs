using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Middleware;

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
        catch (ValidationException ex)
        {
            // Obsługa błędów walidacji (FluentValidation) -> 400 Bad Request
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            // Obsługa innych błędów (np. baza padła) -> 500 Internal Server Error
            _logger.LogError(ex, "Wystąpił nieoczekiwany błąd.");
            await HandleGenericExceptionAsync(context, ex);
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";

        var errors = ex.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());

        var problem = new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Błąd walidacji",
            Detail = "Jeden lub więcej błędów walidacji wystąpiło."
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }

    private static async Task HandleGenericExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var problem = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Wystąpił błąd serwera",
            Detail = ex.Message // Na produkcji ukryj to!
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
}
