/*
A DbContext to access bond values
*/

using Microsoft.EntityFrameworkCore;
using PricerApi.Models;

namespace PricerApi.Data;

public class PricerDbContext : DbContext
{
    public PricerDbContext(DbContextOptions<PricerDbContext> options)
        : base(options) { }

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<PriceQuote> PriceQuotes { get; set; }
    public DbSet<Trade> Trades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Filtering for soft delete
        modelBuilder.Entity<Stock>().HasQueryFilter(p => !p.IsDeleted);
    }
}
