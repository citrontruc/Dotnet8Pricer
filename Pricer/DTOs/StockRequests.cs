/*
A script to define our stock related operations.
*/

namespace PricerApi.StockRequests;

public record class CreateStockRequest
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public long Volume { get; set; }
}

public record class DeleteStockRequest
{
    public Guid Id { get; set; }
}

public record class GetStockRequest
{
    public Guid Id { get; set; }
}
