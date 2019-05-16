using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class ApplicationUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
