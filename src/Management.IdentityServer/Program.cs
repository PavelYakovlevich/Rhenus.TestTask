using Management.IdentityServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.SetupCors();
builder.SetupIdentityServer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors("OnlyFrontend");
app.UseIdentityServer();

app.Run();