using ManagementApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.SetupServices();
builder.Services.SetupDatabase();
builder.Services.SetupAuthentication();
builder.Services.SetupMapper();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseIdentityServer();

app.MapControllers();

app.Run();