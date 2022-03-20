using KamaVerification.Services;
using KamaVerification.Email.Services;
using SendGrid;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddScoped<IEmailVerificationRepository, EmailVerificationRepository>()
    .AddSendGrid(o =>
    {
        o.ApiKey = config["SendGrid:ApiKey"];
    });

services.AddScoped<IVerificationRepository, VerificationRepository>()
    .AddScoped<IEmailVerificationRepository, EmailVerificationRepository>()
    .AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
