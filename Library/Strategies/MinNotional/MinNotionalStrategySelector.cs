using Library.Models;
using System.Collections.Generic;

namespace Library.Strategies.MinNotional
{
    internal class MinNotionalStrategySelector : IMinNotionalStrategySelector
    {
        private readonly IEnumerable<IMinNotionalStrategy> _strategies;

        public MinNotionalStrategySelector(IEnumerable<IMinNotionalStrategy> strategies)
        {
            _strategies = strategies;
        }

        public Order Adjust(Order order, OrderContext context)
        {
            foreach (var strategy in _strategies)
            {
                if (strategy.Supports(order))
                {
                    return strategy.Adjust(order, context);
                }
            }

            return order;
        }
    }
}