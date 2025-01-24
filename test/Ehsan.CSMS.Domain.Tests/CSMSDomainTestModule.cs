using Volo.Abp.Modularity;

namespace Ehsan.CSMS;

[DependsOn(
    typeof(CSMSDomainModule),
    typeof(CSMSTestBaseModule)
)]
public class CSMSDomainTestModule : AbpModule
{

}
