using System;
using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace MonkeyMCPShared;

[McpServerResourceType]
public class MonkeyResources
{
    private readonly MonkeyService monkeyService;

    public MonkeyResources(MonkeyService monkeyService)
    {
        this.monkeyService = monkeyService;
    }


    [McpServerResource, Description("Baboon")]
    public async Task<string> Baboon()
    {
        var monkey = await monkeyService.GetMonkey("Baboon") ?? throw new Exception("Baboon not found");
        return JsonSerializer.Serialize(monkey);
    }

    [McpServerResource, Description("Capuchin")]
    public async Task<string> Capuchin()
    {
        var monkey = await monkeyService.GetMonkey("Capuchin Monkey") ?? throw new Exception("Capuchin not found");
        return JsonSerializer.Serialize(monkey);
    }

    [McpServerResource, Description("Red-shanked douc")]
    public async Task<string> RedShankedDouc()
    {
        var monkey = await monkeyService.GetMonkey("Red-shanked Douc") ?? throw new Exception("Red-shanked Douc not found");
        return JsonSerializer.Serialize(monkey);
    }
}
