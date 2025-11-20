using Microsoft.AspNetCore.Diagnostics;
using Bernhoeft.GRT.Core.Models;
using System.Net;

namespace Bernhoeft.GRT.Teste.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var result = OperationResult<object>
                .ReturnInternalServerError()
                .AddMessage("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");

            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}