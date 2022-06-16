using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailMaketing.SenderEmails
{
    public class CreateUpdateSenderEmailDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Guid CustomerID { get; set; }
        [Required]
        public bool IsSend { get; set; }
    }
}
