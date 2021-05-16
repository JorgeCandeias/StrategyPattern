namespace Library.Models
{
    public record OrderContext(
        decimal MarketPrice)
    {
        public static readonly OrderContext Empty = new(0);
    }
}