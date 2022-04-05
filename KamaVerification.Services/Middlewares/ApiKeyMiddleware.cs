using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KamaVerification.Services.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "API Key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"{APIKEYNAME} was not provided");

                return;
            }

            var customerRepo = context.RequestServices.GetRequiredService<ICustomerRepository>();
            var customer = await customerRepo.GetAsync(apiKey);

            if (customer is null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");

                return;
            }

            await _next(context);
        }
    }
}
