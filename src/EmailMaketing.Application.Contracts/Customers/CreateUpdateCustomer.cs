﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailMaketing.Customers
{
    public class CreateUpdateCustomer
    {
        public Guid UserID { get; set; }
        public Guid CustomerTypeID { get; set; }
        public CustomerType Type { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
