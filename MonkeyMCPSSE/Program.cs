using MonkeyMCPSSE;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddMcpServer()
    .WithTools<MonkeyTools>();

builder.Services.AddSingleton<MonkeyService>();

var app = builder.Build();

app.MapMcp();

app.Run();