﻿using System.Threading.Tasks;
using EmailMaketing.Localization;
using EmailMaketing.MultiTenancy;
using EmailMaketing.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace EmailMaketing.Web.Menus;

public class EmailMaketingMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private  async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<EmailMaketingResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                EmailMaketingMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
        if (await context.IsGrantedAsync(EmailMaketingPermissions.Customers.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "EmailMaketing.Customers",
                        l["Menu:Customers"],
                        url: "/Customers"
                    )
            );

        }
        if (await context.IsGrantedAsync(EmailMaketingPermissions.SenderEmails.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "EmailMaketing.SenderEmails",
                        l["Menu:SenderEmails"],
                        url: "/SenderEmails"
                    )
            );
        }

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
    }
}
