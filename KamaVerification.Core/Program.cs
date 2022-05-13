using System.Text.Json;
using System.Text.Json.Serialization;
using KamaVerification.Data.Extensions;
using KamaVerification.Services;
using KamaVerification.Data.Dtos;
using KamaVerification.Data.Mappers;
using KamaVerification.Data.Options;
using KamaVerification.Data.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddScoped<ITokenRepository, TokenRepository>()
    .AddScoped<IVerificationRepository, VerificationRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<ITenant, Tenant>()
    .AddHttpContextAccessor()
    .AddFluentValidators()
    .AddDataConfiguration(config)
    .AddJwtAuthentication(config)
    .AddAutoMapper(typeof(CustomerProfile).Assembly);

services.AddHealthChecks();

services.Configure<KamaVerificationDbOptions>(config.GetSection(KamaVerificationDbOptions.Section))
    .Configure<JwtOptions>(config.GetSection(JwtOptions.Section));

services.AddControllers();
services.AddMvcCore()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

var app = builder.Build();

app.UseCors(x => x.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.UseKamaVerificationExceptionMiddleware();
app.MapHealthChecks("/v1/health");
app.MapControllers();
app.Run();
