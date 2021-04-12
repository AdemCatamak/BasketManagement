using System.Collections.Generic;
using System.Reflection;
using BasketManagement.Shared.Infrastructure.Db;
using FluentMigrator.Runner.VersionTableInfo;

namespace BasketManagement.BasketModule.Infrastructure.Db.Migrations
{
    public class BasketModuleMigrationRunner : BaseDbMigrationEngine
    {
        public BasketModuleMigrationRunner(AppDbConfig appDbConfig)
        {
            AppDbConfig = appDbConfig;
            VersionTableMetaData = new _0001_VersionTable();
            Assemblies = new[] {typeof(_0001_VersionTable).Assembly};
        }

        public override AppDbConfig AppDbConfig { get; }
        public override IReadOnlyList<Assembly> Assemblies { get; }
        public override IVersionTableMetaData? VersionTableMetaData { get; }
    }
}