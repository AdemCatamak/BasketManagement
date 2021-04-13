using FluentMigrator;

namespace BasketManagement.BasketModule.Infrastructure.Db.Migrations
{
    [Migration(3)]
    public class _0003_CreateTable_BasketLine : Migration
    {
        public override void Up()
        {
            Create.Table("BasketLines").InSchema("dbo.Basket")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("BasketId").AsGuid().NotNullable().ForeignKey("FK__BasketLine_BasketId__Basket_Id", "dbo.Basket", "Baskets", "Id")
                .WithColumn("ProductId").AsString().NotNullable()
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("UpdatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("RowVersion").AsCustom("rowversion")
                ;
        }

        public override void Down()
        {
            Delete.Table("BasketLines").InSchema("dbo.Basket");
        }
    }
}