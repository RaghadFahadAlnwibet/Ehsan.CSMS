using Volo.Abp.Modularity;

namespace Ehsan.CSMS;

public abstract class CSMSApplicationTestBase<TStartupModule> : CSMSTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
