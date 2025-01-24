using Volo.Abp.Settings;

namespace Ehsan.CSMS.Settings;

public class CSMSSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CSMSSettings.MySetting1));
    }
}
