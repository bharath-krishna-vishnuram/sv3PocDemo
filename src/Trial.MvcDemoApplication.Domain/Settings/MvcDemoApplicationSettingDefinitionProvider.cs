using Volo.Abp.Settings;

namespace Trial.MvcDemoApplication.Settings;

public class MvcDemoApplicationSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MvcDemoApplicationSettings.MySetting1));
    }
}
