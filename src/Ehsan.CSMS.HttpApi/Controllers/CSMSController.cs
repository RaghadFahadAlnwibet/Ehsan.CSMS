using Ehsan.CSMS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ehsan.CSMS.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CSMSController : AbpControllerBase
{
    protected CSMSController()
    {
        LocalizationResource = typeof(CSMSResource);
    }
}
