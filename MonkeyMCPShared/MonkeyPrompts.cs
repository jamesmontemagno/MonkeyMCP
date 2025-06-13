using System;
using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MonkeyMCPShared;

[McpServerPromptType]
public class MonkeyPrompts
{
    [McpServerPrompt, Description("Get a list of monkeys.")]
    public static string GetMonkeysPrompt()
    {
        return "Please provide a list of monkeys and organize them by their name and put them in a table.";
    }

    [McpServerPrompt, Description("Get a monkey by name.")]
    public static string GetMonkeyPrompt([Description("The name of the monkey to get details for")] string name)
    {
        return $"Please provide details for the monkey named {name}.";
    }
}
