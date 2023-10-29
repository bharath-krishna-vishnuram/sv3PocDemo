using System;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.Localization;
using Trial.MvcDemoApplication.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Trial.MvcDemoApplication.Web.Menus;

public class MvcDemoApplicationMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<MvcDemoApplicationResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                MvcDemoApplicationMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        ConfigureAdditionalMenuItems(context, l);

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);



        return Task.CompletedTask;
    }

    private void ConfigureAdditionalMenuItems(MenuConfigurationContext context, Microsoft.Extensions.Localization.IStringLocalizer l)
    {
        var PdmMenu = new ApplicationMenuItem(
                "PdmStore",
                l["Menu:PdmStore"],
                icon: "fa fa-Structure"
            );
        PdmMenu.AddItem(
                new ApplicationMenuItem(
                    "PdmStore.Structures",
                    l["Menu:Structures"],
                    url: "/PDM/Structures"
                )
            );
        PdmMenu.AddItem(
                new ApplicationMenuItem(
                    "PdmStore.Components",
                    l["Menu:Components"],
                    url: "/PDM/Components"
                ));
        PdmMenu.AddItem(
                new ApplicationMenuItem(
                    "PdmStore.TextManager",
                    l["Menu:TextManager"],
                    url: "/PDM/TextManager"
                ));
        PdmMenu.AddItem(
                new ApplicationMenuItem(
                    "PdmStore.Options",
                    l["Menu:Options"],
                    url: "/PDM/Options"
                ));
        context.Menu.AddItem(PdmMenu);

    }
}
