/*
A class to define the SQL database operations defined in the IStockRepository interface.
*/

using PricerApi.Data;
using PricerApi.Models;
using PricerApi.StockRequests;
using ResultValues;

namespace PricerApi.StockRepository;

public class SqlStockRepository : IStockRepository
{
    public async Task<Result> CreateStockRequest(
        PricerDbContext dbContext,
        CreateStockRequest stockRequests
    )
    {
        Stock stock = new Stock
        {
            Id = Guid.NewGuid(),
            Symbol = stockRequests.Symbol,
            CompanyName = stockRequests.CompanyName,
            CurrentPrice = stockRequests.Price,
            LastUpdated = DateTime.Now,
            Volume = stockRequests.Volume,
            IsDeleted = false,
        };
        await dbContext.AddAsync(stock);
        await dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public void DeleteStockRequest(PricerDbContext dbContext, DeleteStockRequest stockRequests) { }

    public void GetStockRequestById(PricerDbContext dbContext, GetStockRequest stockRequests) { }

    public void GetAll(PricerDbContext dbContext) { }
}
