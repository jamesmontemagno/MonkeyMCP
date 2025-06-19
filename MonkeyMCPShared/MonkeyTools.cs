using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace MonkeyMCPShared;

[McpServerToolType]
public sealed class MonkeyTools
{
    private readonly MonkeyService monkeyService;

    public MonkeyTools(MonkeyService monkeyService)
    {
        this.monkeyService = monkeyService;
    }

    [McpServerTool, Description("Get a list of monkeys.")]
    public async Task<string> GetMonkeys()
    {
        var monkeys = await monkeyService.GetMonkeys();
        return JsonSerializer.Serialize(monkeys, MonkeyContext.Default.ListMonkey);
    }

    [McpServerTool, Description("Get a monkey by name.")]
    public async Task<string> GetMonkey([Description("The name of the monkey to get details for")] string name)
    {
        var monkey = await monkeyService.GetMonkey(name);
        return JsonSerializer.Serialize(monkey, MonkeyContext.Default.Monkey);
    }

    [McpServerTool, Description("Monkey Business, outputs monkey Unicode character")]
    public async Task<string> GetMonkeyBusiness()
    {
        return "🐵🐵🐵";
    }
}
