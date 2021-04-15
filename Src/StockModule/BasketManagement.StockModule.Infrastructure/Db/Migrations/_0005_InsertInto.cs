using System;
using System.Collections.Generic;
using FluentMigrator;

namespace BasketManagement.StockModule.Infrastructure.Db.Migrations
{
    [Migration(5)]
    public class _0005_InsertInto : Migration
    {
        public override void Up()
        {
            var stockActions = new List<dynamic>()
            {
                new {Id = Guid.NewGuid(), ProductId = "phone", StockActionType = 1, Count = 50, CorrelationId = $"seed-data-{Guid.NewGuid()}", CreatedOn = DateTime.UtcNow},
                new {Id = Guid.NewGuid(), ProductId = "sun-flower", StockActionType = 1, Count = 20, CorrelationId = $"seed-data-{Guid.NewGuid()}", CreatedOn = DateTime.UtcNow},
                new {Id = Guid.NewGuid(), ProductId = "backpack", StockActionType = 1, Count = 0, CorrelationId = $"seed-data-{Guid.NewGuid()}", CreatedOn = DateTime.UtcNow},
                new {Id = Guid.NewGuid(), ProductId = "jeans", StockActionType = 1, Count = 12, CorrelationId = $"seed-data-{Guid.NewGuid()}", CreatedOn = DateTime.UtcNow},
                new {Id = Guid.NewGuid(), ProductId = "monitor", StockActionType = 1, Count = 5, CorrelationId = $"seed-data-{Guid.NewGuid()}", CreatedOn = DateTime.UtcNow},
                new {Id = Guid.NewGuid(), ProductId = "tv", StockActionType = 1, Count = 16, CorrelationId = $"seed-data-{Guid.NewGuid()}", CreatedOn = DateTime.UtcNow},
                new {Id = Guid.NewGuid(), ProductId = "head-set", StockActionType = 1, Count = 70, CorrelationId = $"seed-data-{Guid.NewGuid()}", CreatedOn = DateTime.UtcNow}
            };

            foreach (var stockAction in stockActions)
            {
                Insert.IntoTable("StockActions")
                    .InSchema("dbo.Stock")
                    .Row(stockAction);

                Insert.IntoTable("StockSnapshots")
                    .InSchema("dbo.Stock")
                    .Row(new
                        {
                            Id = Guid.NewGuid(),
                            ProductId = stockAction.ProductId,
                            AvailableStock = stockAction.Count,
                            StockActionId = stockAction.Id,
                            LastStockActionDate = stockAction.CreatedOn
                        }
                    );

                Insert.IntoTable("Stocks")
                    .InSchema("dbo.Stock")
                    .Row(new
                    {
                        Id = Guid.NewGuid(),
                        ProductId = stockAction.ProductId,
                        AvailableStock = stockAction.Count,
                        StockActionId = stockAction.Id,
                        LastStockActionDate = stockAction.CreatedOn
                    });
            }
        }

        public override void Down()
        {
            Delete.FromTable("StockActions")
                .InSchema("dbo.Stock")
                .AllRows();

            Delete.FromTable("StockSnapshots")
                .InSchema("dbo.Stock")
                .AllRows();

            Delete.FromTable("Stocks")
                .InSchema("dbo.Stock")
                .AllRows();
        }
    }
}