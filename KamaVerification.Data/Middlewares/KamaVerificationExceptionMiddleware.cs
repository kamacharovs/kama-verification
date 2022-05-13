using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using KamaVerification.Data.Exceptions;

namespace KamaVerification.Data.Middlewares
{
    public class KamaVerificationProblemDetails : KamaVerificationProblemDetailsBase
    {
        [JsonPropertyName("trace_id")]
        public string TraceId { get; set; }

        [JsonPropertyName("errors")]
        public IEnumerable<string> Errors { get; set; }
    }

    public class KamaVerificationProblemDetailsBase
    {
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class KamaVerificationExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;
        private readonly RequestDelegate _next;

        private const string _defaultMessage = "An unexpected error has occurred";
        private const string _defaultValidationMessage = "One or more validation errors have occurred. Please see errors for details";

        public KamaVerificationExceptionMiddleware(
            ILogger<KamaVerificationExceptionMiddleware> logger,
            IWebHostEnvironment env,
            RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                if (httpContext.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                var id = string.IsNullOrEmpty(httpContext?.TraceIdentifier)
                    ? Guid.NewGuid().ToString()
                    : httpContext.TraceIdentifier;

                _logger.LogError(
                    e,
                    $"an exception was thrown during the request. {id}");

                await WriteExceptionResponseAsync(
                    httpContext,
                    e,
                    id);
            }
        }

        private async Task WriteExceptionResponseAsync(
            HttpContext httpContext,
            Exception e,
            string id)
        {
            var canViewSensitiveInfo = _env
                .IsDevelopment();

            var problem = new KamaVerificationProblemDetails()
            {
                Message = canViewSensitiveInfo
                    ? e.Message
                    : _defaultMessage,
                Code = StatusCodes.Status500InternalServerError,
                TraceId = $"kama:verification:error:{id}"
            };

            if (e is KamaVerificationException ae)
            {
                problem.Code = ae.StatusCode;
                problem.Message = ae.Message;
            }
            else if (e is ValidationException ve)
            {
                problem.Code = StatusCodes.Status400BadRequest;
                problem.Message = _defaultValidationMessage;
                problem.Errors = ve.Errors.Select(x => x.ErrorMessage);
            }

            var problemjson = JsonSerializer
                .Serialize(problem, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

            httpContext.Response.StatusCode = problem.Code ?? StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response
                .WriteAsync(problemjson);
        }
    }

    public static class KamaVerificationExceptionExtensions
    {
        public static IApplicationBuilder UseKamaVerificationExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<KamaVerificationExceptionMiddleware>();
        }
    }
}