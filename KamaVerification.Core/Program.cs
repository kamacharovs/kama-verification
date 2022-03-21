using KamaVerification.Data.Extensions;
using KamaVerification.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddScoped<IVerificationRepository, VerificationRepository>()
    .AddDataConfiguration(config);

services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
