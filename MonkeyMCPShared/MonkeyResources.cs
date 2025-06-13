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


    [McpServerResource, Description("Get monkey details by name")]
    public async Task<string> Monkey(string name)
    {
        var monkey = await monkeyService.GetMonkey(name) ?? throw new Exception($"{name} not found");
        return JsonSerializer.Serialize(monkey, MonkeyContext.Default.Monkey);
    }
}
