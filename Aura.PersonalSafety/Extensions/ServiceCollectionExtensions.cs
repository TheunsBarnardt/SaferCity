using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Aura.Services.Aura.Services;
using Aura.PersonalSafety.Services;
using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Handlers;

namespace Aura.PersonalSafety.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, string configSectionName)
        {
            services.Configure<ServiceOptions>(configuration.GetSection(configSectionName));

            services.AddSingleton<AuthService>();

            services.AddHttpClient<CalloutService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            services.AddHttpClient<ChatService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            services.AddHttpClient<CustomerService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            services.AddHttpClient<ImageMapService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            services.AddHttpClient<LeadService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            services.AddHttpClient<MiscService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            services.AddHttpClient<PanicService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            services.AddHttpClient<ResolutionReportService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            services.AddHttpClient<SubscriptionService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            services.AddHttpClient<UserService>()
                    .AddHttpMessageHandler<AuthorizedHandler>()
                    .ConfigurePrimaryHttpMessageHandler(b => new HttpClientHandler() { AllowAutoRedirect = false })
                    .SetHandlerLifetime(TimeSpan.FromMinutes(30));

            return services;
        }

        public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
        {
            // Configure Serilog to log to the console
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });

            return services;
        }
    }
}
