//using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace ToDoApp.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // 🔥 LOG CENTRALISÉ
            _logger.LogError(
                exception,
                "Unhandled exception | Path: {Path} | Method: {Method}",
                context.Request.Path,
                context.Request.Method
            );

            ProblemDetails problem = exception switch
            {
                ValidationException ve => CreateValidationProblem(ve),
                KeyNotFoundException => CreateProblem(
                    StatusCodes.Status404NotFound,
                    "Resource not found",
                    exception.Message
                ),
                UnauthorizedAccessException => CreateProblem(
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    exception.Message
                ),
                _ => CreateServerProblem(exception)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode =
                problem.Status ?? StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(problem)
            );
        }

        private ProblemDetails CreateValidationProblem(ValidationException exception)
        {
            return new ValidationProblemDetails(
                exception.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    ))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation failed"
            };
        }

        private ProblemDetails CreateServerProblem(Exception exception)
        {
            return new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = _env.IsDevelopment()
                    ? exception.Message
                    : "An unexpected error occurred"
            };
        }

        private ProblemDetails CreateProblem(int status, string title, string detail)
        {
            return new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail
            };
        }
    }
}
