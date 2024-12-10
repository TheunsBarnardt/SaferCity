
# Aura.PersonalSafety

## API Documentation

-** https://personal.aura.services/
-** https://fixed.aura.services/
-** https://gateway.aura.services/

Aura.PersonalSafety is a C# project designed to provide personal safety services through structured APIs, 
handling requests and responses for various functionalities such as authentication, chat, callouts, and more.

## Features

- **Authentication Services**: Manages user authentication and authorization.
- **Chat Services**: Facilitates chat-related functionality.
- **Callout Services**: Handles callout requests and responses.
- **Customer Management**: Provides tools for managing customer data and interactions.
- **Panic Services**: Manages panic alerts, locations, and related data.
- **Resolution Reporting**: Handles resolution reports for various incidents.
- **Subscription Management**: Manages user subscriptions and related data.

## Project Structure

### Configuration
- `Config/ServiceOptions.cs`: Defines configuration options for the services.

### Extensions
- `Extensions/DateTimeExtensions.cs`: Provides utility functions for date-time operations.
- `Extensions/HttpResponseMessageExtensions.cs`: Adds extensions for HTTP response handling.
- `Extensions/ServiceCollectionExtensions.cs`: Adds service collection extensions for dependency injection.

### Handlers
- `Handlers/AuthorizedHandler.cs`: Custom handler for authorized requests.
- `Handlers/UnauthorizedHandler.cs`: Custom handler for unauthorized requests.

### Models
- **Auth**: Authentication-related request and response models.
- **Callout**: Models for callout requests and responses.
- **Chat**: Models for chat operations.
- **Customer**: Models for customer-related data and operations.
- **Panics**: Models for managing panic events, including location details.
- **Subscriptions**: Models for subscription data and interactions.

### Services
- `AuthService.cs`: Handles authentication-related operations.
- `ChatService.cs`: Provides chat functionalities.
- `CalloutService.cs`: Manages callouts.
- `CustomerService.cs`: Handles customer data.
- `PanicService.cs`: Manages panic alerts.
- `ResolutionReportService.cs`: Processes resolution reports.
- `SubscriptionService.cs`: Manages user subscriptions.

## Integration into Another Project

To integrate this project into another application, follow these steps:

### Step 1: Add a Reference and appsettings.json

Add the `Aura.PersonalSafety` project to your solution and reference it in your main project.
```json
{
  "ServiceOptions": {
    "BaseUrl": "https://staging-panic.aura.services/panic-api/v2/",
    "CustomerId": "your-customer-id",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret"
  }
}
```

### Step 2: Configure Services

Use the `ServiceCollectionExtensions` to configure services and inject the `ServiceOptions`:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Aura.PersonalSafety.Extensions;
using Aura.PersonalSafety.Config;

public class Startup
{
   public void ConfigureServices(IServiceCollection services)
   {
        // Register your services with a dynamic configuration section name
        services.AddServices(Configuration, "ServiceOptions");

        // Add Serilog logging
        services.AddSerilogLogging(Configuration);

        // Register other services
        services.AddHttpClient<LeadService>()
                .AddHttpMessageHandler<AuthorizedHandler>()
                .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                .SetHandlerLifetime(TimeSpan.FromMinutes(30));
   }
}
```

### Step 3: Call a Service

Here’s an example of how to use the `PanicService`:

```csharp
using System.Threading.Tasks;
using Aura.PersonalSafety.Services;
using Aura.PersonalSafety.Models.Panics;

public class ExampleUsage
{
    private readonly PanicService _panicService;

    public ExampleUsage(PanicService panicService)
    {
        _panicService = panicService;
    }

    public async Task TriggerPanicAsync()
    {
        var panicRequest = new PanicRequest
        {
            UserId = "12345",
            Location = new Location
            {
                Latitude = 37.7749,
                Longitude = -122.4194
            },
            Message = "Emergency Alert!"
        };

        PanicResponse response = await _panicService.TriggerPanicAsync(panicRequest);

        if (response.IsSuccess)
        {
            Console.WriteLine("Panic triggered successfully!");
        }
        else
        {
            Console.WriteLine("Failed to trigger panic.");
        }
    }
}
```

## How `AuthorizedHandler` Works

The `AuthorizedHandler` is a custom HTTP message handler used to attach authorization headers to outgoing HTTP requests. It ensures that all requests include the necessary API key or token.

### Key Functionality

1. **Attaches Authorization Headers**: 
   - Adds an `Authorization` header to each outgoing request.

2. **Simplifies Secure Communication**:
   - Centralizes the handling of authentication headers for all HTTP clients.

### Example

The handler can be registered when configuring `HttpClient`:

```csharp
services.AddHttpClient("AuthorizedClient")
    .AddHttpMessageHandler<AuthorizedHandler>();
```

This ensures that any `HttpClient` using the `"AuthorizedClient"` configuration automatically includes the authorization header.
