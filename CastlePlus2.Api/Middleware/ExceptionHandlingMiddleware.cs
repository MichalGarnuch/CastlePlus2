using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Błąd walidacji: {Message}", ex.Message);
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            // 500 – log z pełnym wyjątkiem + traceId
            _logger.LogError(ex, "Unhandled exception. TraceId={TraceId}", context.TraceIdentifier);
            await HandleGenericExceptionAsync(context, ex, _env);
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";

        var errors = ex.Errors
            .GroupBy(e => string.IsNullOrWhiteSpace(e.PropertyName) ? "Request" : e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => string.IsNullOrWhiteSpace(x.ErrorMessage) ? "Niepoprawna wartość." : x.ErrorMessage)
                      .Distinct()
                      .ToArray());

        var problem = new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Błąd walidacji",
            Detail = "Dane wejściowe nie spełniają wymagań."
        };

        problem.Extensions["traceId"] = context.TraceIdentifier;

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem, JsonOptions));
    }

    private static async Task HandleGenericExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Wystąpił błąd serwera",
            // DEV: pokaż dokładny błąd (wraz z InnerException)
            // PROD: tylko ogólny tekst
            Detail = env.IsDevelopment()
                ? BuildDevError(ex)
                : "Wystąpił nieoczekiwany błąd."
        };

        problem.Extensions["traceId"] = context.TraceIdentifier;

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem, JsonOptions));
    }

    private static string BuildDevError(Exception ex)
    {
        var msg = ex.Message;
        if (ex.InnerException?.Message is { Length: > 0 } inner)
        {
            msg += $" | Inner: {inner}";
        }
        return msg;
    }
}
