using KamaVerification.Services;
using KamaVerification.Data.Extensions;
using KamaVerification.Data.Mappers;
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

services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
