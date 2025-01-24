using Ehsan.CSMS.Samples;
using Xunit;

namespace Ehsan.CSMS.EntityFrameworkCore.Domains;

[Collection(CSMSTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<CSMSEntityFrameworkCoreTestModule>
{

}
