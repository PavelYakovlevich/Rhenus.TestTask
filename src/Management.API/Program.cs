using ManagementApp.API.Extensions;
using ManagementApp.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.SetupServices();
builder.Services.SetupDatabase();
builder.Services.SetupAuthentication();
builder.Services.SetupMapper();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseIdentityServer();

app.MapControllers();

app.Run();