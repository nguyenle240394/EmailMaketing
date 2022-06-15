﻿using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EmailMaketing.ContentEmails
{
    public class ContentEmail : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public string Subject { get; set; }
        public DateTime Time { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public bool Status { get; set; }
        public bool Featured { get; set; }
        public Guid CustomerID { get; set; }
        public DateTime Schedule { get; set; }
        public string SenderEmail { get; set; }
    }
}
