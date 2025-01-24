using Ehsan.CSMS.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ehsan.CSMS.Web.Pages;

public abstract class CSMSPageModel : AbpPageModel
{
    protected CSMSPageModel()
    {
        LocalizationResourceType = typeof(CSMSResource);
    }
}
