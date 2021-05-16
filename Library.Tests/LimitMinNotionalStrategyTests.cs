using Library.Models;
using Library.Strategies.MinNotional;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Library.Tests
{
    public class LimitMinNotionalStrategyTests
    {
        private readonly IHost _host;

        public LimitMinNotionalStrategyTests()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddStrategies(options =>
                    {
                        options.MinNotional = 10;
                    });
                })
                .Build();
        }

        [Fact]
        public void SupportsLimitOrder()
        {
            // arrange
            var strategy = _host.Services.GetRequiredService<LimitMinNotionalStrategy>();
            var order = new Order("BTCGBP", OrderSide.Buy, OrderType.Limit, null, 1, 12345);

            // act
            var result = strategy.Supports(order);

            // assert
            Assert.True(result);
        }

        [Fact]
        public void DoesNotSupportsMarketOrder()
        {
            // arrange
            var strategy = _host.Services.GetRequiredService<LimitMinNotionalStrategy>();
            var order = new Order("BTCGBP", OrderSide.Buy, OrderType.Market, null, 1, 0);

            // act
            var result = strategy.Supports(order);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void BumpsLowNotional()
        {
            // arrange
            var strategy = _host.Services.GetRequiredService<LimitMinNotionalStrategy>();
            var order = new Order("BTCGBP", OrderSide.Buy, OrderType.Limit, null, 1, 5);

            // act
            var result = strategy.Adjust(order, OrderContext.Empty);

            // assert
            Assert.Equal(2, result.Quantity);
        }

        [Fact]
        public void IgnoresHighNotional()
        {
            // arrange
            var strategy = _host.Services.GetRequiredService<LimitMinNotionalStrategy>();
            var order = new Order("BTCGBP", OrderSide.Buy, OrderType.Limit, null, 3, 5);

            // act
            var result = strategy.Adjust(order, OrderContext.Empty);

            // assert
            Assert.Equal(3, result.Quantity);
        }
    }
}