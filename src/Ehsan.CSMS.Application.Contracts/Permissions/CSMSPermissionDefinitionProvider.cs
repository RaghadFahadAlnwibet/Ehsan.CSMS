using Ehsan.CSMS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ehsan.CSMS.Permissions;

public class CSMSPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CSMSPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(CSMSPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CSMSResource>(name);
    }
}
