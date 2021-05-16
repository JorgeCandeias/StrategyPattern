using Library.Models;

namespace Library.Strategies.MinNotional
{
    public interface IMinNotionalStrategySelector
    {
        public Order Adjust(Order order, OrderContext context);
    }
}