using Volo.Abp.Modularity;

namespace Ehsan.CSMS;

[DependsOn(
    typeof(CSMSApplicationModule),
    typeof(CSMSDomainTestModule)
)]
public class CSMSApplicationTestModule : AbpModule
{

}
