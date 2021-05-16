namespace Library.Models
{
    public record Order(
        string Symbol,
        OrderSide Side,
        OrderType Type,
        TimeInForce? TimeInForce,
        decimal Quantity,
        decimal Price);
}