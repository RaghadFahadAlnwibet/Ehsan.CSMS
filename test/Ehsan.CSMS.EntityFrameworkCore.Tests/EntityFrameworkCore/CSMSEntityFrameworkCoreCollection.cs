using Xunit;

namespace Ehsan.CSMS.EntityFrameworkCore;

[CollectionDefinition(CSMSTestConsts.CollectionDefinitionName)]
public class CSMSEntityFrameworkCoreCollection : ICollectionFixture<CSMSEntityFrameworkCoreFixture>
{

}
