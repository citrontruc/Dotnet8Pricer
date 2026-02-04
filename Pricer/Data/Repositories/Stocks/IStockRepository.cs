/*
An interface to define allowed methods in the stocks database.
*/

using PricerApi.Data;
using PricerApi.Models;
using PricerApi.StockRequests;
using ResultValues;

namespace PricerApi.StockRepository;

public interface IStockRepository
{
    public Task<Result> CreateStockRequest(
        PricerDbContext dbContext,
        CreateStockRequest stockRequests
    );
    public Task<Stock?> DeleteStockRequest(
        PricerDbContext dbContext,
        DeleteStockRequest stockRequests
    );
    public Task<Stock?> GetStockRequestById(
        PricerDbContext dbContext,
        GetStockRequest stockRequests
    );
    public Task<IEnumerable<Stock?>> GetAll(
        PricerDbContext dbContext,
        int? pageSize,
        int? pageNumber
    );
}
