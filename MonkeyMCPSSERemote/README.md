# Monkey MCP Server - Azure Functions Remote

## Overview
This is the Azure Functions deployment version of the Monkey Model Context Protocol (MCP) server. It runs as a serverless function on Azure, providing HTTP-based access to the same monkey data and tools as the local versions.

## Live Server
🚀 **Production URL**: https://func-monkeymcp-3t4eixuap5dfm.azurewebsites.net/

## Configuration

To use this remote MCP server in your MCP clients (VS Code with GitHub Copilot, Claude Desktop, etc.), add the following configuration:

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

## Features

This remote server provides the same functionality as the local versions:

### Available Tools
- **GetMonkeys**: Returns a JSON serialized list of all available monkeys
- **GetMonkey**: Retrieves information about a specific monkey by name  
- **GetMonkeyJourney**: Creates a unique journey path with activities and health stats for a specific monkey
- **GetAllMonkeyJourneys**: Generates journey paths for all available monkeys
- **GetMonkeyBusiness**: Returns random monkey and monkey-adjacent emoji

### Available Resources
- **monkey-data**: Access to the complete monkey dataset
- **monkey-locations**: Geographic and habitat information

### Available Prompts
- **analyze-monkey**: Analyze monkey characteristics and behaviors
- **monkey-comparison**: Compare different monkey species

## Deployment

This server is deployed using Azure Functions with the following architecture:
- **Runtime**: .NET 9.0 (dotnet-isolated)
- **Hosting**: Azure Functions Flex Consumption Plan
- **Transport**: HTTP with Server-Sent Events (SSE)
- **Infrastructure**: Deployed via Azure Developer CLI (azd)

## Development

To deploy your own instance:

1. Install Azure Developer CLI:
   ```bash
   winget install microsoft.azd
   ```

2. Navigate to this directory and deploy:
   ```bash
   cd MonkeyMCPSSERemote
   azd up
   ```

The deployment includes:
- Azure Function App
- Storage Account
- Application Insights
- Virtual Network (optional)
- All necessary role-based access control (RBAC) assignments

## Technical Details

- **Custom Handler**: Uses Azure Functions custom handler to run the .NET MCP server
- **Port Configuration**: Automatically configures to listen on `FUNCTIONS_CUSTOMHANDLER_PORT`
- **Stateless**: Designed to be fully stateless for serverless scaling
- **HTTP Transport**: Uses ModelContextProtocol.AspNetCore with HTTP transport

## Benefits of Remote Deployment

- ✅ **No Local Setup**: No need to install .NET or build the project locally
- ✅ **Always Available**: 24/7 availability without running local processes
- ✅ **Automatic Scaling**: Scales up/down based on demand
- ✅ **Global Access**: Accessible from anywhere with internet connection
- ✅ **Maintenance-Free**: Automatic updates and security patches

## Support

For issues with the remote server or deployment questions, please file an issue in the main repository.