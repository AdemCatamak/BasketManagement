using FluentMigrator;

namespace BasketManagement.BasketModule.Infrastructure.Db.Migrations
{
    [Migration(2)]
    public class _0002_CreateTable_Basket : Migration
    {
        public override void Up()
        {
            Create.Table("Baskets").InSchema("dbo.Basket")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AccountId").AsString().NotNullable()
                .WithColumn("BasketStatus").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("UpdatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
                ;
        }

        public override void Down()
        {
            Delete.Table("Baskets").InSchema("dbo.Basket");
        }
    }
}