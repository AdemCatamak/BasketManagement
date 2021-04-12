using System.Collections.Generic;
using System.Reflection;
using FluentMigrator.Runner.VersionTableInfo;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.AccountModule.Infrastructure.Db.Migrations
{
    public class AccountModuleMigrationRunner : BaseDbMigrationEngine
    {
        public AccountModuleMigrationRunner(AppDbConfig appDbConfig)
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