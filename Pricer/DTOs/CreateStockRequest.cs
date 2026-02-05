/*
A script to define our stock related operations.
*/

namespace PricerApi.StockRequests;

internal record class CreateStockRequest
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public long Volume { get; set; }
}
