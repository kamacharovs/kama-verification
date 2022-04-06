using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using KamaVerification.Data.Extensions;
using KamaVerification.Services;
using KamaVerification.Data;
using KamaVerification.Data.Mappers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddScoped<ITokenRepository, TokenRepository>()
    .AddScoped<IVerificationRepository, VerificationRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddDataConfiguration(config)
    .AddAutoMapper(typeof(CustomerProfile).Assembly);

services.AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config[Keys.JwtIssuer],
            ValidAudience = config[Keys.JwtAudience],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[Keys.JwtKey]))
        };
    });

services.AddControllers();
services.AddMvcCore()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
