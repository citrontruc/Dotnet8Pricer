/*
A model to create quotations for our company.
*/

namespace PricerApi.Models;

public record class PriceQuote
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public decimal BidPrice { get; set; }
    public decimal AskPrice { get; set; }
    public decimal LastTradePrice { get; set; }
    public int BidSize { get; set; }
    public int AskSize { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal Spread => AskPrice - BidPrice;
    public decimal MidPrice => (BidPrice + AskPrice) / 2;
}
