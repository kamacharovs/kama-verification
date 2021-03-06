using System.Text.Json;
using System.Text.Json.Serialization;
using KamaVerification.Services;
using KamaVerification.Data.Extensions;
using KamaVerification.Data.Mappers;
using KamaVerification.Data.Options;
using KamaVerification.Email.Data;
using KamaVerification.Email.Services;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddScoped<ITokenRepository, TokenRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IVerificationRepository, VerificationRepository>()
    .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>()
    .AddScoped<IEmailVerificationRepository, EmailVerificationRepository>()
    .AddAutoMapper(typeof(CustomerProfile).Assembly)
    .AddDataConfiguration(config)
    .AddJwtAuthentication(config)
    .AddSendGrid(o =>
    {
        o.ApiKey = config[Keys.EmailApiKey];
    });

services.AddHealthChecks();

services.Configure<KamaVerificationDbOptions>(config.GetSection(KamaVerificationDbOptions.Section))
    .Configure<JwtOptions>(config.GetSection(JwtOptions.Section));

services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/v1/health");
app.MapControllers();
app.Run();
