using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;

namespace GameDashboardProject.Application.Exceptions
{
    public class ExceptionMiddleware:IExceptionHandler
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Exception occurred:{exception.Message}");

            var status = GetStatusCode(exception);

            httpContext.Response.StatusCode = status;
            var problemDetails = new ProblemDetails();
            problemDetails.Status = status;
            if (exception.GetType() == typeof(ValidationException))
            {
                var errors = ((ValidationException)exception).Errors.Select(x => x.ErrorMessage).ToList();
                problemDetails.Title = string.Join(", ", errors);
            }
            else
                problemDetails.Title = $"{exception.Message}";


            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
