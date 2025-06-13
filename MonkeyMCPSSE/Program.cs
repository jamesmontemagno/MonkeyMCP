using MonkeyMCPShared;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithPrompts<MonkeyPrompts>()
    .WithResources<MonkeyResources>()
    .WithTools<MonkeyTools>();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<MonkeyService>();

var app = builder.Build();

app.MapMcp();

app.Run();