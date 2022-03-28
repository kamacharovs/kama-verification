using KamaVerification.Data.Extensions;
using KamaVerification.Services;
using KamaVerification.Services.Middlewares;
using KamaVerification.Data.Mappers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddScoped<IVerificationRepository, VerificationRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddDataConfiguration(config)
    .AddAutoMapper(typeof(CustomerProfile).Assembly);

services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
