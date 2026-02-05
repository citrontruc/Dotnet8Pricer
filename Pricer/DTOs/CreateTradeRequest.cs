/*
A script to define our trade operations.
*/

namespace PricerApi.TradeRequests;

internal record class CreateTradeRequest
{
    public string Symbol { get; set; } = string.Empty;
    public required string Type { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
