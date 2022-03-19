using KamaVerification.Services;
using KamaVerification.Email.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddFluentEmail("localhost@kamaverification")
    .AddSmtpSender("smtp.example.com", 587);

services.AddScoped<IVerificationRepository, VerificationRepository>()
    .AddScoped<IEmailVerificationRepository, EmailVerificationRepository>()
    .AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
