/*
A model to create trade requests.
*/

namespace PricerApi.Models;

public record class Trade
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public TradeType Type { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime ExecutedAt { get; set; }
    public decimal TotalValue => Price * Quantity;
    public string Status { get; set; } = "Pending";
}

public enum TradeType
{
    Buy,
    Sell,
}
