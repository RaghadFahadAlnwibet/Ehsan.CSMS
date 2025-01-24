using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Ehsan.CSMS.Pages;

[Collection(CSMSTestConsts.CollectionDefinitionName)]
public class Index_Tests : CSMSWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
