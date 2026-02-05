/*
A class to define the SQL database operations defined in the IStockRepository interface.
*/

using ApiServices.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pagination;
using PricerApi.Data;
using PricerApi.Models;
using PricerApi.StockRequests;

namespace PricerApi.StockRepository;

internal class SqlStockRepository : IStockRepository
{
    private readonly PaginationParameters _defaultPaginationParameters = new();
    private IMapper _automapper;

    public SqlStockRepository(IMapper mapper)
    {
        // Automapper only maps fields with an exact correspondance.
        _automapper = mapper;
    }

    public async Task<Stock> CreateStockRequest(
        PricerDbContext dbContext,
        CreateStockRequest stockRequests
    )
    {
        Stock stockMapper = _automapper.Map<Stock>(stockRequests);
        /*
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
        */
        await dbContext.AddAsync(stockMapper);
        await dbContext.SaveChangesAsync();

        return stockMapper;
    }

    public async Task<Stock?> GetStockRequestById(PricerDbContext dbContext, Guid id)
    {
        return await dbContext.Stocks.FirstOrDefaultAsync(a => a.Id == id);
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

    public async Task<Stock?> DeleteStockRequest(PricerDbContext dbContext, Guid id)
    {
        int success = 0;
        Stock? stock = await dbContext.Stocks.FirstOrDefaultAsync(a => a.Id == id);
        if (stock != null)
        {
            success = await dbContext.Stocks.Where(a => a.Id == id).ExecuteDeleteAsync();
        }
        if (success == 1)
        {
            return stock;
        }
        return null;
    }
}
