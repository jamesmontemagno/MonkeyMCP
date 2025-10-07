using MonkeyMCPShared;

var builder = WebApplication.CreateBuilder(args);

// Configure for Azure Functions custom handler
var port = Environment.GetEnvironmentVariable("FUNCTIONS_CUSTOMHANDLER_PORT") ?? "5000";
builder.WebHost.UseUrls($"http://localhost:{port}");

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