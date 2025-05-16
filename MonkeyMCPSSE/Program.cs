using MonkeyMCPSSE;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithTools<MonkeyTools>();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<MonkeyService>();

var app = builder.Build();

app.MapMcp();

app.Run();