using Management.IdentityServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.SetupIdentityServer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseIdentityServer();

app.Run();