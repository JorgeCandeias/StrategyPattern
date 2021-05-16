using Library.Models;
using Library.Strategies.MinNotional;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Library.Tests
{
    public class MinNotionalStrategySelectorTests
    {
        private readonly IHost _host;

        public MinNotionalStrategySelectorTests()
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
        public void BumpsLowNotionalOnMarketOrder()
        {
            // arrange
            var selector = _host.Services.GetRequiredService<IMinNotionalStrategySelector>();
            var order = new Order("BTCGBP", OrderSide.Buy, OrderType.Market, null, 1, 0);

            // act
            var result = selector.Adjust(order, new OrderContext(5));

            // assert
            Assert.Equal(2, result.Quantity);
        }

        [Fact]
        public void BumpsLowNotionalOnLimitOrder()
        {
            // arrange
            var selector = _host.Services.GetRequiredService<IMinNotionalStrategySelector>();
            var order = new Order("BTCGBP", OrderSide.Buy, OrderType.Limit, null, 1, 5);

            // act
            var result = selector.Adjust(order, OrderContext.Empty);

            // assert
            Assert.Equal(2, result.Quantity);
        }

        [Fact]
        public void IgnoresOtherOrders()
        {
            // arrange
            var selector = _host.Services.GetRequiredService<IMinNotionalStrategySelector>();
            var order = new Order("BTCGBP", OrderSide.Buy, OrderType.None, null, 1, 5);

            // act
            var result = selector.Adjust(order, OrderContext.Empty);

            // assert
            Assert.Equal(1, result.Quantity);
        }
    }
}