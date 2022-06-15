using EmailMaketing.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EmailMaketing.Permissions;

public class EmailMaketingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EmailMaketingPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EmailMaketingPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EmailMaketingResource>(name);
    }
}
