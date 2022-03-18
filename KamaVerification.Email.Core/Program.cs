var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddFluentEmail("localhost@kamaverification")
    .AddSmtpSender("localhost", 25);

services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
