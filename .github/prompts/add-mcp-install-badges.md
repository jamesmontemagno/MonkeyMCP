# Add MCP Install Badges to README

## Overview
This prompt helps you add one-click install badges for Model Context Protocol (MCP) servers to your README file. These badges allow users to install your MCP server directly into VS Code, VS Code Insiders, and Visual Studio with a single click.

## Prerequisites
Before using this prompt, ensure you have:
- [ ] A working MCP server configuration
- [ ] At least one deployment method (remote HTTP server, Docker container, or local binary)
- [ ] A README.md file in your repository

## Prompt Template

```
I need to add MCP install badges to my README.md file for easy one-click installation in VS Code and Visual Studio. Please add install buttons for the following configuration(s):

**My MCP Server Details:**
- Server Name: [YOUR_SERVER_NAME]
- Repository: [OWNER]/[REPO_NAME]

**Deployment Options (check all that apply):**
- [ ] Remote HTTP Server
  - URL: [YOUR_SERVER_URL]
- [ ] Docker Container  
  - Docker image: [YOUR_DOCKER_IMAGE]
- [ ] Local Binary
  - Command: [YOUR_COMMAND]
  - Args: [YOUR_ARGS]

**Target IDEs:**
- [ ] VS Code
- [ ] VS Code Insiders  
- [ ] Visual Studio

Please create install badges following this pattern and add them to my README.md file in an appropriate location (preferably near the top after any overview section).
```

## Example Configurations

### Remote HTTP Server
```json
{
  "name": "your-server-name",
  "type": "http",
  "url": "https://your-server-url.com/"
}
```

### Docker Container
```json
{
  "name": "your-server-name", 
  "command": "docker",
  "args": ["run", "-i", "--rm", "your-username/your-image"],
  "env": {}
}
```

### Local Binary
```json
{
  "name": "your-server-name",
  "command": "node", 
  "args": ["path/to/your/server.js"],
  "env": {}
}
```

## Badge URL Patterns

### VS Code & 

```
https://vscode.dev/redirect/mcp/install?name=[NAME]&config=[URL_ENCODED_CONFIG]
```

## VS Code Insiders
```
https://insiders.vscode.dev/redirect/mcp/install?name=[NAME]&config=[URL_ENCODED_CONFIG]
```
- For VS Code Insiders, add `&quality=insiders`

### Visual Studio  
```
https://vs-open.link/mcp-install?[URL_ENCODED_CONFIG]
```

## Badge Styling

### VS Code Badges
- **VS Code**: `0098FF` (blue) with `visualstudiocode` logo
- **VS Code Insiders**: `24bfa5` (green) with `visualstudiocode` logo

### Visual Studio Badges
- **Visual Studio**: `C16FDE` (purple) with `visualstudio` logo

## Example Badge Markdown

```markdown
[![Install in VS Code](https://img.shields.io/badge/VS_Code-Install_Server-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](URL_HERE)
[![Install in VS Code Insiders](https://img.shields.io/badge/VS_Code_Insiders-Install_Server-24bfa5?style=flat-square&logo=visualstudiocode&logoColor=white)](URL_HERE)
[![Install in Visual Studio](https://img.shields.io/badge/Visual_Studio-Install_Server-C16FDE?logo=visualstudio&logoColor=white)](URL_HERE)
```

## URL Encoding Helper

When creating the install URLs, remember to URL encode your JSON configuration. Common characters to encode:
- `{` → `%7B`
- `}` → `%7D`
- `"` → `%22`
- `:` → `%3A`
- `/` → `%2F`
- `[` → `%5B`
- `]` → `%5D`
- `,` → `%2C`

## Tips for Success

1. **Test your configuration first** - Make sure your MCP server works with the configuration before creating badges
2. **Place badges prominently** - Add them near the top of your README for maximum visibility
3. **Use descriptive text** - Consider adding a "Quick Install" section header
4. **Support multiple options** - If you have both remote and Docker deployments, provide badges for both
5. **Keep it organized** - Group badges logically (e.g., all VS Code options together)

## Example Implementation

See the [MonkeyMCP README](https://github.com/jamesmontemagno/monkeymcp/blob/main/README.md) for a complete example of how these badges are implemented with both remote server and Docker container options.

## Validation

After adding badges, verify they work by:
1. Clicking each badge to ensure the URLs are correct
2. Testing the installation flow in at least one target IDE
3. Confirming the server loads and functions properly after installation
