using Library.Strategies.MinNotional;

namespace Library.Models
{
    public record Order(
        string Symbol,
        OrderSide Side,
        OrderType Type,
        TimeInForce? TimeInForce,
        decimal Quantity,
        decimal Price)
    {
        public Order WithMinNotional(IMinNotionalStrategySelector strategy, OrderContext context)
        {
            return strategy.Adjust(this, context);
        }
    }
}