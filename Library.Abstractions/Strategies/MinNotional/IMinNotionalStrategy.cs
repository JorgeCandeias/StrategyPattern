using Library.Models;

namespace Library.Strategies.MinNotional
{
    public interface IMinNotionalStrategy : IMinNotionalStrategySelector
    {
        public bool Supports(Order order);
    }
}