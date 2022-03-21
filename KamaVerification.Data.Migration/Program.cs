using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using KamaVerification.Data.Extensions;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDataConfiguration(config);
    })
    .Build();