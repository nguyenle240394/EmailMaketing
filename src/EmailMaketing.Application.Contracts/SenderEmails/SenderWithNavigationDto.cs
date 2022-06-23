using EmailMaketing.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMaketing.SenderEmails
{
    public class SenderWithNavigationDto
    {
        public SenderEmailDto SenderEmail { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
