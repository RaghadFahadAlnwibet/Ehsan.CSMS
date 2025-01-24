using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Ehsan.CSMS.Web;

[Dependency(ReplaceServices = true)]
public class CSMSBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CSMS";
}
