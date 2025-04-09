using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ehsan.CSMS.Web;

[Dependency(ReplaceServices = true)]
public class CSMSBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CSMS";
}
