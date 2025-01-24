using Volo.Abp.Modularity;

namespace Ehsan.CSMS;

/* Inherit from this class for your domain layer tests. */
public abstract class CSMSDomainTestBase<TStartupModule> : CSMSTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
