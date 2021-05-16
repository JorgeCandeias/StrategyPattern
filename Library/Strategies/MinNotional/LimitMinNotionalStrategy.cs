using Library.Models;
using Microsoft.Extensions.Options;
using System;

namespace Library.Strategies.MinNotional
{
    internal class LimitMinNotionalStrategy : IMinNotionalStrategy
    {
        private readonly MinNotionalStrategyOptions _options;

        public LimitMinNotionalStrategy(IOptions<MinNotionalStrategyOptions> options)
        {
            _options = options.Value;
        }

        public Order Adjust(Order order, OrderContext context)
        {
            if (order is null) throw new ArgumentNullException(nameof(order));

            if (order.Quantity * order.Price < _options.MinNotional)
            {
                order = order with
                {
                    Quantity = _options.MinNotional / order.Price
                };
            }

            return order;
        }

        public bool Supports(Order order)
        {
            if (order is null) throw new ArgumentNullException(nameof(order));

            return order.Type == OrderType.Limit;
        }
    }
}