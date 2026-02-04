/*
A class to define the SQL database operations defined in the IStockRepository interface.
*/

using Microsoft.EntityFrameworkCore;
using Pagination;
using PricerApi.Data;
using PricerApi.Models;
using PricerApi.StockRequests;

namespace PricerApi.StockRepository;

public class SqlStockRepository : IStockRepository
{
    private readonly PaginationParameters _defaultPaginationParameters = new();

    public async Task<Stock> CreateStockRequest(
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

        return stock;
    }

    public async Task<Stock?> GetStockRequestById(PricerDbContext dbContext, Guid id)
    {
        return await dbContext.Stocks.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Stock?> GetStockRequestById(
        PricerDbContext dbContext,
        GetStockRequest stockRequests
    )
    {
        return await GetStockRequestById(dbContext, stockRequests.Id);
    }

    public async Task<IEnumerable<Stock?>> GetAll(
        PricerDbContext dbContext,
        int? pageSize,
        int? pageNumber
    )
    {
        (int correctPageSize, int correctPageNumber) =
            PaginationParameters.CorrectPaginationParameters(
                _defaultPaginationParameters,
                pageSize,
                pageNumber
            );

        return await PagedList<Stock>.ToPagedListAsync(
            dbContext.Stocks.AsNoTracking().OrderBy(a => a.Id),
            correctPageSize,
            correctPageNumber
        );
    }

    public async Task<Stock?> DeleteStockRequest(
        PricerDbContext dbContext,
        DeleteStockRequest stockRequests
    )
    {
        int success = 0;
        Stock? stock = await dbContext.Stocks.FirstOrDefaultAsync(a => a.Id == stockRequests.Id);
        if (stock != null)
        {
            success = await dbContext
                .Stocks.Where(a => a.Id == stockRequests.Id)
                .ExecuteDeleteAsync();
        }
        if (success == 1)
        {
            return stock;
        }
        return null;
    }
}
