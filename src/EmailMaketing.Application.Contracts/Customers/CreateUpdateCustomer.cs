using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailMaketing.Customers
{
    public class CreateUpdateCustomer
    {
        public Guid UserID { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression("[0-9]{10}")]
        public string PhoneNumber { get; set; }
        [Required]
        /*[RegularExpression("^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$")]*/
        [EmailAddress]
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
