using EmailMaketing.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EmailMaketing.Permissions;

public class EmailMaketingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EmailMaketingPermissions.MyPermission1, L("Permission:MyPermission1"));

        var emailMaketingGroup = context.AddGroup(EmailMaketingPermissions.GroupName, L("Permission:EmailMaketing"));
        var emailMaketingPermission = emailMaketingGroup.AddPermission(EmailMaketingPermissions.Customers.Default, L("Permission:Customers"));
        emailMaketingPermission.AddChild(EmailMaketingPermissions.Customers.Create, L("Permission:Customers.Create"));
        emailMaketingPermission.AddChild(EmailMaketingPermissions.Customers.Edit, L("Permission:Customers.Edit"));
        emailMaketingPermission.AddChild(EmailMaketingPermissions.Customers.Delete, L("Permission:Customers.Delete"));


        var senderEmailPermission = emailMaketingGroup.AddPermission(EmailMaketingPermissions.EmailManagement.SenderEmails.Default, L("Permission:EmailManagement.SenderEmails"));
        senderEmailPermission.AddChild(EmailMaketingPermissions.EmailManagement.SenderEmails.Create, L("Permission:EmailManagement.SenderEmails.Create"));
        senderEmailPermission.AddChild(EmailMaketingPermissions.EmailManagement.SenderEmails.Edit, L("Permission:EmailManagement.SenderEmails.Edit"));
        senderEmailPermission.AddChild(EmailMaketingPermissions.EmailManagement.SenderEmails.Delete, L("Permission:EmailManagement.SenderEmails.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EmailMaketingResource>(name);
    }
}
