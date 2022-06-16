﻿using EmailMaketing.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EmailMaketing.Permissions;

public class EmailMaketingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EmailMaketingPermissions.MyPermission1, L("Permission:MyPermission1"));

        var emailMaketingGroup = context.AddGroup(EmailMaketingPermissions.GroupName, L("Permission:EmailMarketing"));

        var senderEmailPermission = emailMaketingGroup.AddPermission(EmailMaketingPermissions.SenderEmails.Default, L("Permission:SenderEmails"));
        senderEmailPermission.AddChild(EmailMaketingPermissions.SenderEmails.Create, L("Permission:SenderEmails.Create"));
        senderEmailPermission.AddChild(EmailMaketingPermissions.SenderEmails.Edit, L("Permission:SenderEmails.Edit"));
        senderEmailPermission.AddChild(EmailMaketingPermissions.SenderEmails.Delete, L("Permission:SenderEmails.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EmailMaketingResource>(name);
    }
}
