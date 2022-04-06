using System.Text.Json;
using KamaVerification.Data.Extensions;
using KamaVerification.Services;
using KamaVerification.Data.Mappers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddScoped<ITokenRepository, TokenRepository>()
    .AddScoped<IVerificationRepository, VerificationRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddDataConfiguration(config)
    .AddJwtAuthentication(config)
    .AddAutoMapper(typeof(CustomerProfile).Assembly);

services.AddControllers();
services.AddMvcCore()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
