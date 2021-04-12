using System.Collections.Generic;
using System.Reflection;
using FluentMigrator.Runner.VersionTableInfo;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.StockModule.Infrastructure.Db.Migrations
{
    public class StockModuleMigrationRunner : BaseDbMigrationEngine
    {
        public StockModuleMigrationRunner(AppDbConfig appDbConfig)
        {
            AppDbConfig = appDbConfig;
            Assemblies = new[] {typeof(_0001_VersionTable).Assembly};
            VersionTableMetaData = new _0001_VersionTable();
        }

        public override AppDbConfig AppDbConfig { get; }
        public override IReadOnlyList<Assembly> Assemblies { get; }
        public override IVersionTableMetaData? VersionTableMetaData { get; }
    }
}