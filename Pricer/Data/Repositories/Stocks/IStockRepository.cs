/*
An interface to define allowed methods in the stocks database.
*/

using PricerApi.Data;
using PricerApi.StockRequests;
using ResultValues;

namespace PricerApi.StockRepository;

public interface IStockRepository
{
    public Task<Result> CreateStockRequest(
        PricerDbContext dbContext,
        CreateStockRequest stockRequests
    );
    public void DeleteStockRequest(PricerDbContext dbContext, DeleteStockRequest stockRequests);
    public void GetStockRequestById(PricerDbContext dbContext, GetStockRequest stockRequests);
    public void GetAll(PricerDbContext dbContext);
}
