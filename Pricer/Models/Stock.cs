/*
A model to present Stock values.
*/

namespace PricerApi.Models;

public class Stock
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal CurrentPrice { get; set; }
    public DateTime LastUpdated { get; set; }
    public decimal DayHigh { get; set; }
    public decimal DayLow { get; set; }
    public long Volume { get; set; }
    public decimal MarketCap { get; set; }
    public bool IsDeleted { get; set; }
}
