using KamaVerification.Services;
using KamaVerification.Data.Extensions;
using KamaVerification.Email.Data;
using KamaVerification.Email.Services;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddScoped<IVerificationRepository, VerificationRepository>()
    .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>()
    .AddScoped<IEmailVerificationRepository, EmailVerificationRepository>()
    .AddDataConfiguration(config)
    .AddSendGrid(o =>
    {
        o.ApiKey = config[Keys.EmailApiKey];
    });

services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
