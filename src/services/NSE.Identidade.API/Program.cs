using NSE.Identidade.API.Configuration;

var builder = WebApplication.CreateBuilder(args);
var Environment = builder.Environment;

builder
    .Configuration.SetBasePath(Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

if (Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddApiConfiguration();

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.UseIdentityConfiguration(app.Environment);

app.MapControllers();

app.Run();
