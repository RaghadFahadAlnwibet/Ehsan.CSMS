using Ehsan.CSMS.Localization;
using Volo.Abp.Application.Services;

namespace Ehsan.CSMS;

/* Inherit your application services from this class.
 */
public abstract class CSMSAppService : ApplicationService
{
    protected CSMSAppService()
    {
        LocalizationResource = typeof(CSMSResource);
    }
}
