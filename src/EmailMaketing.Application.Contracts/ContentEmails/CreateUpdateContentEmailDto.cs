using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMaketing.ContentEmails
{
    public class CreateUpdateContentEmailDto
    {
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
