/*
An interface to define allowed methods in the stocks database.
*/

using PricerApi.Data;
using PricerApi.Models;
using PricerApi.StockRequests;

namespace PricerApi.StockRepository;

internal interface IStockRepository
{
    public Task<Stock> CreateStockRequest(
        PricerDbContext dbContext,
        CreateStockRequest stockRequests
    );

    public Task<Stock?> DeleteStockRequest(PricerDbContext dbContext, Guid id);

    public Task<Stock?> GetStockRequestById(PricerDbContext dbContext, Guid id);

    public Task<IEnumerable<Stock?>> GetAll(
        PricerDbContext dbContext,
        int? pageSize,
        int? pageNumber
    );
}
