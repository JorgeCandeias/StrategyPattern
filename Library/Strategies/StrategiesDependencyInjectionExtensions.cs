using Library.Strategies.MinNotional;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StrategiesDependencyInjectionExtensions
    {
        public static IServiceCollection AddStrategies(this IServiceCollection services, Action<MinNotionalStrategyOptions> configure)
        {
            return services
                .AddSingleton<IMinNotionalStrategySelector, MinNotionalStrategySelector>()
                .AddSingleton<LimitMinNotionalStrategy>()
                .AddSingleton<IMinNotionalStrategy>(sp => sp.GetService<LimitMinNotionalStrategy>())
                .AddSingleton<MarketMinNotionalStrategy>()
                .AddSingleton<IMinNotionalStrategy>(sp => sp.GetService<MarketMinNotionalStrategy>())
                .AddOptions<MinNotionalStrategyOptions>()
                .Configure(configure)
                .ValidateDataAnnotations()
                .Services;
        }
    }
}