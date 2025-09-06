![logo.png](https://github.com/Ddemon26/Lmss/blob/main/.docs/logo.png)

Lightweight hosting and DI helpers for integrating LM Studio into .NET apps. Wraps the LmsSharp client with convenient services and a BackgroundService for health checks, chat, streaming, structured output, and tool use.

[![NuGet](https://img.shields.io/nuget/v/LmsSharp.Hosting.svg)](https://www.nuget.org/packages/LmsSharp.Hosting)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Installation

### From Source
Build the project locally:
```bash
git clone https://github.com/Ddemon26/Lmss.git
cd Lmss/Lmss.Hosting
dotnet build
```

### NuGet Package
```bash
dotnet add package LmsSharp.Hosting
```

### Local Development
Reference the project directly in your `.csproj`:
```xml
<ProjectReference Include="path/to/Lmss.Hosting/Lmss.Hosting.csproj" />
```

## Requirements

- .NET 8.0
- LM Studio running locally (default: http://localhost:1234)

## Quick Start

### Option 1: One‑liner factory
```csharp
using Lmss.Hosting;

var service = ServiceFactory.Create();
var reply = await service.SendMessageAsync("Hello LM Studio!");
Console.WriteLine(reply);
```

### Option 2: Dependency Injection (service helper)
```csharp
using Lmss.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // Registers ILmss + LmssService and optional settings
        services.AddService(opts =>
        {
            // Example: configure base URL or model
            // opts.BaseUrl = "http://localhost:1234";
            // opts.DefaultModel = "your-model-name";
        });
    })
    .Build();

await host.StartAsync();

var svc = host.Services.GetRequiredService<LmssService>();
var response = await svc.SendMessageAsync("Hi there!");
Console.WriteLine(response);

await host.StopAsync();
```

### Option 3: Background service
The package includes `LmssHostedService`, which continuously monitors readiness and can be extended for custom workflows.

```csharp
using Lmss.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // Registers ILmss and the background hosted service
        services.AddHostedService(opts =>
        {
            // opts.BaseUrl = "http://localhost:1234";
        });
    })
    .Build();

await host.RunAsync();
```

To customize periodic background work, subclass `LmssHostedService` and override `OnBackgroundExecuteAsync`.

## Features

- Dependency injection: `AddClient`, `AddService`, `AddHostedService`
- Background processing with readiness checks and logging
- Chat completions (simple and streaming)
- Structured JSON output using schemas
- Tool use/function calling workflow
- Model management and health checks

## Contributing

Contributions are welcome! Please feel free to submit issues, feature requests, or pull requests.

## License

This project is licensed under the MIT License — see the `LICENSE` file for details.

## Support

- LM Studio Docs: https://lmstudio.ai/docs
- GitHub Issues: https://github.com/Ddemon26/Lmss/issues
- API Reference: https://github.com/Ddemon26/Lmss/wiki

