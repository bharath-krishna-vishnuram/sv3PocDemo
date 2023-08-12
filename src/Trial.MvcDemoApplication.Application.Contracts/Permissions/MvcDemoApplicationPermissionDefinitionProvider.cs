using Trial.MvcDemoApplication.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Trial.MvcDemoApplication.Permissions;

public class MvcDemoApplicationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MvcDemoApplicationPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MvcDemoApplicationPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MvcDemoApplicationResource>(name);
    }
}
