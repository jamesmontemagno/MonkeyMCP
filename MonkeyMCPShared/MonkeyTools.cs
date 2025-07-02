using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace MonkeyMCPShared;

[McpServerToolType]
public sealed class MonkeyTools
{
    private readonly MonkeyService monkeyService;
    private readonly MonkeyLocationService locationService;

    public MonkeyTools(MonkeyService monkeyService, MonkeyLocationService locationService)
    {
        this.monkeyService = monkeyService;
        this.locationService = locationService;
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

    [McpServerTool, Description("Monkey Business, outputs random monkey and monkey-adjacent emoji")]
    public Task<string> GetMonkeyBusiness()
    {
        var monkeyEmojis = new[] { "🐵", "🐒", "🦍", "🦧", "🙈", "🙉", "🙊", "🍌", "🌴", "🥥", "🌿", "🐾" };
        var random = new Random();
        var count = random.Next(3, 7); // 3 to 6 inclusive
        
        var result = "";
        for (int i = 0; i < count; i++)
        {
            result += monkeyEmojis[random.Next(monkeyEmojis.Length)];
        }
        
        return Task.FromResult(result);
    }

    [McpServerTool, Description("Get a unique journey path with activities and health stats for a specific monkey.")]
    public async Task<string> GetMonkeyJourney([Description("The name of the monkey to get a journey for")] string name)
    {
        var monkey = await monkeyService.GetMonkey(name);
        if (monkey == null)
        {
            return JsonSerializer.Serialize(new { error = $"Monkey '{name}' not found" });
        }

        var journey = locationService.GenerateMonkeyJourney(monkey);
        return JsonSerializer.Serialize(journey, MonkeyLocationContext.Default.MonkeyJourney);
    }

    [McpServerTool, Description("Get journey paths for all available monkeys.")]
    public async Task<string> GetAllMonkeyJourneys()
    {
        var monkeys = await monkeyService.GetMonkeys();
        var journeys = new List<MonkeyJourney>();

        foreach (var monkey in monkeys)
        {
            var journey = locationService.GenerateMonkeyJourney(monkey);
            journeys.Add(journey);
        }

        return JsonSerializer.Serialize(journeys, MonkeyLocationContext.Default.ListMonkeyJourney);
    }
}
