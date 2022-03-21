using KamaVerification.Services;
using KamaVerification.Data.Extensions;
using KamaVerification.Email.Data;
using KamaVerification.Email.Services;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddScoped<IEmailVerificationRepository, EmailVerificationRepository>()
    .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>()
    .AddSendGrid(o =>
    {
        o.ApiKey = config[Keys.EmailApiKey];
    });

services.AddScoped<IVerificationRepository, VerificationRepository>()
    .AddScoped<IEmailVerificationRepository, EmailVerificationRepository>()
    .AddDataConfiguration(config)
    .AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
