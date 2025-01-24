using Ehsan.CSMS.Samples;
using Xunit;

namespace Ehsan.CSMS.EntityFrameworkCore.Applications;

[Collection(CSMSTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<CSMSEntityFrameworkCoreTestModule>
{

}
