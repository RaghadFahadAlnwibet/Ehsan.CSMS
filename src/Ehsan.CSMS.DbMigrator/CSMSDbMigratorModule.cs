using Ehsan.CSMS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Ehsan.CSMS.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CSMSEntityFrameworkCoreModule),
    typeof(CSMSApplicationContractsModule)
)]
public class CSMSDbMigratorModule : AbpModule
{
}
