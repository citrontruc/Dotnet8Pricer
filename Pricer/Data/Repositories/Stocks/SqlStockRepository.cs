/*
A class to define the SQL database operations defined in the IStockRepository interface.
*/

using Microsoft.EntityFrameworkCore;
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

    public async Task<Stock?> GetStockRequestById(PricerDbContext dbContext, GetStockRequest stockRequests)
    {
        return await dbContext.Stocks.FirstOrDefaultAsync(a => a.Id == stockRequests.Id);
    }

    public async Task<Stock?> GetAll(PricerDbContext dbContext)
    {
        return null;
    }

    public async Task<Stock?> DeleteStockRequest(PricerDbContext dbContext, DeleteStockRequest stockRequests)
    {
        int success = 0;
        Stock? stock = await dbContext.Stocks.FirstOrDefaultAsync(a => a.Id == stockRequests.Id);
        if (stock != null)
        {
            success = await dbContext.Stocks.Where(a => a.Id == stockRequests.Id).ExecuteDeleteAsync();
        }
        if (success == 1)
        {
            return stock;
        }
        return null;
    }
}
