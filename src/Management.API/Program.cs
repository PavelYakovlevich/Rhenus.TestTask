using ManagementApp.API.Extensions;
using ManagementApp.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.SetupServices();
builder.SetupDatabase();
builder.SetupAuthentication();
builder.SetupMapper();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();