# Monkey - Model Context Protocol (MCP) Server

## Overview
This is a Model Context Protocol (MCP) server implementation built with .NET 9.0. The MCP server provides a communication protocol for facilitating interactions between various components in a model-driven system. This implementation demonstrates how to set up a basic MCP server with custom tools and services.

## Try it

### Quick Install for VS Code & VS

[![Install Remote Server in VS Code](https://img.shields.io/badge/VS_Code-Install_Remote_Server-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=monkeymcp&config=%7B%22type%22%3A%20%22http%22%2C%22url%22%3A%20%22https%3A%2F%2Ffunc-monkeymcp-3t4eixuap5dfm.azurewebsites.net%2F%22%7D) [![Install Remote Server in VS Code Insiders](https://img.shields.io/badge/VS_Code_Insiders-Install_Remote_Server-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=monkeymcp&config=%7B%22type%22%3A%20%22http%22%2C%22url%22%3A%20%22https%3A%2F%2Ffunc-monkeymcp-3t4eixuap5dfm.azurewebsites.net%2F%22%7D&quality=insiders)

[![Install Docker Container in VS Code](https://img.shields.io/badge/VS_Code-Install_Docker_Server-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=monkeymcp&config=%7B%22command%22%3A%22docker%22%2C%22args%22%3A%5B%22run%22%2C%22-i%22%2C%22--rm%22%2C%22jamesmontemagno%2Fmonkeymcp%22%5D%2C%22env%22%3A%7B%7D%7D) [![Install Docker Container in VS Code Insiders](https://img.shields.io/badge/VS_Code_Insiders-Install_Docker_Server-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](https://insiders.vscode.dev/redirect/mcp/install?name=monkeymcp&config=%7B%22command%22%3A%22docker%22%2C%22args%22%3A%5B%22run%22%2C%22-i%22%2C%22--rm%22%2C%22jamesmontemagno%2Fmonkeymcp%22%5D%2C%22env%22%3A%7B%7D%7D&quality=insiders)

[![Install Remote Server in Visual Studio](https://img.shields.io/badge/Visual_Studio-Install_Remote_Server-C16FDE?logo=visualstudio&logoColor=white)](https://vs-open.link/mcp-install?%7B%22name%22%3A%22monkeymcp%22%2C%22type%22%3A%22http%22%2C%22url%22%3A%22https%3A%2F%2Ffunc-monkeymcp-3t4eixuap5dfm.azurewebsites.net%2F%22%7D) [![Install Docker Container in Visual Studio](https://img.shields.io/badge/Visual_Studio-Install_Docker_Server-C16FDE?logo=visualstudio&logoColor=white)](https://vs-open.link/mcp-install?%7B%22name%22%3A%22monkeymcp%22%2C%22command%22%3A%22docker%22%2C%22args%22%3A%5B%22run%22%2C%22-i%22%2C%22--rm%22%2C%22jamesmontemagno%2Fmonkeymcp%22%5D%2C%22env%22%3A%7B%7D%7D)

Configure in VS Code with GitHub Copilot, Claude Desktop, or other MCP clients:

### Option 1: Remote Azure Functions Server (Recommended)
```json
{
    "servers": {
        "monkeymcp": {
            "url": "https://func-monkeymcp-3t4eixuap5dfm.azurewebsites.net/",
            "type": "http"
        }
    },
    "inputs": []
}
```

### Option 2: Docker Container
```json
{
    "inputs": [],
    "servers": {
        "monkeymcp": {
            "command": "docker",
            "args": [
                "run",
                "-i",
                "--rm",
                "jamesmontemagno/monkeymcp"
            ],
            "env": {}
        }
    }
}
```

## Features

### Core Components
- **MCP Server**: Built using the ModelContextProtocol library (version 0.1.0-preview.2)
- **Standard I/O Transport**: Uses stdio for communication with clients
- **Custom Tool Integration**: Includes examples of how to create and register MCP tools

### Services
- **MonkeyService**: A sample service that fetches monkey data from an API endpoint
  - Provides methods to retrieve a list of all monkeys or find a specific monkey by name
  - Caches results for better performance

- **MonkeyLocationService**: A service that generates unique journeys and activities for monkeys
  - Creates randomized path points based on monkey species behavior
  - Generates species-specific activities (grooming, howling, hot spring bathing, etc.)
  - Provides health statistics and movement patterns
  - Each journey includes fun activities like eating bananas, cleaning other monkeys, and exploration

### Available Tools
The server exposes several tools that can be invoked by clients:

#### Monkey Tools
- **GetMonkeys**: Returns a JSON serialized list of all available monkeys
- **GetMonkey**: Retrieves information about a specific monkey by name
- **GetMonkeyJourney**: Creates a unique journey path with activities and health stats for a specific monkey
- **GetAllMonkeyJourneys**: Generates journey paths for all available monkeys

## Configuration Options

### Hosting Configuration
The server uses Microsoft.Extensions.Hosting (version 9.0.3) which provides:
- Configuration from multiple sources (JSON, environment variables, command line)
- Dependency injection for services
- Logging capabilities

### Logging Options
Several logging providers are available:
- **Console**: Logs to standard output
- **Debug**: Logs for debugging purposes
- **EventLog**: Logs to the system event log (when running on Windows)
- **EventSource**: Provides ETW (Event Tracing for Windows) integration

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Basic understanding of the Model Context Protocol (MCP)

### Running the Server
1. Clone this repository
2. Navigate to the project directory
3. Build the project: `dotnet build`
4. Configure with VS Code or other client:

```json
"monkeyserver": {
    "type": "stdio",
    "command": "dotnet",
    "args": [
        "run",
        "--project",
        "/Users/jamesmontemagno/GitHub/TestMCP/MonkeyMCP/MonkeyMCP.csproj"
    ]
}
```

> Update the path to the project

### Extending the Server
To add custom tools:
1. Create a class and mark it with the `[McpServerToolType]` attribute
2. Add methods with the `[McpServerTool]` attribute
3. Optionally add `[Description]` attributes to provide documentation

Example:
```csharp
[McpServerToolType]
public static class CustomTool
{
    [McpServerTool, Description("Description of what the tool does")]
    public static string ToolMethod(string param) => $"Result: {param}";
}
```

## Server-Sent Events Implementation (MonkeyMCPSSE)

### Overview
The `MonkeyMCPSSE` project provides an alternative implementation of the Monkey MCP server using Server-Sent Events (SSE) over HTTP instead of stdio transport. This implementation runs as a web server, making it ideal for web-based clients and scenarios requiring HTTP-based communication.

> Read more about [SSE best practices for security here](https://modelcontextprotocol.io/docs/concepts/transports#security-warning%3A-dns-rebinding-attacks)

### Features
- **HTTP-based Transport**: Runs on `http://localhost:3001` by default
- **Server-Sent Events**: Enables real-time, one-way communication from server to client
- **ASP.NET Core Integration**: Built using ASP.NET Core's web server capabilities
- **MCP over HTTP**: Implements the Model Context Protocol over HTTP transport

### Running the SSE Server
1. Navigate to the MonkeyMCPSSE directory
2. Build and run the project:
3. Connect and run in VS Code or using MCP Inspector `npx @modelcontextprotocol/inspector`

### Implementation Details
The SSE implementation uses ASP.NET Core's built-in web server capabilities while maintaining the same monkey data service and tools as the stdio version. This makes it easy to switch between transport methods while keeping the core functionality intact.

## Project Structure
- **/MonkeyMCP**: Main project directory
  - **MonkeyService.cs**: Implementation of the service to fetch monkey data
  - **MonkeyTools.cs**: MCP tools for accessing monkey data
  - **Program.cs**: Entry point that configures and starts the MCP server

## Dependencies
- **Microsoft.Extensions.Hosting** (9.0.3): Provides hosting infrastructure
- **ModelContextProtocol** (0.1.0-preview.2): MCP server implementation
- **System.Text.Json** (9.0.3): JSON serialization/deserialization

## License
This project is available under the MIT License.

<!-- mcp-name: io.github.jamesmontemagno/monkeymcp -->

## MonkeyLocationService Features

The `MonkeyLocationService` provides rich simulation capabilities:

### Monkey Type Configurations
- **Baboons**: Savanna dwellers with social grooming behaviors and root foraging
- **Howler Monkeys**: Forest inhabitants known for territorial howling and leaf eating
- **Japanese Macaques**: Mountain monkeys that enjoy hot springs and snow play
- **Proboscis Monkeys**: Mangrove specialists that swim and show off their distinctive noses
- **Golden Snub-Nosed Monkeys**: High-altitude acrobats that huddle for warmth

### Journey Components
- **Path Points**: GPS coordinates showing where the monkey traveled
- **Activities**: Species-specific behaviors with energy costs/benefits
- **Health Stats**: Energy, happiness, hunger, social, stress, and health metrics
- **Time Tracking**: Start/end times and activity durations
- **Distance Calculation**: Total journey distance using Haversine formula

### Unique Activities Include
- Banana finding and eating
- Mutual grooming sessions
- Territory marking
- Hot spring bathing (macaques)
- Swimming (proboscis monkeys)
- Acrobatic displays
- Social interactions
- Foraging and exploration
