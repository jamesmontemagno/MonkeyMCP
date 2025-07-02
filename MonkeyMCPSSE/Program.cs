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
builder.Services.AddSingleton<MonkeyLocationService>();

var app = builder.Build();

app.MapMcp();

app.Run();