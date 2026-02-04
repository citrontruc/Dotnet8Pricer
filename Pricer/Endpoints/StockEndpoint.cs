/*
A script to write all the endpoints for our stock operations.
*/

using Microsoft.AspNetCore.Mvc;
using PricerApi.Data;
using PricerApi.Models;
using PricerApi.StockRepository;
using PricerApi.StockRequests;

namespace PricerApi.Endpoints;

public static class StockEndpoint
{
    #region Create Endpoints
    /// <summary>
    /// Extension of the app in order to add endpoints to retrieve albums
    /// </summary>
    /// <param name="app">The api to add endpoints to</param>
    public static void MapAlbumEndpoints(this IEndpointRouteBuilder app)
    {
        var stockGroup = app.MapGroup("/stocks");
        stockGroup
            .MapPost("/", CreateStock)
            .WithName("CreateStocks")
            .WithSummary("Creates a new stock");

        stockGroup.MapGet("/", GetStock).WithName("GetStocks").WithSummary("Get all stocks");

        app.MapGet("/{id:guid?}", GetStockById)
            .WithName("GetStocksById")
            .WithSummary("Get a specific stock by ID")
            .WithDescription(
                "Queries the stock with the corresponding id from the database and retrieves its information."
            );

        app.MapDelete("/{id:guid}", DeleteAlbum)
            .WithName("Delete Stock")
            .WithSummary("Delete a stock given its ID.");
    }
    #endregion

    #region Resolve endpoints
    private static async Task<IResult> CreateStock(
        PricerDbContext db,
        IStockRepository repo,
        CreateStockRequest request
    )
    {
        Stock stock = await repo.CreateStockRequest(db, request);
        return Results.Created($"/api/stocks/{stock.Id}", stock);
    }

    private static async Task<IResult> GetStock(
        PricerDbContext db,
        IStockRepository repo,
        int? pageSize,
        int? pageNumber
    )
    {
        return Results.Ok(await repo.GetAll(db, pageSize, pageNumber));
    }

    private static async Task<IResult> GetStockById(
        PricerDbContext db,
        IStockRepository repo,
        Guid? id,
        [FromBody] GetStockRequest? getRequest = null
    )
    {
        Stock? stock;
        if (id.HasValue)
        {
            stock = await repo.GetStockRequestById(db, id.Value);
            return stock is null ? Results.NotFound() : Results.Ok(stock);
        }
        stock = await repo.GetStockRequestById(db, getRequest);
        return stock is null ? Results.NotFound() : Results.Ok(stock);
    }

    private static async Task<IResult> DeleteAlbum(
        PricerDbContext db,
        IStockRepository repo,
        DeleteStockRequest deleteRequest
    )
    {
        Stock? stock = await repo.DeleteStockRequest(db, deleteRequest);
        return stock is null ? Results.NotFound() : Results.Ok(stock);
    }
    #endregion
}
