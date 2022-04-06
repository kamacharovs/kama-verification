using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using KamaVerification.Data.Extensions;
using KamaVerification.Data;
using KamaVerification.Data.Migrations;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDataConfiguration(config);
    })
    .Build();

var context = host.Services.GetService<KamaVerificationDbContext>();
var fakeDataManager = new FakeDataManager(context);

await context.Database.MigrateAsync();
await fakeDataManager.SeedDataAsync();