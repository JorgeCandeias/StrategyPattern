using Library.Models;
using Microsoft.Extensions.Options;
using System;

namespace Library.Strategies.MinNotional
{
    internal class MarketMinNotionalStrategy : IMinNotionalStrategy
    {
        private readonly MinNotionalStrategyOptions _options;

        public MarketMinNotionalStrategy(IOptions<MinNotionalStrategyOptions> options)
        {
            _options = options.Value;
        }

        public Order Adjust(Order order, OrderContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            if (order.Quantity * context.MarketPrice < _options.MinNotional)
            {
                var quantity = _options.MinNotional / context.MarketPrice;

                return order with { Quantity = quantity };
            }

            return order;
        }

        public bool Supports(Order order)
        {
            if (order is null) throw new ArgumentNullException(nameof(order));

            return order.Type == OrderType.Market;
        }
    }
}