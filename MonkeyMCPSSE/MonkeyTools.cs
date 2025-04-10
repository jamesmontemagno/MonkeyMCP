using System;
using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace MonkeyMCPSSE;

[McpServerToolType]
public sealed class MonkeyTools
{
    [McpServerTool, Description("Get a list of monkeys.")]
    public static async Task<string> GetMonkeys(MonkeyService monkeyService)
    {
        var monkeys = await monkeyService.GetMonkeys();
        return JsonSerializer.Serialize(monkeys, MonkeyContext.Default.ListMonkey);
    }

    [McpServerTool, Description("Get a monkey by name.")]
    public static async Task<string> GetMonkey(MonkeyService monkeyService, [Description("The name of the monkey to get details for")] string name)
    {
        var monkey = await monkeyService.GetMonkey(name);
        return JsonSerializer.Serialize(monkey, MonkeyContext.Default.Monkey);
    }
}
